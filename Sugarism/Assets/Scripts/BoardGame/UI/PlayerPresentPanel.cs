using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerPresentPanel : Panel
{
    /********* Editor Interface *********/
    // prefabs
    public GameObject PlayerImage;

    //
    private BoardGame.Player _player = null;
    private Image _image = null;


    void Awake()
    {
        if (null != PlayerImage)
            _image = PlayerImage.GetComponent<Image>();
        else
            Log.Error("not found player image object");

        var mode = Manager.Instance.Object.BoardGameMode;
        mode.ShuffleEvent.Attach(onShuffle);
        mode.EndEvent.Attach(onEnd);
        mode.AttackEvent.Attach(onAttack);
        mode.CounterAttackEvent.Attach(onCounterAttack);
        mode.RemoveAllDefenseEvent.Attach(onRemoveAllDefense);
        mode.CellOwnerChangeEvent.Attach(onCellOwnerChanged);
    }

    public void Set(BoardGame.Player player)
    {
        if (null == player)
        {
            Log.Error("not found player");
            return;
        }

        _player = player;

        BoardGamePlayer p = Manager.Instance.DTBoardGamePlayer[_player.Id];
        setImage(p.idle);

        if (BoardGame.Cell.EOwner.User == _player.Owner)
        {
            RectTransform rect = GetComponent<RectTransform>();
            rect.rotation = new Quaternion(0.0f, 180.0f, 0.0f, 0.0f);
        }
    }

    private void setImage(Sprite s)
    {
        if (null == _image)
        {
            Log.Error("not found image");
            return;
        }

        _image.sprite = s;
    }

    private void onAttack(int playerId)
    {
        BoardGamePlayer p = Manager.Instance.DTBoardGamePlayer[_player.Id];
        if (_player.Id == playerId)
        {            
            setImage(p.attack);
        }
        else
        {
            setImage(p.damage);
        }
    }

    private void onCounterAttack(int playerId)
    {
        BoardGamePlayer p = Manager.Instance.DTBoardGamePlayer[_player.Id];
        if (_player.Id == playerId)
        {
            setImage(p.defense);
        }
        else
        {
            setImage(p.attack);
        }
    }

    private void onRemoveAllDefense(int playerId, int index)
    {
        BoardGamePlayer p = Manager.Instance.DTBoardGamePlayer[_player.Id];
        if (_player.Id == playerId)
        {
            setImage(p.damage);
        }
        else
        {
            setImage(p.idle);
        }
    }

    private void onShuffle()
    {
        BoardGamePlayer p = Manager.Instance.DTBoardGamePlayer[_player.Id];
        setImage(p.idle);
    }

    private void onEnd(BoardGame.BoardGameMode.EUserGameState state)
    {
        BoardGamePlayer p = Manager.Instance.DTBoardGamePlayer[_player.Id];
        setImage(p.idle);
    }

    private void onCellOwnerChanged(int row, int col, BoardGame.Cell.EOwner owner)
    {
        if (null == _player)
            return;

        BoardGamePlayer p = Manager.Instance.DTBoardGamePlayer[_player.Id];
        setImage(p.idle);
    }
}
