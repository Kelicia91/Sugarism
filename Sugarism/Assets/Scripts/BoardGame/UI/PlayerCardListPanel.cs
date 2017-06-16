using System.Collections;
using UnityEngine;
using UnityEngine.UI;


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

        var mode = Manager.Instance.Object.BoardGameMode;
        mode.ShuffleEvent.Attach(onShuffle);
        mode.RemoveAllDefenseEvent.Attach(onRemoveAllDefense);
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
            _cardArray[i].gameObject.SetActive(true);
        }
    }

    private void onRemoveAllDefense(int playerId, int index)
    {
        Log.Debug("PlayerCardListPanel.onRemoveAllDefense");

        if (_player.Id != playerId)
            return;

        const float waitSeconds = 1.0f;
        StartCoroutine(disappear(waitSeconds, _cardArray[index]));
    }

    IEnumerator disappear(float waitSeconds, CardButton card)
    {
        // @todo: 뭔가아쉬운 연출...
        for (int i = 0; i < 1; ++i)
        {
            yield return new WaitForSeconds(waitSeconds);
        }
        
        card.gameObject.SetActive(false);
        Manager.Instance.Object.BoardGameMode.JudgeIter();
    }
}