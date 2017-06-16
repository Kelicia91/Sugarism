using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameStatePanel : Panel
{
    /********* Editor Interface *********/
    // styles
    public FontStyle StartFontStyle = FontStyle.Bold;
    public FontStyle EndFontStyle = FontStyle.BoldAndItalic;
    public Color StartColor = Color.white;
    public Color WinColor = Color.red;
    public Color LoseColor = Color.blue;

    // prefabs
    public Text Text;


    void Awake()
    {
        Manager.Instance.Object.BoardGameMode.EndEvent.Attach(onEnd);
    }

    public void OnStart()
    {
        setText(Def.BOARD_START, StartFontStyle, StartColor);
        show(onStart);
    }

    private void win()
    {
        setText(Def.BOARD_WIN, EndFontStyle, WinColor);
    }

    private void lose()
    {
        setText(Def.BOARD_LOSE, EndFontStyle, LoseColor);
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

        yield return new WaitForSeconds(.3f);
        handler.Invoke();
    }


    private void onStart()
    {
        Hide();
        Manager.Instance.Object.BoardGameMode.Shuffle();
    }

    private void onEnd(BoardGame.BoardGameMode.EUserGameState state)
    {
        switch (state)
        {
            case BoardGame.BoardGameMode.EUserGameState.Win:
                win();
                break;

            case BoardGame.BoardGameMode.EUserGameState.Lose:
                lose();
                break;

            case BoardGame.BoardGameMode.EUserGameState.Unknown:
                Log.Error("unknown game state");
                return;
        }

        show(end);
    }

    private void end()
    {
        Manager.Instance.UI.BoardGamePanel.Hide();
        Manager.Instance.UI.MainPanel.Show();
    }
}
