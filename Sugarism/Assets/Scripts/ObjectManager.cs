﻿using System;
using UnityEngine;


public class ObjectManager : MonoBehaviour
{
    #region Field, Property

    private MainCharacter _mainCharacter = null;
    public MainCharacter MainCharacter { get { return _mainCharacter; } }

    /***** Story *****/
    private Story.Mode _storyMode = null;
    public Story.Mode StoryMode { get { return _storyMode; } }

    /***** Nurture *****/
    private Nurture.Mode _nurtureMode = null;
    public Nurture.Mode NurtureMode { get { return _nurtureMode; } }

    /***** BoardGame *****/
    private BoardGame.BoardGameMode _boardGameMode = null;
    public BoardGame.BoardGameMode BoardGameMode { get { return _boardGameMode; } }

    /***** Combat *****/
    private Combat.CombatMode _combatMode = null;
    public Combat.CombatMode CombatMode { get { return _combatMode; } }

    /***** Events *****/
    private EndNurtureEvent _endNurtureEvent = null;
    public EndNurtureEvent EndNurtureEvent { get { return _endNurtureEvent; } }

    /***** Nurture & Story Chain *****/
    private bool _isStartTargetScenario = false;

    /***** Ending *****/
    private int _nurtureEndingId = -1;

    #endregion  // Field, Property


    //
    void Awake()
    {
        // load
        Nurture.Calendar calendar = loadCalendar();

        _mainCharacter = loadMainCharacter(calendar);
        loadAllStat(_mainCharacter);

        Story.TargetCharacter targetCharacter = loadTargetCharacter();

        // story
        _storyMode = new Story.Mode(targetCharacter);

        // nurture
        _nurtureMode = new Nurture.Mode(calendar, _mainCharacter);

        // board game
        _boardGameMode = new BoardGame.BoardGameMode();

        // combat
        _combatMode = new Combat.CombatMode();

        // events
        _endNurtureEvent = new EndNurtureEvent();

        // attach handler
        NurtureMode.Schedule.EndEvent.Attach(onScheduleEnd);
    }

    
    void Start()
    {
        if (null == StoryMode.TargetCharacter)
            startPrologue();
        else
            StoryMode.ScenarioEndEvent.Attach(onScenarioEnd);
    }


    /********** Prologue **********/
    private void startPrologue()
    {
        string path = string.Format("{0}{1}{2}", RsrcLoader.SCENARIO_FOLDER_PATH,
                                    RsrcLoader.DIR_SEPARATOR, RsrcLoader.PROLOGUE_FILENAME);

        StoryMode.LoadScenario(path);
        StoryMode.ScenarioEndEvent.Attach(onPrologueEnd);
    }

    private void onPrologueEnd()
    {
        StoryMode.ScenarioEndEvent.Detach(onPrologueEnd);
        StoryMode.SelectTargetEvent.Invoke();
    }

    public void CreateTargetCharacter(int targetId)
    {
        if (false == ExtTarget.isValid(targetId))
        {
            Log.Error(string.Format("invalid target id; {0}", targetId));
            return;
        }

        CustomPlayerPrefs.SetInt(PlayerPrefsKey.TARGET, targetId);
        Story.TargetCharacter tc = loadTargetCharacter();
        StoryMode.Set(tc);

        startTargetPrologue(tc);
    }

    private void startTargetPrologue(Story.TargetCharacter targetCharacter)
    {
        string path = string.Format("{0}{1}", targetCharacter.ScenarioDirPath, RsrcLoader.PROLOGUE_FILENAME);

        StoryMode.LoadScenario(path);
        StoryMode.ScenarioEndEvent.Attach(onTargetPrologueEnd);
    }

    private void onTargetPrologueEnd()
    {
        StoryMode.ScenarioEndEvent.Detach(onTargetPrologueEnd);
        StoryMode.ScenarioEndEvent.Attach(onScenarioEnd);
    }


    /********** Load **********/
    private Nurture.Calendar loadCalendar()
    {
        int year = CustomPlayerPrefs.GetInt(PlayerPrefsKey.YEAR, -1);
        if (year < 0)
        {
            Log.Error(string.Format("not found '{0}'", PlayerPrefsKey.YEAR));
            return null;
        }

        int month = CustomPlayerPrefs.GetInt(PlayerPrefsKey.MONTH, -1);
        if (month < 0)
        {
            Log.Error(string.Format("not found '{0}'", PlayerPrefsKey.MONTH));
            return null;
        }

        Nurture.Calendar calendar = new Nurture.Calendar(year, month, Def.INIT_DAY);
        return calendar;
    }

    private MainCharacter loadMainCharacter(Nurture.Calendar calendar)
    {
        if (null == calendar)
        {
            Log.Error("not found calendar");
            return null;
        }

        string name = CustomPlayerPrefs.GetString(PlayerPrefsKey.NAME, null);
        if (null == name)
        {
            Log.Error(string.Format("not found Player's '{0}'", PlayerPrefsKey.NAME));
            return null;
        }

        int zodiac = CustomPlayerPrefs.GetInt(PlayerPrefsKey.ZODIAC, -1);
        if (false == Enum.IsDefined(typeof(EZodiac), zodiac))
        {
            Log.Error(string.Format("not found '{0}'", PlayerPrefsKey.ZODIAC));
            return null;
        }
        
        int age = Def.INIT_AGE + (calendar.Year - Def.INIT_YEAR);

        int condition = CustomPlayerPrefs.GetInt(PlayerPrefsKey.CONDITION, -1);
        if (false == Enum.IsDefined(typeof(Nurture.ECondition), condition))
        {
            Log.Error(string.Format("not found '{0}'", PlayerPrefsKey.CONDITION));
            return null;
        }

        int[] actionCount = new int[Manager.Instance.DT.Action.Count];
        int numAction = actionCount.Length;
        for (int id = 0; id < numAction; ++id)
        {
            string key = PlayerPrefsKey.GetKey(PlayerPrefsKey.ACTION_COUNT, id);
            int value = CustomPlayerPrefs.GetInt(key, -1);
            if (value < 0)
            {
                Log.Error(string.Format("not found '{0}'", key));
                return null;
            }

            actionCount[id] = value;
        }

        int constitution = CustomPlayerPrefs.GetInt(PlayerPrefsKey.CONSTITUTION, -1);
        if (false == Enum.IsDefined(typeof(EConstitution), constitution))
        {
            Log.Error(string.Format("not found '{0}'", PlayerPrefsKey.CONSTITUTION));
            return null;
        }

        int money = CustomPlayerPrefs.GetInt(PlayerPrefsKey.MONEY, -1);
        if (money < Def.MIN_MONEY)
        {
            Log.Error(string.Format("not found '{0}'", PlayerPrefsKey.MONEY));
            return null;
        }

        int wearingCostumeId = CustomPlayerPrefs.GetInt(PlayerPrefsKey.WEARING_COSTUME, -1);
        if (false == ExtMainCharacterCostume.IsValid(wearingCostumeId))
        {
            Log.Error(string.Format("not found '{0}'", PlayerPrefsKey.WEARING_COSTUME));
            return null;
        }

        Wardrobe wardrobe = new Wardrobe();
        int costumeCount = Manager.Instance.DT.MainCharacterCostume.Count;
        for (int id = 0; id < costumeCount; ++id)
        {
            string key = PlayerPrefsKey.GetKey(PlayerPrefsKey.ISBUY_COSTUME, id);
            int value = CustomPlayerPrefs.GetInt(key, -1);
            if (-1 == value)
            {
                Log.Error(string.Format("not found '{0}'", key));
                return null;
            }

            bool isBuy = PlayerPrefsKey.GetIntToBool(value);
            CostumeController costume = new CostumeController(id, isBuy);
            wardrobe.CostumeList.Add(costume);
        }

        EZodiac z = (EZodiac)zodiac;
        Nurture.ECondition c = (Nurture.ECondition)condition;
        EConstitution cs = (EConstitution)constitution;

        MainCharacter mc = new MainCharacter(name, z, age, c, actionCount, cs, money, wardrobe, wearingCostumeId);
        return mc;
    }

    private void loadAllStat(Nurture.Character character)
    {
        if (null == character)
        {
            Log.Error("not found Nurture.Character");
            return;
        }

        int stress = CustomPlayerPrefs.GetInt(PlayerPrefsKey.STRESS, -1);
        int stamina = CustomPlayerPrefs.GetInt(PlayerPrefsKey.STAMINA, -1);
        int intellect = CustomPlayerPrefs.GetInt(PlayerPrefsKey.INTELLECT, -1);
        int grace = CustomPlayerPrefs.GetInt(PlayerPrefsKey.GRACE, -1);
        int charm = CustomPlayerPrefs.GetInt(PlayerPrefsKey.CHARM, -1);
        int attack = CustomPlayerPrefs.GetInt(PlayerPrefsKey.ATTACK, -1);
        int defense = CustomPlayerPrefs.GetInt(PlayerPrefsKey.DEFENSE, -1);
        int leadership = CustomPlayerPrefs.GetInt(PlayerPrefsKey.LEADERSHIP, -1);
        int tactic = CustomPlayerPrefs.GetInt(PlayerPrefsKey.TACTIC, -1);
        int morality = CustomPlayerPrefs.GetInt(PlayerPrefsKey.MORALITY, -1);
        int goodness = CustomPlayerPrefs.GetInt(PlayerPrefsKey.GOODNESS, -1);
        int sensibility = CustomPlayerPrefs.GetInt(PlayerPrefsKey.SENSIBILITY, -1);
        int arts = CustomPlayerPrefs.GetInt(PlayerPrefsKey.ARTS, -1);

        character.Stress = stress;
        character.Stamina = stamina;
        character.Intellect = intellect;
        character.Grace = grace;
        character.Charm = charm;
        character.Attack = attack;
        character.Defense = defense;
        character.Leadership = leadership;
        character.Tactic = tactic;
        character.Morality = morality;
        character.Goodness = goodness;
        character.Sensibility = sensibility;
        character.Arts = arts;
    }

    private Story.TargetCharacter loadTargetCharacter()
    {
        int targetId = CustomPlayerPrefs.GetInt(PlayerPrefsKey.TARGET, -1);
        if (-1 == targetId)
        {
            Log.Debug("not found target -> startPrologue.");
            return null;
        }   

        int feeling = CustomPlayerPrefs.GetInt(PlayerPrefsKey.FEELING, -1);
        if (feeling < 0)
        {
            Log.Error(string.Format("not found '{0}'", PlayerPrefsKey.FEELING));
            return null;
        }

        int lastOpenedScenarioNo = CustomPlayerPrefs.GetInt(PlayerPrefsKey.LAST_OPENED_SCENARIO_NO, -10);
        if (-10 == lastOpenedScenarioNo)
        {
            Log.Error(string.Format("not found '{0}'", PlayerPrefsKey.LAST_OPENED_SCENARIO_NO));
            return null;
        }

        Story.TargetCharacter tc = new Story.TargetCharacter(targetId, feeling, lastOpenedScenarioNo);
        return tc;
    }


    /********** Ending **********/
    public void EndNurture()
    {
        _nurtureEndingId = NurtureMode.GetEndingId();
        if (false == ExtNurtureEnding.isValid(_nurtureEndingId))
        {
            Log.Error(string.Format("invalid nurture.ending id; {0}", _nurtureEndingId));
            return;
        }

        NurtureEnding ending = Manager.Instance.DT.NurtureEnding[_nurtureEndingId];
        Log.Debug(string.Format("nurture.ending id({0}), name({1})", _nurtureEndingId, ending.name));

        TextAsset nurtureEndingScenario = ending.scenario;
        
        bool isLoaded = StoryMode.LoadScenario(nurtureEndingScenario);
        if (isLoaded)
        {
            // @warn : callback's calling order
            StoryMode.ScenarioEndEvent.Attach(onNurtureEndingScenarioEnd);
        }
    }

    private void onNurtureEndingScenarioEnd()
    {
        Log.Debug("onNurtureEndingScenarioEnd");

        StoryMode.ScenarioEndEvent.Detach(onNurtureEndingScenarioEnd);

        string key = PlayerPrefsKey.GetKey(PlayerPrefsKey.ISLOCKED_NURTURE_ENDING, _nurtureEndingId);
        int value = PlayerPrefsKey.GetBoolToInt(false);
        CustomPlayerPrefs.SetInt(key, value);

        if (Def.NURTURE_BAD_ENDING_ID == _nurtureEndingId)
        {
            Manager.Instance.End();
            return;
        }

        _nurtureEndingId = -1;

        EndNurtureEvent.Invoke();
    }
    
    //
    public void EndStory()
    {
        string path = StoryMode.GetEndingScenarioPath(MainCharacter);
        Log.Debug(string.Format("story.ending path: {0}", path));

        bool isLoaded = StoryMode.LoadScenario(path);
        if (isLoaded)
        {
            // @warn : callback's calling order
            StoryMode.ScenarioEndEvent.Attach(onStoryEndingScenarioEnd);
        }
    }

    private void onStoryEndingScenarioEnd()
    {
        Log.Debug("onStoryEndingScenarioEnd");

        StoryMode.ScenarioEndEvent.Detach(onStoryEndingScenarioEnd);

        Manager.Instance.End();
    }


    /********** Game Routine **********/
    private void onScheduleEnd()
    {
        autoSaveNurture();
        
        startNextScenario();
        _isStartTargetScenario = true;
    }

    private void onScenarioEnd()
    {
        if (false == _isStartTargetScenario)
            return;

        _isStartTargetScenario = false;

        Story.TargetCharacter target = StoryMode.TargetCharacter;
        target.NextScenarioNo();

        CustomPlayerPrefs.SetInt(PlayerPrefsKey.FEELING, target.Feeling);
        CustomPlayerPrefs.SetInt(PlayerPrefsKey.LAST_OPENED_SCENARIO_NO, target.LastOpenedScenarioNo);
    }


    //
    private void autoSaveNurture()
    {
        Nurture.Calendar calendar = NurtureMode.Calendar;
        CustomPlayerPrefs.SetInt(PlayerPrefsKey.YEAR, calendar.Year);
        CustomPlayerPrefs.SetInt(PlayerPrefsKey.MONTH, calendar.Month);

        Nurture.Character nurtureCharacter = NurtureMode.Character;
        int condition = (int)nurtureCharacter.Condition;
        CustomPlayerPrefs.SetInt(PlayerPrefsKey.CONDITION, condition);

        int actionCount = Manager.Instance.DT.Action.Count;
        for (int id = 0; id < actionCount; ++id)
        {
            string key = PlayerPrefsKey.GetKey(PlayerPrefsKey.ACTION_COUNT, id);
            CustomPlayerPrefs.SetInt(key, nurtureCharacter.GetActionCount(id));
        }

        CustomPlayerPrefs.SetInt(PlayerPrefsKey.MONEY, nurtureCharacter.Money);

        CustomPlayerPrefs.SetInt(PlayerPrefsKey.STRESS, nurtureCharacter.Stress);
        CustomPlayerPrefs.SetInt(PlayerPrefsKey.STAMINA, nurtureCharacter.Stamina);
        CustomPlayerPrefs.SetInt(PlayerPrefsKey.INTELLECT, nurtureCharacter.Intellect);
        CustomPlayerPrefs.SetInt(PlayerPrefsKey.GRACE, nurtureCharacter.Grace);
        CustomPlayerPrefs.SetInt(PlayerPrefsKey.CHARM, nurtureCharacter.Charm);
        CustomPlayerPrefs.SetInt(PlayerPrefsKey.ATTACK, nurtureCharacter.Attack);
        CustomPlayerPrefs.SetInt(PlayerPrefsKey.DEFENSE, nurtureCharacter.Defense);
        CustomPlayerPrefs.SetInt(PlayerPrefsKey.LEADERSHIP, nurtureCharacter.Leadership);
        CustomPlayerPrefs.SetInt(PlayerPrefsKey.TACTIC, nurtureCharacter.Tactic);
        CustomPlayerPrefs.SetInt(PlayerPrefsKey.MORALITY, nurtureCharacter.Morality);
        CustomPlayerPrefs.SetInt(PlayerPrefsKey.GOODNESS, nurtureCharacter.Goodness);
        CustomPlayerPrefs.SetInt(PlayerPrefsKey.SENSIBILITY, nurtureCharacter.Sensibility);
        CustomPlayerPrefs.SetInt(PlayerPrefsKey.ARTS, nurtureCharacter.Arts);
    }

    private void startNextScenario()
    {
        Story.TargetCharacter target = StoryMode.TargetCharacter;

        bool isLoaded = StoryMode.LoadScenario(target.NextScenarioPath);
        if (false == isLoaded)
            return;
    }
}