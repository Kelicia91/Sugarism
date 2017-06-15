using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RsrcLoader
{
    public const char DIR_SEPARATOR = '/';  // @note: unity file system


    private static RsrcLoader _instance = null;
    public static RsrcLoader Instance
    {
        get
        {
            if (null == _instance)
                _instance = new RsrcLoader();

            return _instance;
        }
    }

    private RsrcLoader()
    {
        // do something
    }
    
    //
    public bool Load(string path, out Scenario scenario)
    {
        Log.Debug(string.Format("try to load scenario: {0}", path));
        scenario = null;

        //
        if (string.IsNullOrEmpty(path))
            return false;

        TextAsset textAsset = Resources.Load<TextAsset>(path);
        if (null == textAsset)
            return false;

        //
        Newtonsoft.Json.JsonSerializerSettings settings = new Newtonsoft.Json.JsonSerializerSettings();
        settings.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto;

        //
        object result = null;
        bool isDeserialized = JsonUtils.Deserialize<Sugarism.Scenario>(textAsset.text, out result, settings);
        if (false == isDeserialized)
        {
            string msg = string.Format("Failed to Load Scenario: {0}", path);
            Log.Error(msg);
            return false;
        }

        Sugarism.Scenario mScenario = result as Sugarism.Scenario;
        scenario = new Scenario(mScenario);

        return true;
    }

    //@todo: remove it
    public bool Load(string path, out List<Scenario> scenarioList)
    {
        scenarioList = null;

        //
        if (string.IsNullOrEmpty(path))
            return false;

        TextAsset[] textAssets = Resources.LoadAll<TextAsset>(path);
        if (null == textAssets)
            return false;

        //
        Newtonsoft.Json.JsonSerializerSettings settings = new Newtonsoft.Json.JsonSerializerSettings();
        settings.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto;

        scenarioList = new List<Scenario>();
        for (int i = 0; i < textAssets.Length; ++i)
        {
            object result = null;
            bool isDeserialized = JsonUtils.Deserialize<Sugarism.Scenario>(textAssets[i].text, out result, settings);
            if (false == isDeserialized)
            {
                string msg = string.Format("Failed to Load {0}th Scenario", i);
                Log.Error(msg);
                return false;
            }

            Sugarism.Scenario mScenario = result as Sugarism.Scenario;
            Scenario scenario = new Scenario(mScenario);
            scenarioList.Add(scenario);
        }

        return true;
    }
}
