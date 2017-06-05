using System;
using UnityEngine;
using UnityEngine.UI;


public class CmdPanel : Panel
{
    /********* Editor Interface *********/
    // values
    public int SPACE_Y_BETWEEN_BUTTON;
    public Vector2 SPACE_FROM_ANCHOR;
    // prefabs
    public Button PrefCmdButton;

    //
    private enum ECmdType
    {
        SCHEDULE = 0,
        GOOUT,
        STATE
    }

    private Button[] _cmdButtonArray;

    void Start()
    {
        if (null == PrefCmdButton)
        {
            Log.Error("not found cmd button");
            return;
        }

        Array cmdArray = Enum.GetValues(typeof(ECmdType));
        int numOfCmd = cmdArray.Length;

        RectTransform rectTransform = PrefCmdButton.GetComponent<RectTransform>();
        float btnHeight = rectTransform.rect.height;
        float deltaY = btnHeight + SPACE_Y_BETWEEN_BUTTON;

        float posY = numOfCmd * deltaY + SPACE_FROM_ANCHOR.y;   // top to bottom

        _cmdButtonArray = new Button[numOfCmd];
        for (int i = 0; i < numOfCmd; ++i)
        {
            Button btn = Instantiate(PrefCmdButton);
            btn.transform.SetParent(transform, false);

            RectTransform rect = btn.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(SPACE_FROM_ANCHOR.x, posY);
            posY -= deltaY;

            ECmdType cmdType = (ECmdType)cmdArray.GetValue(i);            
            Text btnText = btn.GetComponentInChildren<Text>();
            btnText.text = getName(cmdType);

            btn.onClick.AddListener(getHandler(cmdType));

            _cmdButtonArray[i] = btn;
        }
    }

    private string getName(ECmdType cmdType)
    {
        switch (cmdType)
        {
            case ECmdType.SCHEDULE:
                return Def.CMD_SCHEDULE_NAME;

            case ECmdType.GOOUT:
                return Def.CMD_GO_OUT_NAME;

            case ECmdType.STATE:
                return Def.CMD_STATE_NAME;

            default:
                return string.Empty;
        }
    }

    private UnityEngine.Events.UnityAction getHandler(ECmdType cmdType)
    {
        switch (cmdType)
        {
            case ECmdType.SCHEDULE:
                return onClickScheduleButton;

            case ECmdType.GOOUT:
                return onClickGoOutButton;

            case ECmdType.STATE:
                return onClickStateButton;

            default:
                return null;
        }
    }

    
    private void onClickScheduleButton()
    {
        Manager.Instance.UI.MainPanel.Hide();
        Manager.Instance.UI.SchedulePanel.Show();
    }

    private void onClickGoOutButton()
    {
        Manager.Instance.UI.MainPanel.Hide();
        Manager.Instance.UI.SelectTargetPanel.Show();
    }

    private void onClickStateButton()
    {
        Manager.Instance.UI.MainPanel.Hide();
        Manager.Instance.UI.StatePanel.Show();
    }
}
