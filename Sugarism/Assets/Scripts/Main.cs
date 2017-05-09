using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Main : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
        bool isLoaded = false;

        Dictionary<int, Sugarism.Character> characterDict = null;
        isLoaded = RsrcLoader.Instance.Load(Configuration.CHARACTER_FILE_PATH, out characterDict);
        if (true == isLoaded)
        {
            Log.Debug("success to load character dict");
            foreach(KeyValuePair<int, Sugarism.Character> kv in characterDict)
            {
                string s = string.Format("{0}:{1}", kv.Key, kv.Value.Name);
                Log.Debug(s);
            }
        }
        else
        {
            Log.Error("failed to load character dict");
        }


        List<Scenario> scenarioList = null;
        isLoaded = RsrcLoader.Instance.Load(Configuration.SCENARIO_FOLDER_PATH, out scenarioList);
        if (true == isLoaded)
        {
            Log.Debug("success to load scenario list");
            foreach (Scenario s in scenarioList)
            {
                s.Execute();
            }
        }
        else
        {
            Log.Error("failed to load scenario list");
        }
	}
}
