using System;
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
        _storyMode = new Story.Mode(_mainCharacter, targetCharacter);

        // nurture
        _nurtureMode = new Nurture.Mode(calendar, _mainCharacter);

        // board game
        _boardGameMode = new BoardGame.BoardGameMode();

        // combat
        _combatMode = new Combat.CombatMode();

        // events
        _endNurtureEvent = new EndNurtureEvent();

        // attach handler
        StoryMode.ScenarioEndEvent.Attach(onScenarioEnd);
        NurtureMode.Schedule.EndEvent.Attach(onScheduleEnd);
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
            string key = PlayerPrefsKey.GetActionCountKey(id);
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
            string key = PlayerPrefsKey.GetCostumeKey(id);
            int value = CustomPlayerPrefs.GetInt(key, -1);
            if (-1 == value)
            {
                Log.Error(string.Format("not found '{0}'", key));
                return null;
            }

            bool isBuy = PlayerPrefsKey.GetCostumeValue(value);
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
        if (false == ExtTarget.isValid(targetId))
        {
            Log.Error(string.Format("not found '{0}'", PlayerPrefsKey.TARGET));
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
        if (false == isLoaded)
            return; // @todo: 에러. 게임종료.
        
        // @warn : callback's calling order
        StoryMode.ScenarioEndEvent.Attach(onNurtureEndingScenarioEnd);
    }

    private void onNurtureEndingScenarioEnd()
    {
        Log.Debug("onNurtureEndingScenarioEnd");

        StoryMode.ScenarioEndEvent.Detach(onNurtureEndingScenarioEnd);

        if (Def.NURTURE_BAD_ENDING_ID == _nurtureEndingId)
            return; // @todo: 육성 엔딩 끝. 스토리 엔딩 없음.(떠돌이) 게임종료.

        _nurtureEndingId = -1;

        EndNurtureEvent.Invoke();
    }
    
    //
    public void EndStory()
    {
        string path = StoryMode.GetEndingScenarioPath();
        if (null == path)
            return; // @todo: 에러. 게임종료.

        Log.Debug(string.Format("story.ending path: {0}", path));

        bool isLoaded = StoryMode.LoadScenario(path);
        if (false == isLoaded)
            return; // @todo: 에러. 게임종료.

        StoryMode.ScenarioEndEvent.Attach(onStoryEndingScenarioEnd);
    }

    private void onStoryEndingScenarioEnd()
    {
        Log.Debug("onStoryEndingScenarioEnd");

        StoryMode.ScenarioEndEvent.Detach(onStoryEndingScenarioEnd);

        // @todo: 게임종료.
    }

    //
    private void onScheduleEnd()
    {
        Story.TargetCharacter target = StoryMode.TargetCharacter;

        bool isLoaded = StoryMode.LoadScenario(target.NextScenarioPath);
        if (false == isLoaded)
            return;

        _isStartTargetScenario = true;
    }

    private void onScenarioEnd()
    {
        if (false == _isStartTargetScenario)
            return;

        _isStartTargetScenario = false;

        Story.TargetCharacter target = StoryMode.TargetCharacter;
        target.NextScenarioNo();
    }
}