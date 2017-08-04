using UnityEngine;
using UnityEngine.UI;


public class AlbumTypeToggle : MonoBehaviour
{
    /********* Editor Interface *********/
    // colors
    [SerializeField]
    private Color UnSelectionBgColor = Color.black;
    [SerializeField]
    private Color UnSelectionTextColor = Color.white;
    [SerializeField]
    private Color SelectionBgColor = Color.white;
    [SerializeField]
    private Color SelectionTextColor = Color.black;

    // objects
    [SerializeField]
    private Text Text = null;
    
    //
    private Toggle _toggle = null;
    private UnitAlbumPanel _parentPanel = null;
    private AlbumController.EAlbumType _type = AlbumController.EAlbumType.MAX;


    //
    void Awake()
    {
        _toggle = GetComponent<Toggle>();
        _toggle.onValueChanged.AddListener(onValueChanged);
    }

    void OnEnable()
    {
        onEnable();
    }


    private void onEnable()
    {
        if (AlbumController.EAlbumType.PICTURE == _type)
            _toggle.isOn = true;
        else if (AlbumController.EAlbumType.NURTURE_ENDING == _type)
            _toggle.isOn = true;
        else
            _toggle.isOn = false;
    }


    public void Set(ToggleGroup toggleGroup)
    {
        _toggle.group = toggleGroup;
    }

    public void Set(UnitAlbumPanel parentPanel, AlbumController.EAlbumType albumType)
    {
        _parentPanel = parentPanel;
        _type = albumType;

        Text.text = AlbumController.GetAlbumTypeName(_type);

        onEnable();
    }


    // event handler for setting toggle.isOn
    private void onValueChanged(bool isOn)
    {
        if (isOn)
            onChecked();
        else
            onUnchecked();
    }

    private void onChecked()
    {
        setBackgroundColor(SelectionBgColor);
        setTextColor(SelectionTextColor);

        _parentPanel.ShowCGList(_type);
    }

    private void onUnchecked()
    {
        setBackgroundColor(UnSelectionBgColor);
        setTextColor(UnSelectionTextColor);
    }

    //
    private void setTextColor(Color c)
    {
        Text.color = c;
    }

    private void setBackgroundColor(Color c)
    {
        Graphic g = _toggle.targetGraphic;
        g.color = c;
    }
}
