using UnityEngine;
using UnityEngine.UI;


public class ActionPanel : Panel
{
    /********* Editor Interface *********/
    // prefabs
    public Image Image;
    public Text NameText;
    public Text MoneyText;
    
    
    public void SetActionId(int actionId)
    {
        Action action = Manager.Instance.DT.Action[actionId];

        setActionIcon(action.icon);
        setNameText(action.name);
        setMoneyText(action.money);
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

        string s = money.ToString();
        MoneyText.text = s;
    }
}
