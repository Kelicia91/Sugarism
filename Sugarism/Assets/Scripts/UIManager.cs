using UnityEngine;


public class UIManager : MonoBehaviour
{
    /********* Editor Interface *********/
    // prefabs
    public GameObject PrefEventSystem;
    public GameObject PrefCanvas;

    public GameObject PrefMainPanel;
    public GameObject PrefSchedulePanel;
    public GameObject PrefRunSchedulePanel;
    public GameObject PrefWardrobePanel;
    public GameObject PrefStatePanel;
    public GameObject PrefCurrencyPanel;
    public GameObject PrefFeelingCheckPanel;
    public GameObject PrefPopupPanel;
    public GameObject PrefSelectTargetPanel;
    public GameObject PrefStoryPanel;    
    public GameObject PrefBoardGamePanel;
    public GameObject PrefCombatPanel;


    /********* Game Interface *********/
    private Canvas _canvas = null;
    public Canvas Canvas { get { return _canvas; } }

    private MainPanel _mainPanel = null;
    public MainPanel MainPanel { get { return _mainPanel; } }

    private SchedulePanel _schedulePanel = null;
    public SchedulePanel SchedulePanel { get { return _schedulePanel; } }

    private RunSchedulePanel _runSchedulePanel = null;
    public RunSchedulePanel RunSchedulePanel { get { return _runSchedulePanel; } }

    private WardrobePanel _wardrobePanel = null;
    public WardrobePanel WardrobePanel { get { return _wardrobePanel; } }

    private StatePanel _statePanel = null;
    public StatePanel StatePanel { get { return _statePanel; } }

    private CurrencyPanel _currencyPanel = null;
    public CurrencyPanel CurrencyPanel { get { return _currencyPanel; } }

    private FeelingCheckPanel _feelingCheckPanel = null;
    public FeelingCheckPanel FeelingCheckPanel { get { return _feelingCheckPanel; } }

    private PopupPanel _popupPanel = null;
    public PopupPanel Popup { get { return _popupPanel; } }

    private SelectTargetPanel _selectTargetPanel = null;
    public SelectTargetPanel SelectTargetPanel { get { return _selectTargetPanel; } }

    private StoryPanel _storyPanel = null;
    public StoryPanel StoryPanel { get { return _storyPanel; } }

    private BoardGamePanel _boardGamePanel = null;
    public BoardGamePanel BoardGamePanel { get { return _boardGamePanel; } }

    private CombatPanel _combatPanel = null;
    public CombatPanel CombatPanel { get { return _combatPanel; } }


    // Use this for initialization
    void Awake()
    {
        create();

        MainPanel.Hide();
        SchedulePanel.Hide();
        RunSchedulePanel.Hide();
        WardrobePanel.Hide();
        StatePanel.Hide();
        CurrencyPanel.Hide();
        FeelingCheckPanel.Hide();
        Popup.Hide();
        SelectTargetPanel.Hide();
        StoryPanel.Hide();
        BoardGamePanel.Hide();
        CombatPanel.Hide();
    }

    void Start()
    {
        Popup.Show();
        MainPanel.Show();
    }


    private void create()
    {
        Instantiate(PrefEventSystem);

        GameObject o = null;

        o = Instantiate(PrefCanvas);
        _canvas = o.GetComponent<Canvas>();

        o = Instantiate(PrefMainPanel);
        _mainPanel = o.GetComponent<MainPanel>();
        _mainPanel.transform.SetParent(_canvas.transform, false);

        o = Instantiate(PrefSchedulePanel);
        _schedulePanel = o.GetComponent<SchedulePanel>();
        _schedulePanel.transform.SetParent(_canvas.transform, false);

        o = Instantiate(PrefRunSchedulePanel);
        _runSchedulePanel = o.GetComponent<RunSchedulePanel>();
        _runSchedulePanel.transform.SetParent(_canvas.transform, false);

        o = Instantiate(PrefWardrobePanel);
        _wardrobePanel = o.GetComponent<WardrobePanel>();
        _wardrobePanel.transform.SetParent(_canvas.transform, false);

        o = Instantiate(PrefStatePanel);
        _statePanel = o.GetComponent<StatePanel>();
        _statePanel.transform.SetParent(_canvas.transform, false);

        o = Instantiate(PrefCurrencyPanel);
        _currencyPanel = o.GetComponent<CurrencyPanel>();
        _currencyPanel.transform.SetParent(_canvas.transform, false);

        o = Instantiate(PrefFeelingCheckPanel);
        _feelingCheckPanel = o.GetComponent<FeelingCheckPanel>();
        _feelingCheckPanel.transform.SetParent(_canvas.transform, false);

        o = Instantiate(PrefPopupPanel);
        _popupPanel = o.GetComponent<PopupPanel>();
        _popupPanel.transform.SetParent(_canvas.transform, false);

        o = Instantiate(PrefSelectTargetPanel);
        _selectTargetPanel = o.GetComponent<SelectTargetPanel>();
        _selectTargetPanel.transform.SetParent(_canvas.transform, false);

        o = Instantiate(PrefStoryPanel);
        _storyPanel = o.GetComponent<StoryPanel>();
        _storyPanel.transform.SetParent(_canvas.transform, false);

        o = Instantiate(PrefBoardGamePanel);
        _boardGamePanel = o.GetComponent<BoardGamePanel>();
        _boardGamePanel.transform.SetParent(_canvas.transform, false);

        o = Instantiate(PrefCombatPanel);
        _combatPanel = o.GetComponent<CombatPanel>();
        _combatPanel.transform.SetParent(_canvas.transform, false);
    }
}
