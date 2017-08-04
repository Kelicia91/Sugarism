using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ActionButton : MonoBehaviour
{
    /********* Editor Interface *********/
    // prefabs
    public Image Image;
    public Text MoneyText;
    public Text NameText;

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
        Action action = Manager.Instance.DT.Action[_actionId];

        setActionIcon(action.icon);

        string actionName = getActionName();
        setNameText(actionName);

        int money = action.money;
        setMoneyText(money);
    }

    private void setActionIcon(Sprite s)
    {
        if (null == Image)
        {
            Log.Error("not found image for action icon");
            return;
        }

        Image.sprite = s;
    }

    private void setNameText(string s)
    {
        if (null == NameText)
        {
            Log.Error("not found name text");
            return;
        }

        NameText.text = s;
    }

    private void setMoneyText(int money)
    {
        if (null == MoneyText)
        {
            Log.Error("not found money text");
            return;
        }

        string s = string.Format("{0}/{1}", money, Def.DAY_UNIT);
        MoneyText.text = s;
    }

    private string getActionName()
    {
        if (_actionId < 0)
            return string.Empty;
        else if (_actionId >= Manager.Instance.DT.Action.Count)
            return string.Empty;
        else
            return Manager.Instance.DT.Action[_actionId].name;
    }

    private void onClick()
    {
        int index = Manager.Instance.UI.SchedulePanel.SelectedScheduleIndex;
        Manager.Instance.Object.NurtureMode.Schedule.Insert(index, _actionId);
    }
}
