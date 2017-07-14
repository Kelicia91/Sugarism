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
        Log.Debug("RsrcLoader is constructed");
    }
    
    //
    public bool Load(string path, out Sugarism.Scenario scenario)
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

        scenario = result as Sugarism.Scenario;

        return true;
    }
}
