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

    public void LoadScene(string sceneName)
    {
        Log.Debug(string.Format("==========> LoadScene; {0} <==========", sceneName));
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
