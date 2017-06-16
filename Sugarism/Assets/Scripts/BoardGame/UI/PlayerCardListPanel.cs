using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCardListPanel : Panel
{
    /********* Editor Interface *********/
    // prefabs
    public GameObject PrefCardButton;

    //
    private BoardGame.Player _player = null;
    private CardButton[] _cardArray = null;


    //
    void Awake()
    {
        _cardArray = new CardButton[BoardGame.BoardGameMode.MAX_NUM_CARD];

        int numCard = _cardArray.Length;
        for (int i = 0; i < numCard; ++i)
        {
            GameObject o = Instantiate(PrefCardButton);
            o.transform.SetParent(transform, false);

            CardButton card = o.GetComponent<CardButton>();
            card.SetIndex(i);

            _cardArray[i] = card;
        }

        Manager.Instance.Object.BoardGameMode.ShuffleEvent.Attach(onShuffle);
    }

    public void Set(BoardGame.Player player)
    {
        _player = player;

        int numCard = _cardArray.Length;
        for (int i = 0; i < numCard; ++i)
        {
            _cardArray[i].Set(player);
            _cardArray[i].Hide();
        }
    }

    private void onShuffle()
    {
        if (null == _player)
        {
            Log.Error("not found player");
            return;
        }

        int numPlayerCard = _player.CardArray.Length;
        int numCardButton = _cardArray.Length;

        for (int i = 0; i < numPlayerCard; ++i)
        {
            _cardArray[i].SetCard(_player.CardArray[i]);
        }
    }
}