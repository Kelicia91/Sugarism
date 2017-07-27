using UnityEngine;
using UnityEngine.UI;


public class ZodiacToggle : MonoBehaviour
{
    //
    private EZodiac _zodiac = EZodiac.MAX;
    public EZodiac Zodiac
    {
        get { return _zodiac; }
        set
        {
            _zodiac = value;

            int id = (int)_zodiac;
            var zodiac = LobbyManager.Instance.DT.Zodiac[id];
            _toggle.SetImage(zodiac.image);
            _toggle.SetText(zodiac.name);
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

        LobbyManager.Instance.UI.ZodiacPanel.SelectedZodiac = Zodiac;
    }
}
