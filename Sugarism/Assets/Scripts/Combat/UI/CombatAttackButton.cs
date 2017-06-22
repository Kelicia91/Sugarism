using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CombatAttackButton : MonoBehaviour
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

        setText(Def.COMBAT_CMD_ATTACK);
    }

    private void onClick()
    {
        Log.Debug("click combat attack button");

        Combat.CombatMode mode = Manager.Instance.Object.CombatMode;
        mode.EndUserBattleEvent.Invoke();
        mode.UserPlayer.Attack();
    }

    private void setText(string s)
    {
        if (null == Text)
        {
            Log.Error("not found combat attack button's text");
            return;
        }

        Text.text = s;
    }
}
