using System;
using UnityEngine;
using UnityEngine.UI;


public class ZodiacPanel : Panel
{
    /********* Editor Interface *********/
    // prefabs
    [SerializeField]
    private GameObject PrefBackButton = null;
    [SerializeField]
    private GameObject PrefCustomToggle = null;
    // objects
    [SerializeField]
    private Text GuideText = null;
    [SerializeField]
    private ToggleGroup ToggleGroup = null;
    [SerializeField]
    private TextButton TextButton = null;

    //
    private Button _backButton = null;

    //
    private EZodiac _selectedZodiac = Def.DEFAULT_ZODIAC;
    public EZodiac SelectedZodiac
    {
        get { return _selectedZodiac; }
        set { _selectedZodiac = value; }
    }

    //
    void Awake()
    {
        create();

        // guide
        string guide = string.Format(Def.GUIDE_SELECT, Def.ZODIAC);
        setGuide(guide);
    }

    void Start()
    {
        // back button
        _backButton.onClick.AddListener(onClickBackButton);

        // submit button
        TextButton.SetText(Def.START_GAME);
        TextButton.AddClick(onClickSubmitButton);
    }


    private void create()
    {
        GameObject o = null;

        o = Instantiate(PrefBackButton);
        o.transform.SetParent(transform, false);
        _backButton = o.GetComponent<Button>();

        Array array = Enum.GetValues(typeof(EZodiac));
        int count = array.Length - 1;   // except EZodiac.MAX
        for (int id = 0; id < count; ++id)
        {
            o = Instantiate(PrefCustomToggle);
            o.transform.SetParent(ToggleGroup.transform, false);

            EZodiac zodiac = (EZodiac)array.GetValue(id);

            ZodiacToggle zToggle = o.AddComponent<ZodiacToggle>();
            zToggle.Zodiac = zodiac;

            Toggle toggle = zToggle.Component;
            if (SelectedZodiac == zToggle.Zodiac)
                toggle.isOn = true;
            else
                toggle.isOn = false;

            toggle.group = ToggleGroup;
        }
    }

    private void setGuide(string s)
    {
        if (null == GuideText)
        {
            Log.Error("not found guide text");
            return;
        }

        GuideText.text = s;
    }

    private void onClickSubmitButton()
    {
        LobbyManager.Instance.PlayerInitProperty.Zodiac = SelectedZodiac;

        Hide();

        // @todo: 경고 팝업창 하나 띄워줘야겠네. (ex. 기존 데이터 날아가도 괜츈?)
        LobbyManager.Instance.FreshStart();
    }

    private void onClickBackButton()
    {
        Hide();
        LobbyManager.Instance.UI.ConstitutionPanel.Show();
    }
}
