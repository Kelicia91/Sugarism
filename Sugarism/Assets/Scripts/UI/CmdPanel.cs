using System;
using UnityEngine;
using UnityEngine.UI;


public class CmdPanel : Panel
{
    /********* Editor Interface *********/
    // values
    public int SPACE_Y_BETWEEN_BUTTON = 0;
    public Vector2 SPACE_FROM_ANCHOR = new Vector2(0.0f, 20.0f);
    // prefabs
    public Button PrefCmdButton;

    //
    private enum ECmdType
    {
        SCHEDULE = 0,
        STORY,
        STATE,
        TEST_BOARD_GAME
    }

    private Button[] _cmdButtonArray = null;

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

            case ECmdType.STORY:
                return Def.CMD_GO_OUT_NAME;

            case ECmdType.STATE:
                return Def.CMD_STATE_NAME;

            case ECmdType.TEST_BOARD_GAME:
                return Def.CMD_TEST_BOARD_GAME;

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

            case ECmdType.STORY:
                return onClickStoryButton;

            case ECmdType.STATE:
                return onClickStateButton;

            case ECmdType.TEST_BOARD_GAME:
                return onClickTestBoardGameButton;

            default:
                return null;
        }
    }

    
    private void onClickScheduleButton()
    {
        Manager.Instance.UI.MainPanel.Hide();
        Manager.Instance.UI.SchedulePanel.Show();
    }

    private void onClickStoryButton()
    {
        Manager.Instance.UI.MainPanel.Hide();
        Manager.Instance.UI.SelectTargetPanel.Show();
    }

    private void onClickStateButton()
    {
        Manager.Instance.UI.MainPanel.Hide();
        Manager.Instance.UI.StatePanel.Show();
    }

    private void onClickTestBoardGameButton()
    {
        Manager.Instance.UI.MainPanel.Hide();

        int opponentPlayerId = 1;  // test~~~~~
        Manager.Instance.Object.BoardGameMode.Start(opponentPlayerId);
    }
}
