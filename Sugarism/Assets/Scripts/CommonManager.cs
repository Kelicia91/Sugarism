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

#if UNITY_IOS
        blockiOSCodeScrip();
#endif
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


    // @NOTE : When Unity builds iOS, unreferenced code is gone.(= code strip)
    // By the way, If you use JSON deserializer, may need default constructor of objects.
    // but If the default constructor only is defined and NOT referenced, then result in code strip for iOS.
    // so you need to refer the default constructor.
	private void blockiOSCodeScrip()
	{
		// for json deserializer
		Sugarism.Scenario scenario = new Sugarism.Scenario ();
		Sugarism.Scene scene = new Sugarism.Scene ();
		Sugarism.CmdAppear appear = new Sugarism.CmdAppear ();
		Sugarism.CmdBackground bg = new Sugarism.CmdBackground ();
		Sugarism.CmdCase cs = new Sugarism.CmdCase ();
		Sugarism.CmdDisappear ds = new Sugarism.CmdDisappear ();
		Sugarism.CmdFeeling feel = new Sugarism.CmdFeeling ();
		Sugarism.CmdFilter filter = new Sugarism.CmdFilter ();
		Sugarism.CmdLines lines = new Sugarism.CmdLines ();
		Sugarism.CmdMiniPicture mini = new Sugarism.CmdMiniPicture ();
		Sugarism.CmdPicture picture = new Sugarism.CmdPicture ();
		Sugarism.CmdSE se = new Sugarism.CmdSE ();
		Sugarism.CmdSwitch sw = new Sugarism.CmdSwitch ();
		Sugarism.CmdTargetAppear ta = new Sugarism.CmdTargetAppear ();
		Sugarism.CmdText t = new Sugarism.CmdText ();
	}
}
