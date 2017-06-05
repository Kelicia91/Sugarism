﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainPanel : Panel
{
    /********* Editor Interface *********/
    // prefabs
    public GameObject PrefBackButton;
    public GameObject PrefCurrencyPanel;
    public GameObject PrefCalendarPanel;
    public GameObject PrefProfilePanel;
    public GameObject PrefCmdPanel;


    /********* Game Interface *********/
    private Button _backButton = null;

    private CurrencyPanel _currencyPanel = null;
    public CurrencyPanel CurrencyPanel { get { return _currencyPanel; } }

    private CalendarPanel _calendarPanel = null;
    public CalendarPanel CalendarPanel { get { return _calendarPanel; } }

    private ProfilePanel _profilePanel = null;
    public ProfilePanel ProfilePanel { get { return _profilePanel; } }

    private CmdPanel _cmdPanel = null;
    public CmdPanel CmdPanel { get { return _cmdPanel; } }


    // Use this for initialization
    void Start ()
    {
        create();

        _backButton.onClick.AddListener(onClick);

        CalendarPanel.Show();
        ProfilePanel.Show();
        CmdPanel.Show();
    }

    private void create()
    {
        GameObject o = null;

        o = Instantiate(PrefBackButton);
        _backButton = o.GetComponent<Button>();
        _backButton.transform.SetParent(transform, false);

        o = Instantiate(PrefCurrencyPanel);
        _currencyPanel = o.GetComponent<CurrencyPanel>();
        _currencyPanel.transform.SetParent(transform, false);

        o = Instantiate(PrefCalendarPanel);
        _calendarPanel = o.GetComponent<CalendarPanel>();
        _calendarPanel.transform.SetParent(transform, false);

        o = Instantiate(PrefProfilePanel);
        _profilePanel = o.GetComponent<ProfilePanel>();
        _profilePanel.transform.SetParent(transform, false);

        o = Instantiate(PrefCmdPanel);
        _cmdPanel = o.GetComponent<CmdPanel>();
        _cmdPanel.transform.SetParent(transform, false);
    }

    private void onClick()
    {
        Log.Debug("click. back button");
    }
}