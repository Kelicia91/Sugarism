using System;
using UnityEngine;
using UnityEngine.UI;


public class ZodiacPanel : Panel
{
    /********* Editor Interface *********/
    // prefabs
    public GameObject PrefBackButton;
    public GameObject PrefCustomToggle;
    // objects
    public Text GuideText;
    public ToggleGroup ToggleGroup;
    public SubmitButton SubmitButton;

    //
    private Button _backButton = null;

    //
    private EZodiac _selectedZodiac = EZodiac.RAT;
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
        SubmitButton.SetText(Def.START_GAME);
        SubmitButton.AddClick(onClickSubmitButton);
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

        // 시작하기...
        // @todo : 다음씬이냐?
    }

    private void onClickBackButton()
    {
        Hide();
        LobbyManager.Instance.UI.ConstitutionPanel.Show();
    }
}
