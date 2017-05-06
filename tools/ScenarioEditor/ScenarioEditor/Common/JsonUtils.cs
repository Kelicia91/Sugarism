﻿using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;


namespace ScenarioEditor
{
    class JsonUtils
    {
        public const string FILE_EXTENSION = ".json";
        public const string FILE_FILTER = "JSON TEXT(.json)|*.json";
        

        public static bool Serialize(object o, out string s, JsonSerializerSettings settings)
        {
            s = string.Empty;

            try
            {
                s = JsonConvert.SerializeObject(o, settings);
            }
            catch (Exception e)
            {
                Log.Error(Properties.Resources.ErrSerialize, e.Message);
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
                Log.Error(Properties.Resources.ErrDeserialize, e.Message);
                return false;
            }

            return true;
        }
    }


    // for blocking from containing AssemblyName(e.g.Namespace) in property '$type' during Serialization.
    public class KnownTypesBinder : ISerializationBinder
    {
        public System.Collections.Generic.IList<Type> KnownTypes { get; set; }

        public Type BindToType(string assemblyName, string typeName)
        {
            foreach(Type t in KnownTypes)
            {
                if (typeName.Equals(t.Name))
                    return t;
            }

            string errmsg = string.Format("unknown type : {0}", typeName);
            throw new JsonSerializationException(errmsg);
        }

        public void BindToName(Type serializedType, out string assemblyName, out string typeName)
        {
            assemblyName = null;
            typeName = serializedType.Name;
        }
    }
    /* How to Use 'KnownTypesBinder'
     * 
        //KnownTypesBinder knownTypesBinder = new KnownTypesBinder
        //{
        //    KnownTypes = new List<System.Type>
        //    {
        //        typeof(Sugarism.Scenario), typeof(Sugarism.Scene),
        //        typeof(Sugarism.CmdLines), typeof(Sugarism.CmdSwitch), typeof(Sugarism.CmdCase)
        //    }
        //};
        //_jsonSerializerSettings.SerializationBinder = knownTypesBinder;     
     *
     */
}
