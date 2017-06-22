using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CombatPlayerInfoPanel : Panel
{
    /********* Editor Interface *********/
    // prefabs
    public Transform TopPanelTransform;
    public Text NameText;
    public Transform BottomPanelTransform;

    public GameObject PrefCombatStatPanel;

    //
    private Combat.Player _player = null;

    // top
    private CombatPlayerHpPanel _hpPanel = null;
    private CombatPlayerMpPanel _mpPanel = null;

    // bottom
    private CombatStatPanel _warriorValuesPanel = null;
    private CombatStatPanel _trickerValuesPanel = null;


    //
    void Awake()
    {
        createTop();
        createBottom();
    }


    public void OnStart(Combat.Player player)
    {
        _player = player;

        _hpPanel.OnStart(_player);
        _mpPanel.OnStart(_player);

        setNameText(_player.Name);

        int warriorValues = (_player.AttackPower + _player.Defense) / 2;
        _warriorValuesPanel.Set(Def.WARRIOR_VALUES, warriorValues);

        int trickerValues = (_player.Intellect + _player.Tactic) / 2;
        _trickerValuesPanel.Set(Def.TRICKER_VALUES, trickerValues);
    }

    private void createTop()
    {
        GameObject o = null;


        o = Instantiate(PrefCombatStatPanel);
        o.transform.SetParent(TopPanelTransform, false);

        _hpPanel = o.AddComponent<CombatPlayerHpPanel>();


        o = Instantiate(PrefCombatStatPanel);
        o.transform.SetParent(TopPanelTransform, false);

        _mpPanel = o.AddComponent<CombatPlayerMpPanel>();
    }

    private void createBottom()
    {
        GameObject o = null;


        o = Instantiate(PrefCombatStatPanel);
        o.transform.SetParent(BottomPanelTransform, false);

        _warriorValuesPanel = o.GetComponent<CombatStatPanel>();


        o = Instantiate(PrefCombatStatPanel);
        o.transform.SetParent(BottomPanelTransform, false);

        _trickerValuesPanel = o.GetComponent<CombatStatPanel>();
    }


    private void setNameText(string name)
    {
        if (null == NameText)
        {
            Log.Error("not found name text");
            return;
        }

        NameText.text = name;
    }
}
