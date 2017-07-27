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

        // events
        _endNurtureEvent = new EndNurtureEvent();

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

    /***** Events *****/
    private EndNurtureEvent _endNurtureEvent = null;
    public EndNurtureEvent EndNurtureEvent { get { return _endNurtureEvent; } }

    /***** Nurture & Story Chain *****/
    private bool _isStartTargetScenario = false;

    /***** Ending *****/
    private int _nurtureEndingId = -1;

    #endregion  // Field, Property



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