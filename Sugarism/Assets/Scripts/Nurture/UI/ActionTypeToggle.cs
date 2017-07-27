using UnityEngine;
using UnityEngine.UI;


public class ActionTypeToggle : MonoBehaviour
{
    /********* Editor Interface *********/
    // colors
    public Color UnSelectionBgColor = Color.black;
    public Color UnSelectionTextColor = Color.white;
    public Color SelectionBgColor = Color.white;
    public Color SelectionTextColor = Color.black;
    // prefabs
    public Text Text;

    //
    private const EActionType DEFAULT_ENABLE_ACTION_TYPE = EActionType.PARTTIME;

    //
    private Toggle _toggle = null;
    private EActionType _actionType = EActionType.MAX;


    //
    void Awake()
    {
        _toggle = GetComponent<Toggle>();
        if (null == _toggle)
        {
            Log.Error("Not found toggle");
            return;
        }
        
        _toggle.onValueChanged.AddListener(onValueChanged);
	}

    void OnEnable()
    {
        onEnable();
    }

    public void Set(ToggleGroup toggleGroup)
    {
        _toggle.group = toggleGroup;
    }

    // @note : called after OnEnable()
    public void SetActionType(EActionType actionType)
    {
        _actionType = actionType;

        string actionTypeName = getActionTypeName();
        setText(actionTypeName);
        
        onEnable();
    }

    private void onEnable()
    {
        if (DEFAULT_ENABLE_ACTION_TYPE == _actionType)
            _toggle.isOn = true;
        else
            _toggle.isOn = false;
    }

    private void setText(string s)
    {
        if (null == Text)
        {
            Log.Error("Not Found Text");
            return;
        }

        Text.text = s;
    }

    private void setTextColor(Color c)
    {
        if (null == Text)
        {
            Log.Error("Not Found Text");
            return;
        }

        Text.color = c;
    }

    private string getActionTypeName()
    {
        if (EActionType.MAX != _actionType)
        {
            int actionTypeId = (int)_actionType;
            return Manager.Instance.DT.ActionType[actionTypeId].name;
        }
        else
        {
            return Def.BACK;
        }
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

        ActionListPanel p = Manager.Instance.UI.SchedulePanel.ActionListPanel;
        p.Show(_actionType);
    }

    private void onUnchecked()
    {
        setBackgroundColor(UnSelectionBgColor);
        setTextColor(UnSelectionTextColor);
    }

    private void setBackgroundColor(Color c)
    {
        Graphic g = _toggle.targetGraphic;
        g.color = c;
    }
}
