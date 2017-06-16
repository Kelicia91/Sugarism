using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CriterionPanel : Panel
{
    /********* Editor Interface *********/
    // prefabs
    public Text Text;


    void Awake()
    {
        Manager.Instance.Object.BoardGameMode.CriterionChangeEvent.Attach(onCriterionChanged);
    }

    private void onCriterionChanged(BoardGame.BoardGameMode.ENumberCriterion criterion)
    {
        setText(getStringCriterion(criterion));
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

    private void setText(string s)
    {
        if (null == Text)
        {
            Log.Error("not found text");
            return;
        }

        Text.text = s;
    }
}
