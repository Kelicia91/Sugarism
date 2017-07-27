using UnityEngine;


// prohibit from abusing Singleton.
// @IMPORTANT : prohibit from re-define constuctor because of inheriting MonoBehaviour.
public class Manager : MonoBehaviour
{
    private static Manager _instance = null;
    public static Manager Instance { get { return _instance; } }


    /********* Editor Interface *********/
    // Prefabs
    [SerializeField]
    private DataTableCollection PrefDataTableCollection = null;
    [SerializeField]
    private ObjectManager PrefObjectManager = null;
    [SerializeField]
    private UIManager PrefUIManager = null;
    [SerializeField]
    private TestManager PrefTestManager = null;

    /********* Game Interface *********/
    private DataTableCollection _dataTableCollection = null;
    public DataTableCollection DT { get { return _dataTableCollection; } }

    private ObjectManager _object = null;
    public ObjectManager Object { get { return _object; } }

    private UIManager _ui = null;
    public UIManager UI { get { return _ui; } }


    //
    void Awake()
    {
        _instance = this;
        
        // data table
        GameObject dtObject = GameObject.FindWithTag(DataTableCollection.TAG);
        if (null == dtObject)
            _dataTableCollection = Instantiate(PrefDataTableCollection);
        else
            _dataTableCollection = dtObject.GetComponent<DataTableCollection>();

        // manager
        _object = Instantiate(PrefObjectManager);
        _ui = Instantiate(PrefUIManager);

#if UNITY_EDITOR
        Instantiate(PrefTestManager);
#endif

        // game
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.SetResolution(Screen.width, (Screen.width / Def.RESOLUTION_WIDTH_RATIO * Def.RESOLUTION_HEIGHT_RATIO), true);
        Log.Debug(string.Format("Screen width: {0}, height: {1}", Screen.width, Screen.height));
    }
    
}   // class
 
 