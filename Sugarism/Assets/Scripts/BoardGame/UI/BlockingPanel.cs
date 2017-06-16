using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlockingPanel : Panel
{
    void Awake()
    {
        Manager.Instance.Object.BoardGameMode.DrawEvent.Attach(onDraw);
        Manager.Instance.Object.BoardGameMode.JudgeEvent.Attach(onJudge);

        Hide();
    }

    private void onDraw()
    {
        Hide();
    }

    private void onJudge()
    {
        Show();
    }
}
