using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatPresentPanel : Panel
{
    /********* Editor Interface *********/
    // prefabs
    public GameObject PrefCombatPlayerPresentPanel;

    //
    CombatPlayerPresentPanel _userPanel = null;
    CombatPlayerPresentPanel _aiPanel = null;

    //
    void Awake()
    {
        if (null == PrefCombatPlayerPresentPanel)
        {
            Log.Error("not found prefab combat player present panel");
            return;
        }

        GameObject o = null;

        // user
        o = Instantiate(PrefCombatPlayerPresentPanel);
        o.transform.SetParent(transform, false);
        _userPanel = o.GetComponent<CombatPlayerPresentPanel>();

        // ai
        o = Instantiate(PrefCombatPlayerPresentPanel);
        o.transform.SetParent(transform, false);
        _aiPanel = o.GetComponent<CombatPlayerPresentPanel>();
    }

    public void OnStart(Combat.Player user, Combat.Player ai)
    {
        _userPanel.OnStart(user);
        _aiPanel.OnStart(ai);
    }
}
