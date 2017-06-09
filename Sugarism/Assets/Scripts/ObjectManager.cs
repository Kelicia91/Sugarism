using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectManager : MonoBehaviour
{
    void Awake()
    {        
        _calendar = new Calendar(Def.INIT_YEAR, Def.INIT_MONTH, Def.INIT_DAY);
        _mainCharacter = new MainCharacter(Def.INIT_AGE, Def.INIT_MONEY);
        
        _schedule = gameObject.AddComponent<Schedule>();
        _schedule.Constrution(Def.MAX_NUM_ACTION_IN_MONTH, _calendar, _mainCharacter);

        _targetCharacterArray = new TargetCharacter[Manager.Instance.DTTarget.Count];
        for (int i = 0; i < _targetCharacterArray.Length; ++i)
        {
            _targetCharacterArray[i] = new TargetCharacter(i);
        }
    }


    #region Field, Property

    private Calendar _calendar = null;
    public Calendar Calendar { get { return _calendar; } }

    private Schedule _schedule = null;
    public Schedule Schedule { get { return _schedule; } }

    private MainCharacter _mainCharacter = null;
    public MainCharacter MainCharacter { get { return _mainCharacter; } }

    private TargetCharacter[] _targetCharacterArray = null;
    public TargetCharacter[] TargetCharacterArray { get { return _targetCharacterArray; } }

    private int _caseKey = -1;
    public int CaseKey
    {
        get { return _caseKey; }
        set { _caseKey = value; }
    }

    //

    private Dictionary<int, Sugarism.Character>     _characterDict;
    private List<Scenario>                          _scenarioList;

    private int _targetId = -1;
    private Scenario _scenario = null;

    #endregion




    public Sugarism.Character Get(int characterId)
    {
        if (null == _characterDict)
        {
            Log.Error("Not found character dict");
            return null;
        }

        Sugarism.Character c = null;

        bool isGot = _characterDict.TryGetValue(characterId, out c);
        if (isGot)
            return c;
        else
            return null;
    }


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


    private void load()
    {
        bool isLoaded = false;

        // Character DIct
        isLoaded = RsrcLoader.Instance.Load(Configuration.CHARACTER_FILE_PATH, out _characterDict);
        if (true == isLoaded)
        {
            Log.Debug("success to load character dict");
            //foreach (KeyValuePair<int, Sugarism.Character> kv in characterDict)
            //{
            //    string s = string.Format("{0}:{1}", kv.Key, kv.Value.Name);
            //    Log.Debug(s);
            //}
        }
        else
        {
            Log.Error("failed to load character dict");
        }

        // Scenario List
        isLoaded = RsrcLoader.Instance.Load(Configuration.SCENARIO_FOLDER_PATH, out _scenarioList);
        if (true == isLoaded)
        {
            Log.Debug("success to load scenario list");
            //foreach (Scenario s in _scenarioList)
            //{
            //    s.Execute();
            //}
        }
        else
        {
            Log.Error("failed to load scenario list");
        }
    }
}