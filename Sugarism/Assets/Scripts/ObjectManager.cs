using UnityEngine;


public class ObjectManager : MonoBehaviour
{
    void Awake()
    {
        _mainCharacter = new MainCharacter(Def.INIT_AGE, Def.INIT_MONEY);

        // date
        _targetCharacterArray = new TargetCharacter[Manager.Instance.DTTarget.Count];

        int numTargetCharacterArray = _targetCharacterArray.Length;
        for (int i = 0; i < numTargetCharacterArray; ++i)
        {
            _targetCharacterArray[i] = new TargetCharacter(i);
        }

        // nurture
        _nurtureMode = new Nurture.Mode(_mainCharacter);

        // board game
        _boardGameMode = new BoardGame.BoardGameMode();

        // combat
        _combatMode = new Combat.CombatMode();
    }


    #region Field, Property
    
    private MainCharacter _mainCharacter = null;
    public MainCharacter MainCharacter { get { return _mainCharacter; } }

    /***** Date *****/
    private TargetCharacter[] _targetCharacterArray = null;
    public TargetCharacter[] TargetCharacterArray { get { return _targetCharacterArray; } }

    private int _caseKey = -1;
    public int CaseKey
    {
        get { return _caseKey; }
        set { _caseKey = value; }
    }
    
    private int _targetId = -1;
    private Scenario _scenario = null;

    /***** Nurture *****/
    private Nurture.Mode _nurtureMode = null;
    public Nurture.Mode NurtureMode { get { return _nurtureMode; } }

    /***** BoardGame *****/
    private BoardGame.BoardGameMode _boardGameMode = null;
    public BoardGame.BoardGameMode BoardGameMode { get { return _boardGameMode; } }
    
    /***** Combat *****/
    private Combat.CombatMode _combatMode = null;
    public Combat.CombatMode CombatMode { get { return _combatMode; } }

    #endregion  // Field, Property


    public bool LoadScenario(int targetId)
    {
        if (false == TargetCharacter.isValid(targetId))
        {
            Log.Error(string.Format("invalid target id({0})", targetId));
            return false;
        }

        string dirPath = string.Format("{0}{1}{2}", Configuration.SCENARIO_FOLDER_PATH,
                        RsrcLoader.DIR_SEPARATOR, Manager.Instance.DTTarget[targetId].scenarioDirName);

        TargetCharacter tc = Manager.Instance.Object.TargetCharacterArray[targetId];        
        string filename = (tc.LastOpenedScenarioNo + 1).ToString(); // without extension

        string fullPath = string.Format("{0}{1}{2}", dirPath, 
                        RsrcLoader.DIR_SEPARATOR, filename);

        _scenario = null;
        bool isLoaded = RsrcLoader.Instance.Load(fullPath, out _scenario);

        if (isLoaded)
            _targetId = targetId;

        return isLoaded;
    }


    // temp : test (data <-> ui)
    public void NextCmd()
    {
        if (_isEndedScenario)
        {
            _isEndedScenario = false;
            _scenario = null;
            
            Manager.Instance.ScenarioEndEvent.Invoke();
            return;
        }

        if (null == _scenario)
        {
            Log.Error("not found scenario, load please");
            return;
        }

        play();
    }

    private bool _isEndedScenario = false;
    private void play()
    {
        bool canMorePlay = _scenario.Play();
        if (false == canMorePlay)
        {
            _isEndedScenario = true;

            if (TargetCharacter.isValid(_targetId))
            {
                TargetCharacter c = _targetCharacterArray[_targetId];
                ++c.LastOpenedScenarioNo;

                _targetId = -1;
            }

            Log.Debug("end. scenario");
        }
    }
}