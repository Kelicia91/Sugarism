using UnityEngine;


// prohibit from abusing Singleton.
// @IMPORTANT : prohibit from re-define constuctor because of inheriting MonoBehaviour.
public class LobbyManager : MonoBehaviour
{
    private static LobbyManager _instance = null;
    public static LobbyManager Instance { get { return _instance; } }


    /********* Editor Interface *********/
    // Prefabs
    [SerializeField]
    private DataTableCollection PrefDataTableCollection = null;
    [SerializeField]
    private PlayerInitProperty PrefPlayerInitProperty = null;
    [SerializeField]
    private LobbyUIManager PrefLobbyUIManager = null;

    /********* Game Interface *********/
    private DataTableCollection _dataTableCollection = null;
    public DataTableCollection DT { get { return _dataTableCollection; } }

    private PlayerInitProperty _playerInitProperty = null;
    public PlayerInitProperty PlayerInitProperty { get { return _playerInitProperty; } }

    private LobbyUIManager _ui = null;
    public LobbyUIManager UI { get { return _ui; } }


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
        _playerInitProperty = Instantiate(PrefPlayerInitProperty);        
        _ui = Instantiate(PrefLobbyUIManager);
    }

}   // class
