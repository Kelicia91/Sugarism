using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RsrcLoader
{
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
        // singleton
    }


    public bool Load(string path, out Dictionary<int, Sugarism.Character> characterMap)
    {
        characterMap = null;

        //
        if (string.IsNullOrEmpty(path))
            return false;

        TextAsset textAsset = Resources.Load<TextAsset>(path);
        if (null == textAsset)
            return false;

        //
        object result = null;
        bool isDeserialized = JsonUtils.Deserialize<List<Sugarism.Character>>(textAsset.text, out result, null);
        if (false == isDeserialized)
            return false;

        //
        List<Sugarism.Character> cList = result as List<Sugarism.Character>;

        characterMap = new Dictionary<int, Sugarism.Character>();
        foreach(Sugarism.Character c in cList)
        {
            characterMap.Add(c.Id, c);
        }

        return true;
    }

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
