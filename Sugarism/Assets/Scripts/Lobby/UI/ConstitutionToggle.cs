using UnityEngine;
using UnityEngine.UI;


public class ConstitutionToggle : MonoBehaviour
{
    //
    private EConstitution _constitution = EConstitution.MAX;
    public EConstitution Constitution
    {
        get { return _constitution; }
        set
        {
            _constitution = value;

            int id = (int)_constitution;
            var constitution = LobbyManager.Instance.DT.Constitution[id];
            _toggle.SetImage(constitution.sprite);
            _toggle.SetText(constitution.name);
            _toggle.SetColor(constitution.color);
        }
    }

    //
    private CustomToggle _toggle = null;
    public Toggle Component { get { return _toggle.Component; } }

    //
    void Awake()
    {
        _toggle = GetComponent<CustomToggle>();
    }

    void Start()
    {
        Component.onValueChanged.AddListener(onValueChanged);
    }

    //
    private void onValueChanged(bool isOn)
    {
        if (false == isOn)
            return;

        LobbyManager.Instance.UI.ConstitutionPanel.SelectedConstitution = Constitution;
    }
}
