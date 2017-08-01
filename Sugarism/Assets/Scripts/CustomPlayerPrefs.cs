using UnityEngine;

/*******************************************
 *  Wrapper class UnityEngine.PlayerPrefs  *
 *******************************************/
public class CustomPlayerPrefs
{
    public static void DeleteKey(string key)
    {
        //PlayerPrefs.DeleteKey(MakeHash(key + _saltForKey));

        // @note: DeleteAll() 즉시적용 안됨. 약간의 딜레이 필요한듯..
        PlayerPrefs.DeleteKey(key);
    }
 
    public static void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }
 
    public static void Save()
    {
        PlayerPrefs.Save();
    }
 
    public static void SetInt(string key, int value)
    {
        //SetSecurityValue(key, value.ToString());
        PlayerPrefs.SetInt(key, value);
    }
 
    //public static void SetLong(string key, long value)
    //{
    //    SetSecurityValue(key, value.ToString());
    //}
 
    public static void SetFloat(string key, float value)
    {
        //SetSecurityValue(key, value.ToString());
        PlayerPrefs.SetFloat(key, value);
    }
 
    public static void SetString(string key, string value)
    {
        //SetSecurityValue(key, value);
        PlayerPrefs.SetString(key, value);
    }
 
    public static int GetInt(string key, int defaultValue)
    {
        return PlayerPrefs.GetInt(key, defaultValue);

        //string originalValue = GetSecurityValue(key);
        //if (true == string.IsNullOrEmpty(originalValue))
        //    return defaultValue;
 
        //int result = defaultValue;
        //if (false == int.TryParse(originalValue, out result))
        //    return defaultValue;
 
        //return result;
    }
 
    //public static long GetLong(string key, long defaultValue)
    //{
    //    string originalValue = GetSecurityValue(key);
    //    if (true == string.IsNullOrEmpty(originalValue))
    //        return defaultValue;
 
    //    long result = defaultValue;
    //    if (false == long.TryParse(originalValue, out result))
    //        return defaultValue;
 
    //    return result;
    //}
 
    public static float GetFloat(string key, float defaultValue)
    {
        return PlayerPrefs.GetFloat(key, defaultValue);

        //string originalValue = GetSecurityValue(key);
        //if (true == string.IsNullOrEmpty(originalValue))
        //    return defaultValue;
 
        //float result = defaultValue;
        //if (false == float.TryParse(originalValue, out result))
        //    return defaultValue;
 
        //return result;
    }
 
    public static string GetString(string key, string defaultValue)
    {
        return PlayerPrefs.GetString(key, defaultValue);

        //string originalValue = GetSecurityValue(key);
        //if (true == string.IsNullOrEmpty(originalValue))
        //    return defaultValue;
 
        //return originalValue;
    }
}
