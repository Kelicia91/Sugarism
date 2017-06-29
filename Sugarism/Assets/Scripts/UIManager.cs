using System.Collections;
using System.Collections.Generic;
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
    public GameObject PrefSelectTargetPanel;
    public GameObject PrefStoryPanel;
    public GameObject PrefStatePanel;
    public GameObject PrefBoardGamePanel;
    public GameObject PrefCombatPanel;


    /********* Game Interface *********/
    private Canvas _canvas = null;

    private MainPanel _mainPanel = null;
    public MainPanel MainPanel { get { return _mainPanel; } }

    private SchedulePanel _schedulePanel = null;
    public SchedulePanel SchedulePanel { get { return _schedulePanel; } }

    private RunSchedulePanel _runSchedulePanel = null;
    public RunSchedulePanel RunSchedulePanel { get { return _runSchedulePanel; } }

    private SelectTargetPanel _selectTargetPanel = null;
    public SelectTargetPanel SelectTargetPanel { get { return _selectTargetPanel; } }

    private StoryPanel _storyPanel = null;
    public StoryPanel StoryPanel { get { return _storyPanel; } }

    private StatePanel _statePanel = null;
    public StatePanel StatePanel { get { return _statePanel; } }

    private BoardGamePanel _boardGamePanel = null;
    public BoardGamePanel BoardGamePanel { get { return _boardGamePanel; } }

    private CombatPanel _combatPanel = null;
    public CombatPanel CombatPanel { get { return _combatPanel; } }


    // Use this for initialization
    void Start ()
    {
        create();

        SchedulePanel.Hide();
        RunSchedulePanel.Hide();
        SelectTargetPanel.Hide();
        StoryPanel.Hide();
        StatePanel.Hide();
        BoardGamePanel.Hide();
        CombatPanel.Hide();

        MainPanel.Show();

        Nurture.Mode nurtureMode = Manager.Instance.Object.NurtureMode;
        nurtureMode.Schedule.StartEvent.Attach(onScheduleStart);
        nurtureMode.Schedule.EndEvent.Attach(onScheduleEnd);
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

        o = Instantiate(PrefSelectTargetPanel);
        _selectTargetPanel = o.GetComponent<SelectTargetPanel>();
        _selectTargetPanel.transform.SetParent(_canvas.transform, false);

        o = Instantiate(PrefStoryPanel);
        _storyPanel = o.GetComponent<StoryPanel>();
        _storyPanel.transform.SetParent(_canvas.transform, false);

        o = Instantiate(PrefStatePanel);
        _statePanel = o.GetComponent<StatePanel>();
        _statePanel.transform.SetParent(_canvas.transform, false);

        o = Instantiate(PrefBoardGamePanel);
        _boardGamePanel = o.GetComponent<BoardGamePanel>();
        _boardGamePanel.transform.SetParent(_canvas.transform, false);

        o = Instantiate(PrefCombatPanel);
        _combatPanel = o.GetComponent<CombatPanel>();
        _combatPanel.transform.SetParent(_canvas.transform, false);
    }

    
    private void onScheduleStart()
    {
        SchedulePanel.Hide();
        RunSchedulePanel.Show();
    }

    private void onScheduleEnd()
    {
        RunSchedulePanel.Hide();
        MainPanel.Show();
    }
}
