using UnityEngine;


public class TestManager : MonoBehaviour
{
    /********* Editor Interface *********/
    // exposed variables
    public BoardGame.EValuationBasis BoardGameValuationBasis = BoardGame.EValuationBasis.Tricker;
    public int BoardGameOpponentPlayerId = 1;
    public int CombatOpponentPlayerId = 3;
    public string ScenarioPath = RsrcLoader.SCENARIO_FOLDER_PATH;

    // prefabs
    public GameObject PrefTesterCmdPanel;
    public GameObject PrefCmdButton;


    //
    void Awake()
    {
        Canvas canvas = Manager.Instance.UI.Canvas;

        GameObject cmdPanel = Instantiate(PrefTesterCmdPanel);
        cmdPanel.transform.SetParent(canvas.transform, false);
        cmdPanel.transform.SetSiblingIndex(1);  // hard coding: next MainPanel
        
        CmdButton combatBtn = createCmdButton(Def.CMD_TEST_COMBAT, onClickTestCombatButton);
        combatBtn.transform.SetParent(cmdPanel.transform, false);

        CmdButton boardGameBtn = createCmdButton(Def.CMD_TEST_BOARD_GAME, onClickTestBoardGameButton);
        boardGameBtn.transform.SetParent(cmdPanel.transform, false);

        CmdButton storyBtn = createCmdButton(Def.CMD_GO_OUT_NAME, onClickStoryButton);
        storyBtn.transform.SetParent(cmdPanel.transform, false);

        CmdButton endingBtn = createCmdButton(Def.CMD_ENDING, onClickEndingButton);
        endingBtn.transform.SetParent(cmdPanel.transform, false);
    }


    private CmdButton createCmdButton(string text, UnityEngine.Events.UnityAction clickHandler)
    {
        GameObject o = Instantiate(PrefCmdButton);

        CmdButton btn = o.GetComponent<CmdButton>();
        btn.SetText(text);
        btn.AddClickListener(clickHandler);

        return btn;
    }

    
    private void onClickStoryButton()
    {
        Manager.Instance.Object.StoryMode.LoadScenario(ScenarioPath);
    }

    private void onClickTestBoardGameButton()
    {
        Manager.Instance.Object.BoardGameMode.Start(
            BoardGameValuationBasis, BoardGameOpponentPlayerId);
    }

    private void onClickTestCombatButton()
    {
        Manager.Instance.Object.CombatMode.Start(CombatOpponentPlayerId);
    }

    private void onClickEndingButton()
    {
        Manager.Instance.Object.Ending();
    }
}
