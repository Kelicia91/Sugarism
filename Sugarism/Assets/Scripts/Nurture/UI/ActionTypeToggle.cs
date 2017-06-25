using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ActionTypeToggle : MonoBehaviour
{
    /********* Editor Interface *********/
    // colors
    public Color SelectionColor;
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

    private string getActionTypeName()
    {
        if (EActionType.MAX != _actionType)
        {
            int actionTypeId = (int)_actionType;
            return Manager.Instance.DTActionType[actionTypeId].name;
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
        Graphic g = _toggle.targetGraphic;
        g.color = SelectionColor;

        ActionListPanel p = Manager.Instance.UI.SchedulePanel.ActionListPanel;
        p.Show(_actionType);
    }

    private void onUnchecked()
    {
        Graphic g = _toggle.targetGraphic;
        g.color = _toggle.colors.normalColor;
    }
}
