using UnityEngine;


public class LobbyUIManager : MonoBehaviour
{
    /********* Editor Interface *********/
    // prefabs
    public GameObject PrefEventSystem;
    public GameObject PrefCanvas;
    public GameObject PrefFormPanel;
    public GameObject PrefConstitutionPanel;
    public GameObject PrefZodiacPanel;


    /********* Game Interface *********/
    private Canvas _canvas = null;
    public Canvas Canvas { get { return _canvas; } }

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
        ConstitutionPanel.Hide();
        ZodiacPanel.Hide();

        FormPanel.Show();
    }

    private void create()
    {
        Instantiate(PrefEventSystem);

        GameObject o = null;

        o = Instantiate(PrefCanvas);
        _canvas = o.GetComponent<Canvas>();

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
