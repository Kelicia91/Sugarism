using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatPersonalInfoPanel : Panel
{
    /********* Editor Interface *********/
    // prefabs
    public GameObject PrefCombatPlayerInfoPanel;

    //
    CombatPlayerInfoPanel _userPanel = null;
    CombatPlayerInfoPanel _aiPanel = null;

    //
    void Awake()
    {
        if (null == PrefCombatPlayerInfoPanel)
        {
            Log.Error("not found prefab combat player info panel");
            return;
        }

        GameObject o = null;

        // user
        o = Instantiate(PrefCombatPlayerInfoPanel);
        o.transform.SetParent(transform, false);
        _userPanel = o.GetComponent<CombatPlayerInfoPanel>();

        // ai
        o = Instantiate(PrefCombatPlayerInfoPanel);
        o.transform.SetParent(transform, false);
        _aiPanel = o.GetComponent<CombatPlayerInfoPanel>();
    }

    public void OnStart(Combat.Player user, Combat.Player ai)
    {
        _userPanel.OnStart(user);
        _aiPanel.OnStart(ai);
    }
}
