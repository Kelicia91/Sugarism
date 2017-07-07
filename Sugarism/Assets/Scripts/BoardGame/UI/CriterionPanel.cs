using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class CriterionPanel : Panel
{
    /********* Editor Interface *********/
    // color
    public Color LOW_BG_COLOR = Color.black;
    public Color LOW_TEXT_COLOR = Color.yellow;
    public Color HIGH_BG_COLOR = Color.white;
    public Color HIGH_TEXT_COLOR = Color.green;
    // prefabs
    public Text Text;

    //
    private Image _bgImage = null;


    void Awake()
    {
        _bgImage = GetComponent<Image>();

        Manager.Instance.Object.BoardGameMode.CriterionChangeEvent.Attach(onCriterionChanged);
    }

    private void onCriterionChanged(BoardGame.ENumberCriterion criterion)
    {
        setText(getTextCriterion(criterion));
        setTextColor(getTextColorCriterion(criterion));

        Color bgColor = getBgColorCriterion(criterion);
        if (gameObject.activeInHierarchy)
            StartCoroutine(bgColoring(waitSeconds, bgColor));
        else
            setBgColor(bgColor);
    }

    private string getTextCriterion(BoardGame.ENumberCriterion criterion)
    {
        switch(criterion)
        {
            case BoardGame.ENumberCriterion.Low:
                return Def.BOARD_CRITERION_LOW;

            case BoardGame.ENumberCriterion.High:
                return Def.BOARD_CRITERION_HIGH;

            default:
                return string.Empty;
        }
    }
    private Color getTextColorCriterion(BoardGame.ENumberCriterion criterion)
    {
        switch (criterion)
        {
            case BoardGame.ENumberCriterion.Low:
                return LOW_TEXT_COLOR;

            case BoardGame.ENumberCriterion.High:
                return HIGH_TEXT_COLOR;

            default:
                return Color.clear;
        }
    }

    private Color getBgColorCriterion(BoardGame.ENumberCriterion criterion)
    {
        switch (criterion)
        {
            case BoardGame.ENumberCriterion.Low:
                return LOW_BG_COLOR;

            case BoardGame.ENumberCriterion.High:
                return HIGH_BG_COLOR;

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

    private void setTextColor(Color c)
    {
        if (null == Text)
        {
            Log.Error("not found text");
            return;
        }

        Text.color = c;
    }

    private void setBgColor(Color c)
    {
        if (null == _bgImage)
        {
            Log.Error("not found bg image component");
            return;
        }

        _bgImage.color = c;
    }

    private const float waitSeconds = 0.1f;
    private IEnumerator bgColoring(float waitSeconds, Color color)
    {
        Color originColor = _bgImage.color;
        Color targetColor = color;

        for (float i = 1.0f; i > 0.0f; i -= 0.2f)
        {
            float j = 1 - i;

            float red = (originColor.r * i) + (targetColor.r * j);
            float green = (originColor.g * i) + (targetColor.g * j);
            float blue = (originColor.b * i) + (targetColor.b * j);
            float alpha = (originColor.a * i) + (targetColor.a * j);

            Color c = new Color(red, green, blue, alpha);
            setBgColor(c);

            yield return new WaitForSeconds(waitSeconds);
        }

        setBgColor(targetColor);
        Manager.Instance.Object.BoardGameMode.JudgeIter();
    }
}
