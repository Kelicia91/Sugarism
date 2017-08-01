using UnityEngine;


public class CommonManager : MonoBehaviour
{
    // @note : Register this to TagManager
    public const string TAG = "CommonManager";


    /********* Editor Interface *********/
    // Prefabs
    [SerializeField]
    private DataTableCollection PrefDataTableCollection = null;

    /********* Game Interface *********/
    private DataTableCollection _dataTableCollection = null;
    public DataTableCollection DT { get { return _dataTableCollection; } }


    //
    void Awake()
    {
        Log.Debug("CommonManager.Awake");
        DontDestroyOnLoad(this);

        // DataTable
        _dataTableCollection = GetDataTable();

        // Game Configuration
        initConfigure();
    }


    private DataTableCollection GetDataTable()
    {
        GameObject o = GameObject.FindWithTag(DataTableCollection.TAG);
        if (null == o)
            return Instantiate(PrefDataTableCollection);
        else
            return o.GetComponent<DataTableCollection>();
    }


    private void initConfigure()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        Screen.SetResolution(Screen.width, (Screen.width / Def.RESOLUTION_WIDTH_RATIO * Def.RESOLUTION_HEIGHT_RATIO), true);
        Log.Debug(string.Format("Screen width: {0}, height: {1}", Screen.width, Screen.height));
    }
}
