using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerInfoPanel : Panel
{
    /********* Editor Interface *********/
    // prefabs
    public Text NameText;
    public GameObject PrefStatPanel;

    //
    private const int NUM_STAT = 3;
    private StatPanel[] _statPanelArray = null;


    // Use this for initialization
    void Awake()
    {
        createStats();
    }


    public void Set(BoardGame.Player player)
    {
        if (null == player)
        {
            Log.Error("not found player");
            return;
        }
        
        setNameText(player.Name);
        setStats(player);
    }


    private void createStats()
    {
        if (null == PrefStatPanel)
        {
            Log.Error("not found prefab stat panel");
            return;
        }

        _statPanelArray = new StatPanel[NUM_STAT];

        int numStat = _statPanelArray.Length;
        for (int i = 0; i < numStat; ++i)
        {
            GameObject o = Instantiate(PrefStatPanel);
            o.transform.SetParent(transform, false);

            _statPanelArray[i] = o.GetComponent<StatPanel>();
        }
    }

    private void setStats(BoardGame.Player player)
    {
        _statPanelArray[0].Set(EStat.INTELLECT, player.Intellect);
        _statPanelArray[1].Set(EStat.TACTIC, player.Tactic);
        _statPanelArray[2].Set(EStat.LEADERSHIP, player.Leadership);
    }

    private void setNameText(string s)
    {
        if (null == NameText)
        {
            Log.Error("not found name text");
            return;
        }

        NameText.text = s;
    }
}
