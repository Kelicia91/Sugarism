using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatPanel : Panel
{
    /********* Editor Interface *********/
    // prefabs
    public CombatTurnPanel TurnPanel;
    public CombatCommentPanel CommentPanel;
    public CombatPresentPanel PresentPanel;
    public CombatPersonalInfoPanel PersonalInfoPanel;
    public CombatAttackButton AttackButton;
    public CombatTrickButton TrickButton;
    public CombatBlockingPanel BlockingPanel;
    public CombatGameStatePanel GameStatePanel;

    //
    void Awake()
    {
        Manager.Instance.Object.CombatMode.StartEvent.Attach(onStart);
    }

    private void onStart(Combat.UserPlayer user, Combat.AIPlayer ai)
    {
        TurnPanel.OnStart();
        CommentPanel.OnStart(user, ai);
        PresentPanel.OnStart(user, ai);
        PersonalInfoPanel.OnStart(user, ai);
        // attack button
        // trick button
        BlockingPanel.Show();
        GameStatePanel.Hide();

        Show();

        GameStatePanel.OnStart();
    }
}
