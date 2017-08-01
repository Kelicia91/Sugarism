using UnityEngine;


// prohibit from abusing Singleton.
// @IMPORTANT : prohibit from re-define constuctor because of inheriting MonoBehaviour.
public class Manager : SceneManager
{
    private static Manager _instance = null;
    public static Manager Instance { get { return _instance; } }


    /********* Editor Interface *********/
    // Prefabs
    [SerializeField]
    private ObjectManager PrefObjectManager = null;
    [SerializeField]
    private UIManager PrefUIManager = null;
    [SerializeField]
    private TestManager PrefTestManager = null;

    /********* Game Interface *********/
    private ObjectManager _object = null;
    public ObjectManager Object { get { return _object; } }

    private UIManager _ui = null;
    public UIManager UI { get { return _ui; } }


    //
    void Awake()
    {
        _instance = this;
        Initialize();

        // manager
        _object = Instantiate(PrefObjectManager);
        _ui = Instantiate(PrefUIManager);

#if UNITY_EDITOR
        Instantiate(PrefTestManager);
#endif
    }

}   // class
 
 