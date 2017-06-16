using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoardGamePanel : Panel
{
    /********* Editor Interface *********/
    // prefabs
    public TurnPanel TurnPanel;
    public CriterionPanel CriterionPanel;
    public BoardPanel BoardPanel;

    public PresentPanel PresentPanel;
    public FlowSlider FlowSlider;

    public CardPanel CardPanel;
    public PersonalInfoPanel PersonalInfoPanel;
    
    public BlockingPanel BlockingPanel;
    public GameStatePanel GameStatePanel;


    void Awake()
    {
        Manager.Instance.Object.BoardGameMode.StartEvent.Attach(onStart);
    }


    private void onStart(BoardGame.UserPlayer user, BoardGame.AIPlayer ai)
    {
        TurnPanel.OnStart();
        CriterionPanel.Show();
        BoardPanel.Show();

        PresentPanel.OnStart(user, ai);
        
        CardPanel.OnStart(user, ai);
        PersonalInfoPanel.OnStart(user, ai);

        BlockingPanel.Show();
        GameStatePanel.Hide();

        Show();

        GameStatePanel.OnStart();
    }
}
