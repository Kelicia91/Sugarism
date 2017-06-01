using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectManager : MonoBehaviour
{
    void Awake()
    {        
        _calendar = new Calendar(Def.INIT_YEAR, Def.INIT_MONTH, Def.INIT_DAY);
        _heroine = new Heroine(Def.INIT_AGE, Def.INIT_MONEY);
        
        _schedule = gameObject.AddComponent<Schedule>();
        _schedule.Constrution(Def.MAX_NUM_ACTION_IN_MONTH, _calendar, _heroine);
        

        //load();

        //if((null != _scenarioList) && (_scenarioList.Count > 0))
        //    _selectedScenario = _scenarioList[0];   // sample
    }


    #region Field, Property

    private Calendar _calendar = null;
    public Calendar Calendar { get { return _calendar; } }

    private Schedule _schedule = null;
    public Schedule Schedule { get { return _schedule; } }

    private Heroine _heroine = null;
    public Heroine Heroine { get { return _heroine; } }

    //

    private Dictionary<int, Sugarism.Character>     _characterDict;
    private List<Scenario>                          _scenarioList;

    private Scenario _selectedScenario;

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


    // temp : test (data <-> ui)
    public void NextCmd()
    {
        if (null == _selectedScenario)
            return;

        play();
    }

    private void play()
    {
        bool canMorePlay = _selectedScenario.Play();
        if (false == canMorePlay)
        {
            _selectedScenario = null;
            Log.Debug("end. Scenario");  // @todo : TRANSITION
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