using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SchedulePanel : Panel
{
    /********* Editor Interface *********/
    // prefabs
    public GameObject PrefBackButton;
    public GameObject PrefCurrencyPanel;
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
    void Start()
    {
        create();
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

        if (null != PrefCurrencyPanel)
        {
            GameObject o = Instantiate(PrefCurrencyPanel);
            o.transform.SetParent(transform, false);
        }
        else
        {
            Log.Error("not found prefab currency panel");
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
        Manager.Instance.UI.MainPanel.Show();
    }
}
