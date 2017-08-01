using UnityEngine;
using UnityEngine.UI;


public class MainPanel : Panel
{
    /********* Editor Interface *********/
    // prefabs
    public GameObject PrefMainCharacterPanel;
    public GameObject PrefBackButton;
    public GameObject PrefCalendarPanel;
    public GameObject PrefProfilePanel;
    public GameObject PrefCmdPanel;


    /********* Game Interface *********/
    private MainCharacterPanel _mainCharacterPanel = null;
    private Button _backButton = null;

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

    public override void Show()
    {
        CurrencyPanel c = Manager.Instance.UI.CurrencyPanel;
        c.Hide();
        c.ActPowePanel.SetCharge(true);
        c.ActPowePanel.Show();
        c.GoldPanel.SetCharge(true);
        c.GoldPanel.Show();
        c.MoneyPanel.SetCharge(true);
        c.MoneyPanel.Show();
        c.Show();

        base.Show();
    }

    private void create()
    {
        GameObject o = null;

        o = Instantiate(PrefMainCharacterPanel);
        _mainCharacterPanel = o.GetComponent<MainCharacterPanel>();
        _mainCharacterPanel.transform.SetParent(transform, false);

        o = Instantiate(PrefBackButton);
        _backButton = o.GetComponent<Button>();
        _backButton.transform.SetParent(transform, false);

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
        // @todo: 팝업(타이틀로 돌아갈까요? y/n)
        Manager.Instance.Stop();
    }
}
