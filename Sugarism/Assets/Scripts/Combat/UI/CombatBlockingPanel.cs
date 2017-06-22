using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatBlockingPanel : Panel
{
    void Awake()
    {
        Combat.CombatMode mode = Manager.Instance.Object.CombatMode;

        mode.StartUserBattleEvent.Attach(onStartUserBattle);
        mode.EndUserBattleEvent.Attach(onEndUserBattle);
    }

    private void onStartUserBattle()
    {
        Hide();
    }

    private void onEndUserBattle()
    {
        Show();
    }
}
