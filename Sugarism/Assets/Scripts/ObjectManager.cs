using UnityEngine;


public class ObjectManager : MonoBehaviour
{
    void Awake()
    {
        _mainCharacter = new MainCharacter(Def.INIT_AGE, Def.INIT_MONEY, Def.DEFAULT_COSTUME_ID);

        // story
        int targetId = 0;   // @todo: TEST
        _storyMode = new Story.Mode(_mainCharacter, targetId);

        // nurture
        _nurtureMode = new Nurture.Mode(_mainCharacter);

        // board game
        _boardGameMode = new BoardGame.BoardGameMode();

        // combat
        _combatMode = new Combat.CombatMode();

        // attach handler
        StoryMode.ScenarioEndEvent.Attach(onScenarioEnd);
        NurtureMode.Schedule.EndEvent.Attach(onScheduleEnd);
    }


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

    /***** Ending *****/
    private int _nurtureEndingId = -1;
    private bool _isStartStoryEndingScenario = false;

    /***** Nurture & Story Chain *****/
    private bool _isStartTargetScenario = false;

    #endregion  // Field, Property


    public void Ending()
    {
        _nurtureEndingId = NurtureMode.GetEndingId();
        Log.Debug(string.Format("ending id: {0}", _nurtureEndingId));

        if (false == ExtNurtureEnding.isValid(_nurtureEndingId))
        {
            Log.Error(string.Format("invalid nurture.ending id; {0}", _nurtureEndingId));
            return;
        }

        NurtureEnding ending = Manager.Instance.DTNurtureEnding[_nurtureEndingId];
        Log.Debug(string.Format("nurture ending name: {0}", ending.name));

        TextAsset nurtureEndingScenario = ending.scenario;
        
        bool isLoaded = StoryMode.LoadScenario(nurtureEndingScenario);
        if (false == isLoaded)
            return;
        
        // @warn : callback's calling order
        StoryMode.ScenarioEndEvent.Attach(onNurtureEndingScenarioEnd);
    }

    private void onNurtureEndingScenarioEnd()
    {
        if (_isStartStoryEndingScenario)
        {
            _isStartStoryEndingScenario = false;
            return; // @todo: 스토리 엔딩 끝. 게임종료.
        }

        if (Def.NURTURE_BAD_ENDING_ID == _nurtureEndingId)
            return; // @todo: 육성 엔딩 끝. 스토리 엔딩 없음.(떠돌이) 게임종료.
        else
            _nurtureEndingId = -1;
        
        string storyEndingScenarioPath = StoryMode.GetEndingScenarioPath();
        if (null == storyEndingScenarioPath)
            return; // @todo: 육성 엔딩 끝. 스토리 엔딩 없음.(호감도부족) 게임종료.

        bool isLoaded = StoryMode.LoadScenario(storyEndingScenarioPath);
        if (false == isLoaded)
            return; // @todo: 에러. 게임종료.

        _isStartStoryEndingScenario = true;
    }



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