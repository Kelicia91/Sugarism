using UnityEngine;


public class LobbyUIManager : MonoBehaviour
{
    /********* Editor Interface *********/
    // prefabs
    [SerializeField]
    private GameObject PrefEventSystem = null;
    [SerializeField]
    private GameObject PrefCanvas = null;
    [SerializeField]
    private GameObject PrefLobbyPanel = null;
    [SerializeField]
    private GameObject PrefFormPanel = null;
    [SerializeField]
    private GameObject PrefConstitutionPanel = null;
    [SerializeField]
    private GameObject PrefZodiacPanel = null;


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


    //
    void Awake()
    {
        create();
	}

    //
    void Start()
    {
        ZodiacPanel.Hide();
        ConstitutionPanel.Hide();
        FormPanel.Hide();

        LobbyPanel.Show();
    }

    private void create()
    {
        Instantiate(PrefEventSystem);

        GameObject o = null;

        o = Instantiate(PrefCanvas);
        _canvas = o.GetComponent<Canvas>();

        o = Instantiate(PrefLobbyPanel);
        o.transform.SetParent(_canvas.transform, false);
        _lobbyPanel = o.GetComponent<LobbyPanel>();

        o = Instantiate(PrefFormPanel);
        o.transform.SetParent(_canvas.transform, false);
        _formPanel = o.GetComponent<FormPanel>();

        o = Instantiate(PrefConstitutionPanel);
        o.transform.SetParent(_canvas.transform, false);
        _constitutionPanel = o.GetComponent<ConstitutionPanel>();

        o = Instantiate(PrefZodiacPanel);
        o.transform.SetParent(_canvas.transform, false);
        _zodiacPanel = o.GetComponent<ZodiacPanel>();
    }
}
