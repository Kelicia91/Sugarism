using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SelectActionButton : MonoBehaviour
{
    /********* Editor Interface *********/
    // prefabs
    public Text Text;

    //
    private Button _btn = null;
    private int _actionId = -1;


    // Use this for initialization
    void Start()
    {
        _btn = GetComponent<Button>();
        if (null == _btn)
        {
            Log.Error("Not found Button");
            return;
        }

        _btn.onClick.AddListener(onClick);
    }

    public void SetActionId(int actionId)
    {
        _actionId = actionId;

        string actionName = getActionName();
        setText(actionName);
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

    private string getActionName()
    {
        if (_actionId < 0)
            return Def.BACK;
        else if (_actionId >= Manager.Instance.DTAction.Count)
            return Def.BACK;
        else
            return Manager.Instance.DTAction[_actionId].name;
    }

    private void onClick()
    {
        if (_actionId >= 0)
        {
            //int index = Manager.Instance.UI.SchedulePanel.SelectedActionIndex;
            //Manager.Instance.Object.Schedule.Insert(index, _actionId);
        }
        else
        {
            // BACK button
            //Manager.Instance.UI.SelectActionPanel.Hide();
            //Manager.Instance.UI.SelectActionTypePanel.Show();
        }
    }
}
