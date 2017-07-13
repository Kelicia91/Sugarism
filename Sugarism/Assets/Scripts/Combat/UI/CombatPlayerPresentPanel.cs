using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CombatPlayerPresentPanel : Panel
{
    /********* Editor Interface *********/
    // styles - damage text
    public Color CriticalColor = Color.red;
    public Color NormalColor = Color.magenta;
    public int CriticalSize = 100;
    public int NormalSize = 70;
    // prefabs
    public Image Image;
    public FloatingText FloatingText;

    //
    private Combat.Player _player = null;

    //
    void Awake()
    {
        Combat.CombatMode mode = Manager.Instance.Object.CombatMode;
        mode.StartUserBattleEvent.Attach(onStartUserBattle);
        mode.StartAIBattleEvent.Attach(onStartAIBattle);
        mode.EndEvent.Attach(onEnd);
        mode.CriticalAttackEvent.Attach(onCriticalAttack);
        mode.AttackEvent.Attach(onAttack);
        mode.CriticalTrickEvent.Attach(onCriticalTrick);
        mode.TrickEvent.Attach(onTrick);
    }


    public void OnStart(Combat.Player player)
    {
        _player = player;

        CombatPlayer p = Manager.Instance.DTCombatPlayer[_player.Id];
        set(p.idle);

        if (player.Id == Def.MAIN_CHARACTER_ID)
        {
            rotate180Yaxis(Image.gameObject);
        }

        FloatingText.Hide();
    }

    private void rotate180Yaxis(GameObject o)
    {
        if (null == o)
        {
            Log.Error("not found game object to rotate");
            return;
        }

        Quaternion rotation = new Quaternion(0.0f, 180.0f, 0.0f, 0.0f);

        RectTransform[] childrenRect = o.GetComponentsInChildren<RectTransform>();
        int childrenRectCount = childrenRect.Length;
        for (int i = 0; i < childrenRectCount; ++i)
        {
            childrenRect[i].localRotation = rotation;
        }
    }

    private void set(Sprite s)
    {
        if (null == Image)
        {
            Log.Error("not found combat player present image");
            return;
        }

        Image.sprite = s;
        Image.preserveAspect = true;
    }


    private void onStartUserBattle()
    {
        CombatPlayer p = Manager.Instance.DTCombatPlayer[_player.Id];
        set(p.idle);
    }

    private void onStartAIBattle()
    {
        CombatPlayer p = Manager.Instance.DTCombatPlayer[_player.Id];
        set(p.idle);
    }

    private void onEnd(Combat.CombatMode.EUserGameState state)
    {
        CombatPlayer p = Manager.Instance.DTCombatPlayer[_player.Id];
        set(p.idle);
    }

    private void onCriticalAttack(int playerId, int damage)
    {
        CombatPlayer p = Manager.Instance.DTCombatPlayer[_player.Id];

        Sprite s = null;
        if (_player.Id == playerId)
        {
            s = p.att;
        }
        else
        {
            s = p.dmg;
            FloatingText.Float(damage.ToString(), CriticalSize, CriticalColor);
        }

        set(s);
    }

    private void onAttack(int playerId, int damage)
    {
        CombatPlayer p = Manager.Instance.DTCombatPlayer[_player.Id];

        Sprite s = null;
        if (_player.Id == playerId)
        {
            s = p.att;
        }
        else
        {
            s = p.dfs;
            FloatingText.Float(damage.ToString(), NormalSize, NormalColor);
        }

        set(s);
    }

    private void onCriticalTrick(int playerId, int damage)
    {
        CombatPlayer p = Manager.Instance.DTCombatPlayer[_player.Id];

        Sprite s = null;
        if (_player.Id == playerId)
        {
            s = p.trick;
        }
        else
        {
            s = p.dmg;
            FloatingText.Float(damage.ToString(), CriticalSize, CriticalColor);
        }            

        set(s);
    }

    private void onTrick(int playerId, int damage)
    {
        CombatPlayer p = Manager.Instance.DTCombatPlayer[_player.Id];

        Sprite s = null;
        if (_player.Id == playerId)
        {
            s = p.trick;
        }
        else
        {
            s = p.dfs;
            FloatingText.Float(damage.ToString(), NormalSize, NormalColor);
        }

        set(s);
    }
}
