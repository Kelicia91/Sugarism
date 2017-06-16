using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCardPanel : Panel
{
    /********* Editor Interface *********/
    // prefabs
    public PlayerCardListPanel PlayerCardListPanel;
    public DrawCardPanel DrawCardPanel;
    

    public void Set(BoardGame.Player player)
    {
        if (null != PlayerCardListPanel)
            PlayerCardListPanel.Set(player);
        else
            Log.Error("not found player card list panel");

        if (null != DrawCardPanel)
            DrawCardPanel.Set(player);
        else
            Log.Error("not found draw card panel");
    }
}
