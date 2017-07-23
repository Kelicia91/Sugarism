using UnityEngine;


public class CmdPanel : Panel
{
    /********* Editor Interface *********/
    // prefabs
    public Sprite ScheduleIcon;
    public Sprite WardrobeIcon;
    public Sprite StateIcon;

    public GameObject PrefCmdButton;

    //
    private CmdButton _scheduleCmdButton = null;

    //
    void Awake()
    {
        if (null == PrefCmdButton)
        {
            Log.Error("not found prefab cmd button");
            return;
        }

        /** reverse to order **/
        {
            // temp.begin
            //createCmdButton(StateIcon, Def.CMD_TEST_COMBAT, onClickTestCombatButton);
            //createCmdButton(StateIcon, Def.CMD_TEST_BOARD_GAME, onClickTestBoardGameButton);
            createCmdButton(StateIcon, Def.CMD_GO_OUT_NAME, onClickStoryButton);
            // temp.end
        }
        createCmdButton(StateIcon, Def.CMD_STATE_NAME, onClickStateButton);
        createCmdButton(WardrobeIcon, Def.CMD_WARDROBE_NAME, onClickWardrobeButton);
        _scheduleCmdButton = createCmdButton(ScheduleIcon, Def.CMD_SCHEDULE_NAME, onClickScheduleButton);
        
        // tmp:엔딩 테스트
        //_scheduleCmdButton = createCmdButton(ScheduleIcon, Def.CMD_SCHEDULE_NAME, onClickEndingButton);

        Manager.Instance.Object.NurtureMode.Character.AgeChangeEvent.Attach(onAgeChanged);
    }


    private CmdButton createCmdButton(Sprite sprite, string text, 
                                UnityEngine.Events.UnityAction clickHandler)
    {
        GameObject o = Instantiate(PrefCmdButton);
        o.transform.SetParent(transform, false);

        CmdButton btn = o.GetComponent<CmdButton>();
        if (null == btn)
        {
            Log.Error("not found CmdButton component");
            return null;
        }

        btn.SetIcon(sprite);
        btn.SetText(text);
        btn.AddClickListener(clickHandler);

        return btn;
    }

    //
    private void onAgeChanged(int age)
    {
        if (Def.MAX_AGE != age)
            return;

        _scheduleCmdButton.SetText(Def.CMD_ENDING);
        _scheduleCmdButton.RemoveAllClickListener();
        _scheduleCmdButton.AddClickListener(onClickEndingButton);
    }

    //
    private void onClickEndingButton()
    {
        Manager.Instance.Object.Ending();
    }

    private void onClickScheduleButton()
    {
        Manager.Instance.UI.SchedulePanel.Show();
    }

    private void onClickWardrobeButton()
    {
        Manager.Instance.UI.WardrobePanel.Show();
    }

    private void onClickStateButton()
    {
        Manager.Instance.UI.StatePanel.Show();
    }

    
    // for test
    private void onClickStoryButton()
    {
        Manager.Instance.UI.SelectTargetPanel.Show();
    }

    private void onClickTestBoardGameButton()
    {
        int opponentPlayerId = 1;  // test~~~~~
        Manager.Instance.Object.BoardGameMode.Start(BoardGame.EValuationBasis.Tricker, opponentPlayerId);
    }

    private void onClickTestCombatButton()
    {
        int opponentPlayerId = 3; // test~~~
        Manager.Instance.Object.CombatMode.Start(opponentPlayerId);
    }
}
