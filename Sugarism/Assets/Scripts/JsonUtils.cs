using System;
using Newtonsoft.Json;


public class JsonUtils
{
    public static bool Serialize(object o, out string s, JsonSerializerSettings settings)
    {
        s = null;

        try
        {
            s = JsonConvert.SerializeObject(o, settings);
        }
        catch(Exception e)
        {
            Log.Error(e.Message);
            return false;
        }

        return true;
    }

    public static bool Deserialize<T>(string s, out object o, JsonSerializerSettings settings)
    {
        o = null;

        try
        {
            o = JsonConvert.DeserializeObject<T>(s, settings);
        }
        catch(Exception e)
        {
            Log.Error(e.Message);
            return false;
        }

        return true;
    }
}
