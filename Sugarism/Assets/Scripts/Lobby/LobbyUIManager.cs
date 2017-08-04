using UnityEngine;


public class LobbyUIManager : MonoBehaviour
{
    /********* Editor Interface *********/
    // prefabs
    [SerializeField]
    private GameObject PrefEventSystem = null;
    [SerializeField]
    private Canvas PrefCanvas = null;
    [SerializeField]
    private LobbyPanel PrefLobbyPanel = null;
    [SerializeField]
    private FormPanel PrefFormPanel = null;
    [SerializeField]
    private ConstitutionPanel PrefConstitutionPanel = null;
    [SerializeField]
    private ZodiacPanel PrefZodiacPanel = null;
    [SerializeField]
    private AlbumPanel PrefAlbumPanel = null;


    /********* Game Interface *********/
    private Canvas _canvas = null;
    public Canvas Canvas { get { return _canvas; } }

    private LobbyPanel _lobbyPanel = null;
    public LobbyPanel LobbyPanel { get { return _lobbyPanel; } }

    private FormPanel _formPanel = null;
    public FormPanel FormPanel { get { return _formPanel; } }

    private ConstitutionPanel _constitutionPanel = null;
    public ConstitutionPanel ConstitutionPanel { get { return _constitutionPanel; } }

    private ZodiacPanel _zodiacPanel = null;
    public ZodiacPanel ZodiacPanel { get { return _zodiacPanel; } }

    private AlbumPanel _albumPanel = null;
    public AlbumPanel AlbumPanel { get { return _albumPanel; } }


    //
    void Awake()
    {
        create();

        HideAllPanel();
    }

    //
    void Start()
    {
        LobbyPanel.Show();
    }

    //
    public void HideAllPanel()
    {
        AlbumPanel.Hide();
        ZodiacPanel.Hide();
        ConstitutionPanel.Hide();
        FormPanel.Hide();
        LobbyPanel.Hide();
    }

    private void create()
    {
        Instantiate(PrefEventSystem);

        _canvas = Instantiate(PrefCanvas);

        _lobbyPanel = Instantiate(PrefLobbyPanel);
        _lobbyPanel.transform.SetParent(_canvas.transform, false);

        _formPanel = Instantiate(PrefFormPanel);
        _formPanel.transform.SetParent(_canvas.transform, false);

        _constitutionPanel = Instantiate(PrefConstitutionPanel);
        _constitutionPanel.transform.SetParent(_canvas.transform, false);

        _zodiacPanel = Instantiate(PrefZodiacPanel);
        _zodiacPanel.transform.SetParent(_canvas.transform, false);

        _albumPanel = Instantiate(PrefAlbumPanel);
        _albumPanel.transform.SetParent(_canvas.transform, false);
    }
}
