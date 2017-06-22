using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CombatTurnPanel : Panel
{
    /********* Editor Interface *********/
    // color
    public Color DEFAULT_COLOR = Color.black;
    public Color URGENT_COLOR = Color.red;
    // prefabs
    public Text Text;


    void Awake()
    {
        Manager.Instance.Object.CombatMode.TurnChangeEvent.Attach(onTurnChanged);
    }

    public void OnStart()
    {
        setColor(DEFAULT_COLOR);
    }

    private void setText(string s)
    {
        if (null == Text)
        {
            Log.Error("not found combat turn text");
            return;
        }

        Text.text = s;
    }

    private void setColor(Color c)
    {
        if (null == Text)
        {
            Log.Error("not found combat turn text");
            return;
        }

        Text.color = c;
    }

    private void onTurnChanged(byte turn)
    {
        string s = turn.ToString();
        setText(s);

        if (turn <= Combat.CombatMode.URGENT_REMAIN_TURN)
            setColor(URGENT_COLOR);
        else
            setColor(DEFAULT_COLOR);
    }
}
