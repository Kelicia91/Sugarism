using System;
using UnityEngine;
using UnityEngine.UI;


public class ConstitutionPanel : Panel
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
    private EConstitution _selectedConstitution = Def.DEFAULT_CONSTITUTION;
    public EConstitution SelectedConstitution
    {
        get { return _selectedConstitution; }
        set { _selectedConstitution = value; }
    }

    //
    void Awake()
    {
        create();

        // guide
        string guide = string.Format(Def.GUIDE_SELECT, Def.CONSTITUTION);
        setGuide(guide);
    }

    void Start()
    {
        // back button
        _backButton.onClick.AddListener(onClickBackButton);

        // submit button
        TextButton.SetText(Def.NEXT);
        TextButton.AddClick(onClickSubmitButton);
    }


    private void create()
    {
        GameObject o = null;

        o = Instantiate(PrefBackButton);
        o.transform.SetParent(transform, false);
        _backButton = o.GetComponent<Button>();
        
        Array array = Enum.GetValues(typeof(EConstitution));
        int count = array.Length - 1;   // except EConstitution.MAX
        for (int id = 0; id < count; ++id)
        {
            o = Instantiate(PrefCustomToggle);
            o.transform.SetParent(ToggleGroup.transform, false);

            EConstitution constition = (EConstitution) array.GetValue(id);

            ConstitutionToggle cstToggle = o.AddComponent<ConstitutionToggle>();
            cstToggle.Constitution = constition;

            Toggle toggle = cstToggle.Component;
            if (SelectedConstitution == cstToggle.Constitution)
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
        LobbyManager.Instance.PlayerInitProperty.Constitution = SelectedConstitution;
        
        LobbyManager.Instance.UI.ZodiacPanel.Show();
    }

    private void onClickBackButton()
    {
        Hide();
    }
}
