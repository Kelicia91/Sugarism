using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    /********* Editor Interface *********/
    // prefabs
    public GameObject PrefEventSystem;
    public GameObject PrefCanvas;

    public GameObject PrefBackButton;
    public GameObject PrefCurrencyPanel;
    public GameObject PrefCalendarPanel;
    public GameObject PrefProfilePanel;
    public GameObject PrefCmdPanel;
    public GameObject PrefSchedulePanel;
    public GameObject PrefRunSchedulePanel;
    public GameObject PrefStatePanel;
    //public GameObject PrefSelectActionTypePanel;
    //public GameObject PrefSelectActionPanel;


    /********* Game Interface *********/
    private Canvas _canvas = null;

    private Button _backButton = null;
    public Button BackButton { get { return _backButton; } }

    private CurrencyPanel _currencyPanel = null;
    public CurrencyPanel CurrencyPanel { get { return _currencyPanel; } }

    private CalendarPanel _calendarPanel = null;
    public CalendarPanel CalendarPanel { get { return _calendarPanel; } }

    private ProfilePanel _profilePanel = null;
    public ProfilePanel ProfilePanel { get { return _profilePanel; } }

    private CmdPanel _cmdPanel = null;
    public CmdPanel CmdPanel { get { return _cmdPanel; } }

    private SchedulePanel _schedulePanel = null;
    public SchedulePanel SchedulePanel { get { return _schedulePanel; } }

    private RunSchedulePanel _runSchedulePanel = null;
    public RunSchedulePanel RunSchedulePanel { get { return _runSchedulePanel; } }

    private StatePanel _statePanel = null;
    public StatePanel StatePanel { get { return _statePanel; } }

    //private SelectActionTypePanel _selectActionTypePanel = null;
    //public SelectActionTypePanel SelectActionTypePanel { get { return _selectActionTypePanel; } }

    //private SelectActionPanel _selectActionPanel = null;
    //public SelectActionPanel SelectActionPanel { get { return _selectActionPanel; } }


    // Use this for initialization
    void Start ()
    {
        create();

        BackButton.onClick.AddListener(onClick);

        CalendarPanel.Show();
        ProfilePanel.Show();
        CmdPanel.Show();

        SchedulePanel.Hide();
        RunSchedulePanel.Hide();
        StatePanel.Hide();
        //SelectActionTypePanel.Hide();
        //SelectActionPanel.Hide();

        Manager.Instance.ScheduleStartEvent.Attach(onScheduleStart);
        Manager.Instance.ScheduleEndEvent.Attach(onScheduleEnd);
    }


    private void create()
    {
        Instantiate(PrefEventSystem);

        GameObject o = null;

        o = Instantiate(PrefCanvas);
        _canvas = o.GetComponent<Canvas>();

        o = Instantiate(PrefBackButton);
        _backButton = o.GetComponent<Button>();
        _backButton.transform.SetParent(_canvas.transform, false);

        o = Instantiate(PrefCurrencyPanel);
        _currencyPanel = o.GetComponent<CurrencyPanel>();
        _currencyPanel.transform.SetParent(_canvas.transform, false);

        o = Instantiate(PrefCalendarPanel);
        _calendarPanel = o.GetComponent<CalendarPanel>();
        _calendarPanel.transform.SetParent(_canvas.transform, false);

        o = Instantiate(PrefProfilePanel);
        _profilePanel = o.GetComponent<ProfilePanel>();
        _profilePanel.transform.SetParent(_canvas.transform, false);

        o = Instantiate(PrefCmdPanel);
        _cmdPanel = o.GetComponent<CmdPanel>();
        _cmdPanel.transform.SetParent(_canvas.transform, false);

        o = Instantiate(PrefSchedulePanel);
        _schedulePanel = o.GetComponent<SchedulePanel>();
        _schedulePanel.transform.SetParent(_canvas.transform, false);

        o = Instantiate(PrefRunSchedulePanel);
        _runSchedulePanel = o.GetComponent<RunSchedulePanel>();
        _runSchedulePanel.transform.SetParent(_canvas.transform, false);

        o = Instantiate(PrefStatePanel);
        _statePanel = o.GetComponent<StatePanel>();
        _statePanel.transform.SetParent(_canvas.transform, false);

        //o = Instantiate(PrefSelectActionPanel);
        //_selectActionPanel = o.GetComponent<SelectActionPanel>();
        //_selectActionPanel.transform.SetParent(_canvas.transform, false);

        //o = Instantiate(PrefSelectActionTypePanel);
        //_selectActionTypePanel = o.GetComponent<SelectActionTypePanel>();
        //_selectActionTypePanel.transform.SetParent(_canvas.transform, false);
    }


    // BACK button
    private void onClick()
    {
        Log.Debug("click. back button");
    }

    private void onScheduleStart()
    {
        CurrencyPanel.Hide();   //@todo: 각 화면 정리하자
        SchedulePanel.Hide();
        RunSchedulePanel.Show();
    }

    private void onScheduleEnd()
    {
        BackButton.gameObject.SetActive(true);
        CurrencyPanel.Show();
        CalendarPanel.Show();
        ProfilePanel.Show();
        CmdPanel.Show();

        SchedulePanel.Hide();
        RunSchedulePanel.Hide();
    }
}
