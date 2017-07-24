using UnityEngine;
using UnityEngine.UI;


public class SchedulePanel : Panel
{
    /********* Editor Interface *********/
    // prefabs
    public GameObject PrefBackButton;
    public GameObject PrefStatPanel;
    public GameObject StatsPanel;
    public ActionListPanel ActionListPanel;

    //
    private int _selectedScheduleIndex = 0;
    public int SelectedScheduleIndex
    {
        get { return _selectedScheduleIndex; }
        set { _selectedScheduleIndex = value; }
    }


    // Initialization Call Order : Awake(once) -> OnEnable -> Start(once)
    void Awake()
    {
        create();
        
        Manager.Instance.Object.NurtureMode.Schedule.StartEvent.Attach(onScheduleStart);
    }

    private void create()
    {
        if (null != PrefBackButton)
        {
            GameObject o = Instantiate(PrefBackButton);
            o.transform.SetParent(transform, false);

            Button BackButton = o.GetComponent<Button>();
            BackButton.onClick.AddListener(onClickBackButton);
        }
        else
        {
            Log.Error("not found prefab back button");
        }

        if (null != PrefStatPanel)
        {
            GameObject o = Instantiate(PrefStatPanel);
            o.transform.SetParent(StatsPanel.transform, false);

            StatPanel sp = o.GetComponent<StatPanel>();
            sp.Set(EStat.STRESS);
        }
        else
        {
            Log.Error("not found prefab stat panel");
        }
    }

    private void onClickBackButton()
    {
        Hide();
    }

    private void onScheduleStart()
    {
        Hide();
    }
}
