using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class GameStatePanel : Panel, IPointerClickHandler
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
        Manager.Instance.Object.BoardGameMode.EndEvent.Attach(onEnd);
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
        
        Text.color = new Color(Text.color.r, Text.color.g, Text.color.b, 0.3f);
        RectTransform rect = Text.rectTransform;
        rect.anchoredPosition = new Vector2(0.0f, 21.0f);

        Show();

        StartCoroutine(floating(handler));
    }

    private IEnumerator floating(UnityEngine.Events.UnityAction handler)
    {
        RectTransform rect = Text.rectTransform;
        
        for (float alpha = 0.3f, y = 21.0f; alpha <= 1.0f; alpha += 0.1f, y -= 3.0f)
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
        Manager.Instance.Object.BoardGameMode.Shuffle();
    }

    private void onEnd(BoardGame.EUserGameState state)
    {
        switch (state)
        {
            case BoardGame.EUserGameState.Win:
                win();
                break;

            case BoardGame.EUserGameState.Lose:
                lose();
                break;

            case BoardGame.EUserGameState.Unknown:
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

        Log.Debug("clicked game state panel");

        Manager.Instance.UI.BoardGamePanel.Hide();
        Manager.Instance.UI.MainPanel.Show();
    }
}
