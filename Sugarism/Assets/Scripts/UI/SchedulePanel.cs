using System.Collections;
using System.Collections.Generic;
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
    private int _selectedScheduleIndex = -1;
    public int SelectedScheduleIndex
    {
        get { return _selectedScheduleIndex; }
        set { _selectedScheduleIndex = value; }
    }


    // Initialization Call Order : Awake(once) -> OnEnable -> Start(once)
    void Awake()
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

    private string getActionName(int actionId)
    {
        if (actionId < 0)
            return string.Empty;
        else if (actionId >= Manager.Instance.DTAction.Count)
            return string.Empty;
        else
            return Manager.Instance.DTAction[actionId].name; 
    }
    
    private void onClickBackButton()
    {
        Hide(); // SchedulePanel

        Manager.Instance.UI.BackButton.gameObject.SetActive(true);
        Manager.Instance.UI.CalendarPanel.Show();
        Manager.Instance.UI.ProfilePanel.Show();
        Manager.Instance.UI.CmdPanel.Show();
    }
}
