using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlockingPanel : Panel
{
    void Awake()
    {
        Manager.Instance.Object.BoardGameMode.ShuffleEvent.Attach(onShuffle);
        Manager.Instance.Object.BoardGameMode.DrawEvent.Attach(onDraw);

        Hide();
    }

    private void onShuffle()
    {
        Hide();
    }

    private void onDraw()
    {
        Show();
    }
}
