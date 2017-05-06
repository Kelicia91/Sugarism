using System;
using Newtonsoft.Json;


namespace CharacterEditor
{
    class JsonUtils
    {
        public const string FILE_EXTENSION  = ".json";
        public const string FILE_FILTER     = "JSON TEXT(.json)|*.json";
        

        public static bool Serialize(object o, out string s)
        {
            s = string.Empty;

            try
            {
                s = JsonConvert.SerializeObject(o);
            }
            catch(Exception e)
            {
                Log.Error(Properties.Resources.ErrSerialize, e.Message);
                return false;
            }

            return true;
        }


        public static bool Deserialize<T>(string s, out object o)
        {
            o = null;

            try
            {
                o = JsonConvert.DeserializeObject<T>(s);
            }
            catch(Exception e)
            {
                Log.Error(Properties.Resources.ErrDeserialize, e.Message);
                return false;
            }

            return true;
        }
    }
}
