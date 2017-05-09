
public class Log
{
    private const string DEBUG = "DEBUG";
    private const string ERROR = "ERROR";
    
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
}
