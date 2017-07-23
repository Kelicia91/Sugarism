
public class RsrcLoader
{
    // @note : UnityEngine.Resources
    // - The path is relative to any Resources folder inside the Assets folder of your project, 
    //  extensions must be omitted.
    // - All asset names and paths in Unity use forward slashes, 
    //  paths using backslashes will not work.

    public const char DIR_SEPARATOR = '/';  // @note: unity file system

    //
    public const string SCENARIO_FOLDER_PATH = "Scenarios";
    public const string TARGET_NORMAL_ENDING_FILENAME = "normal";
    public const string TARGET_HAPPY_ENDING_FILENAME = "happy";
}
