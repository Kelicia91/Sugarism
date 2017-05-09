
public class Log
{
    private const string DEBUG = "DEBUG";
    private const string ERROR = "ERROR";
    private const string ASSERT = "ASSERT";
    
    public static void Debug(string msg)
    {
        string s = string.Format("[{0}] {1}", DEBUG, msg);
        UnityEngine.Debug.Log(s);
    }

    public static void Error(string msg)
    {
        string s = string.Format("[{0}] {1}", ERROR, msg);
        UnityEngine.Debug.LogError(s);
    }

    public static void Assert(bool condition, string errMsg)
    {
        string s = string.Format("[{0}] {1}", ASSERT, errMsg);
        UnityEngine.Debug.Assert(condition, errMsg);
    }
}
