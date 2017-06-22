using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CombatCommentPanel : Panel
{
    /********* Editor Interface *********/
    // styles
    public Color UserPlayerNameTextColor = Color.red;
    public Color AIPlayerNameTextColor = Color.blue;
    // prefabs
    public GameObject LayoutPanel;
    public Text PlayerNameText;
    public Text SuffixText;

    //
    private string _userName = null;
    private string _aiName = null;

    //
    void Awake()
    {
        setPlayerNameText(string.Empty);
        setSuffixText(Def.COMBAT_COMMENT_PLAYER_TURN);

        Combat.CombatMode mode = Manager.Instance.Object.CombatMode;
        mode.StartUserBattleEvent.Attach(onStartUserBattle);
        mode.StartAIBattleEvent.Attach(onStartAIBattle);
        mode.EndAIBattleEvent.Attach(onEndAIBattle);
    }

    public void OnStart(Combat.UserPlayer user, Combat.AIPlayer ai)
    {
        _userName = user.Name;
        _aiName = ai.Name;

        hideTexts();
    }

    private void onStartUserBattle()
    {
        setPlayerNameText(_userName);
        setPlayerNameColor(UserPlayerNameTextColor);

        showTexts();
    }

    private void onStartAIBattle()
    {
        hideTexts();

        setPlayerNameText(_aiName);
        setPlayerNameColor(AIPlayerNameTextColor);

        showTexts();
        
        Invoke("next", DELAY_TIME_SECONDS);
    }

    private const float DELAY_TIME_SECONDS = 0.5f;
    private void next()
    {
        Manager.Instance.Object.CombatMode.BattleIterate();
    }

    private void onEndAIBattle()
    {
        hideTexts();
    }

    private void showTexts()
    {
        if (null == LayoutPanel)
        {
            Log.Error("not found layout panel");
            return;
        }

        LayoutPanel.SetActive(true);
    }

    private void hideTexts()
    {
        if (null == LayoutPanel)
        {
            Log.Error("not found layout panel");
            return;
        }

        LayoutPanel.SetActive(false);
    }

    private void setPlayerNameText(string name)
    {
        if (null == PlayerNameText)
        {
            Log.Error("not found player name text");
            return;
        }

        PlayerNameText.text = name;
    }

    private void setPlayerNameColor(Color c)
    {
        if (null == PlayerNameText)
        {
            Log.Error("not found player name text");
            return;
        }

        PlayerNameText.color = c;
    }

    private void setSuffixText(string suffix)
    {
        if (null == SuffixText)
        {
            Log.Error("not found suffix text");
            return;
        }

        SuffixText.text = suffix;
    }
}
