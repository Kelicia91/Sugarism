using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StatePanel : Panel
{
    /********* Editor Interface *********/
    // prefabs
    public GameObject PrefBackButton;
    public Text TitleText;
    public GameObject StatListPanel;
    public GameObject PrefStatPanel;


    // Use this for initialization
    void Start ()
    {
        createBackButton();
        setTitleText(Def.CMD_STATE_NAME);
        createStatPanel();
	}

    private void createBackButton()
    {
        if (null == PrefBackButton)
        {
            Log.Error("not found prefab back button");
            return;
        }

        GameObject o = Instantiate(PrefBackButton);
        o.transform.SetParent(transform, false);

        Button btn = o.GetComponent<Button>();
        btn.onClick.AddListener(onClickBackButton);
    }

    private void setTitleText(string s)
    {
        if (null == TitleText)
        {
            Log.Error("not found title text");
            return;
        }

        TitleText.text = s;
    }

    private void createStatPanel()
    {
        if (null == PrefStatPanel)
        {
            Log.Error("not found prefab stat panel");
            return;
        }

        if (null == StatListPanel)
        {
            Log.Error("not found stat list panel");
            return;
        }

        Array statTypeArray = Enum.GetValues(typeof(EStat));

        const int NUM_STAT = (int)EStat.MAX;
        for (int i = 0; i < NUM_STAT; ++i)
        {
            GameObject o = Instantiate(PrefStatPanel);
            o.transform.SetParent(StatListPanel.transform, false);

            EStat stat = (EStat)statTypeArray.GetValue(i);

            StatPanel p = o.GetComponent<StatPanel>();
            p.Set(stat);
        }
    }

    private void onClickBackButton()
    {
        Hide(); // StatePanel

        Manager.Instance.UI.BackButton.gameObject.SetActive(true);
        Manager.Instance.UI.CurrencyPanel.Show();
        Manager.Instance.UI.CalendarPanel.Show();
        Manager.Instance.UI.ProfilePanel.Show();
        Manager.Instance.UI.CmdPanel.Show();
    }
}
