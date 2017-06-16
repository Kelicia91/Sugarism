using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CriterionPanel : Panel
{
    /********* Editor Interface *********/
    // color
    public Color LOW_COLOR = Color.yellow;
    public Color HIGH_COLOR = Color.green;
    // prefabs
    public Text Text;


    void Awake()
    {
        Manager.Instance.Object.BoardGameMode.CriterionChangeEvent.Attach(onCriterionChanged);
    }

    private void onCriterionChanged(BoardGame.BoardGameMode.ENumberCriterion criterion)
    {
        setText(getStringCriterion(criterion));
        setColor(getColorCriterion(criterion));
    }

    private string getStringCriterion(BoardGame.BoardGameMode.ENumberCriterion criterion)
    {
        switch(criterion)
        {
            case BoardGame.BoardGameMode.ENumberCriterion.Low:
                return Def.BOARD_CRITERION_LOW;

            case BoardGame.BoardGameMode.ENumberCriterion.High:
                return Def.BOARD_CRITERION_HIGH;

            default:
                return string.Empty;
        }
    }
    private Color getColorCriterion(BoardGame.BoardGameMode.ENumberCriterion criterion)
    {
        switch (criterion)
        {
            case BoardGame.BoardGameMode.ENumberCriterion.Low:
                return LOW_COLOR;

            case BoardGame.BoardGameMode.ENumberCriterion.High:
                return HIGH_COLOR;

            default:
                return Color.clear;
        }
    }

    private void setText(string s)
    {
        if (null == Text)
        {
            Log.Error("not found text");
            return;
        }

        Text.text = s;
    }

    private void setColor(Color c)
    {
        if (null == Text)
        {
            Log.Error("not found text");
            return;
        }

        Text.color = c;
    }
}
