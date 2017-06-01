using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SelectActionTypeButton : MonoBehaviour
{
    /********* Editor Interface *********/
    // prefabs
    public Text Text;

    //
    private Button _btn = null;
    private EActionType _actionType = EActionType.MAX;


    // Use this for initialization
    void Start ()
    {
        _btn = GetComponent<Button>();
        if(null == _btn)
        {
            Log.Error("Not found Button");
            return;
        }
        
        _btn.onClick.AddListener(onClick);
	}

    public void SetActionType(EActionType actionType)
    {
        _actionType = actionType;
        
        string actionTypeName = getActionTypeName();
        setText(actionTypeName);
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

    private void onClick()
    {
        //Manager.Instance.UI.SelectActionTypePanel.SelectedActionType = _actionType;
        //Manager.Instance.UI.SelectActionTypePanel.Hide();

        //if (EActionType.MAX != _actionType)
        //{
        //    Manager.Instance.UI.SelectActionPanel.Show();
        //}
        //else
        //{
        //    // BACK button
        //    Manager.Instance.UI.SchedulePanel.Hide();
        //    Manager.Instance.UI.CmdPanel.Show();
        //}
    }
}
