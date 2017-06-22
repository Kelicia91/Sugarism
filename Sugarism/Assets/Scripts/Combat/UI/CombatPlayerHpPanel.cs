using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CombatPlayerHpPanel : Panel
{
    private int _playerId = -1;
    private CombatStatPanel _statPanel = null;

    //
    void Awake()
    {
        _statPanel = GetComponent<CombatStatPanel>();

        Manager.Instance.Object.CombatMode.HpChangeEvent.Attach(onHpChanged);
    }

    public void OnStart(Combat.Player player)
    {
        _playerId = player.Id;

        _statPanel.Set(Def.HP, player.Hp);
    }

    public void Set(int value)
    {
        _statPanel.SetRoutine(value);
    }

    private void onHpChanged(int playerId, int hp)
    {
        if (_playerId != playerId)
            return;

        Set(hp);
    }
}
