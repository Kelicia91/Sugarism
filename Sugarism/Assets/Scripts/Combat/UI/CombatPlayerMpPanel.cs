using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CombatPlayerMpPanel : Panel
{
    private int _playerId = -1;
    private CombatStatPanel _statPanel = null;
    
    //
    void Awake()
    {
        _statPanel = GetComponent<CombatStatPanel>();

        Manager.Instance.Object.CombatMode.MpChangeEvent.Attach(onMpChanged);
    }

    public void OnStart(Combat.Player player)
    {
        _playerId = player.Id;

        _statPanel.Set(Def.MP, player.Mp);
    }

    public void Set(int value)
    {
        _statPanel.Set(value);
    }

    private void onMpChanged(int playerId, int mp)
    {
        if (_playerId != playerId)
            return;

        Set(mp);
    }
}
