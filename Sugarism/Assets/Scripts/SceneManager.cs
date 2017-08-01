using UnityEngine;


public class SceneManager : MonoBehaviour
{
    /********* Editor Interface *********/
    // Prefabs
    [SerializeField]
    protected CommonManager PrefCommonManager = null;

    /********* Game Interface *********/
    private CommonManager _commonManager = null;
    protected CommonManager CommonManager { get { return _commonManager; } }

    public DataTableCollection DT { get { return CommonManager.DT; } }
    
    //
    protected void Initialize()
    {
        GameObject o = null;
        
        o = GameObject.FindWithTag(CommonManager.TAG);
        if (null == o)
            _commonManager = Instantiate(PrefCommonManager);
        else
            _commonManager = o.GetComponent<CommonManager>();
    }

    protected void LoadScene(string sceneName)
    {
        Log.Debug(string.Format("==========> LoadScene; {0} <==========", sceneName));
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }


    /***** Continue Game *****/
    public const string CONTINUE_KEY = PlayerPrefsKey.NAME;

    public bool IsContinueData()
    {
        string invalidName = string.Empty;
        string playerName = CustomPlayerPrefs.GetString(CONTINUE_KEY, invalidName);
        if (playerName.Equals(invalidName))
            return false;
        else
            return true;
    }

    protected void SetContinueData(string playerName)
    {
        CustomPlayerPrefs.SetString(CONTINUE_KEY, playerName);
    }

    protected void ClearContinueData()
    {
        CustomPlayerPrefs.DeleteKey(CONTINUE_KEY);
    }
}
