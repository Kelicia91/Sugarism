using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CombatTrickButton : MonoBehaviour
{
    /********* Editor Interface *********/
    // prefabs
    public Text Text;

    //
    private Button _btn = null;

    void Awake()
    {
        _btn = GetComponent<Button>();
        _btn.onClick.AddListener(onClick);

        setText(Def.COMBAT_CMD_TRICK);

        Manager.Instance.Object.CombatMode.StartUserBattleEvent.Attach(onStartUserBattle);
    }

    private void onClick()
    {
        Log.Debug("click combat trick button");

        Combat.CombatMode mode = Manager.Instance.Object.CombatMode;
        mode.EndUserBattleEvent.Invoke();
        mode.UserPlayer.Trick();
    }

    private void onStartUserBattle()
    {
        if (false == Manager.Instance.Object.CombatMode.UserPlayer.CanTrick())
            _btn.interactable = false;
        else
            _btn.interactable = true;
    }

    private void setText(string s)
    {
        if (null == Text)
        {
            Log.Error("not found combat trick button's text");
            return;
        }

        Text.text = s;
    }
}
