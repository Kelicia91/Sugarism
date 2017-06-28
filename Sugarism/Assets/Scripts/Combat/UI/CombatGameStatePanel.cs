using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class CombatGameStatePanel : Panel, IPointerClickHandler
{
    /********* Editor Interface *********/
    // styles
    public FontStyle StartFontStyle = FontStyle.Bold;
    public FontStyle EndFontStyle = FontStyle.BoldAndItalic;
    public Color StartColor = Color.white;
    public Color WinColor = Color.magenta;
    public Color LoseColor = Color.blue;

    // prefabs
    public Text Text;

    //
    private bool _isEnd = false;


    void Awake()
    {
        Manager.Instance.Object.CombatMode.EndEvent.Attach(onEnd);
    }

    public void OnStart()
    {
        _isEnd = false;

        setText(Def.START, StartFontStyle, StartColor);
        show(onStart);
    }

    private void win()
    {
        setText(Def.WIN, EndFontStyle, WinColor);
    }

    private void lose()
    {
        setText(Def.LOSE, EndFontStyle, LoseColor);
    }

    private void setText(string s, FontStyle fontStyle, Color c)
    {
        if (null == Text)
        {
            Log.Error("not found text");
            return;
        }

        Text.text = s;
        Text.fontStyle = fontStyle;
        Text.color = c;
    }

    private void show(UnityEngine.Events.UnityAction handler)
    {
        if (null == Text)
        {
            Log.Error("not found text");
            return;
        }

        Text.color = new Color(Text.color.r, Text.color.g, Text.color.b, FROM_TEXT_COLOR_ALPHA);
        RectTransform rect = Text.rectTransform;
        rect.anchoredPosition = new Vector2(0.0f, FROM_TEXT_POS_Y);

        Show();

        StartCoroutine(floating(handler));
    }

    private const float FROM_TEXT_COLOR_ALPHA = 0.3f;
    private const float TO_TEXT_COLOR_ALPHA = 1.0f;
    private const float FROM_TEXT_POS_Y = 21.0f;
    //private const float TO_TEXT_POS_Y = 0.0f;

    private IEnumerator floating(UnityEngine.Events.UnityAction handler)
    {
        RectTransform rect = Text.rectTransform;

        for (float alpha = FROM_TEXT_COLOR_ALPHA, y = FROM_TEXT_POS_Y; 
            alpha <= TO_TEXT_COLOR_ALPHA; 
            alpha += 0.1f, y -= 3.0f)
        {
            Text.color = new Color(Text.color.r, Text.color.g, Text.color.b, alpha);
            rect.anchoredPosition = new Vector2(0.0f, y);

            yield return new WaitForSeconds(.1f);
        }

        yield return new WaitForSeconds(.5f);
        handler.Invoke();
    }

    private void onStart()
    {
        Hide();
        Manager.Instance.Object.CombatMode.StartBattleRoutine();
    }

    private void onEnd(Combat.CombatMode.EUserGameState state)
    {
        switch (state)
        {
            case Combat.CombatMode.EUserGameState.Win:
                win();
                break;

            case Combat.CombatMode.EUserGameState.Lose:
                lose();
                break;

            case Combat.CombatMode.EUserGameState.Unknown:
                Log.Error("unknown game state");
                return;
        }

        show(end);
    }

    private void end()
    {
        _isEnd = true;
    }

    // 오브젝트에서 포인터를 누르고 동일한 오브젝트에서 뗄 때 호출됩니다.
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (false == _isEnd)
            return;

        Log.Debug("clicked combat's game state panel");

        Manager.Instance.UI.CombatPanel.Hide();
    }
}
