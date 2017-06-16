using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CardButton : MonoBehaviour
{
    /********* Editor Interface *********/
    // color
    public static Color NumberColor = Color.cyan;
    public static Color AttackColor = Color.magenta;
    public static Color DefenseColor = Color.yellow;

    // text
    public static string AttackText = "A";
    public static string DefenseText = "D";

    // prefabs
    public Text Text;

    //
    private BoardGame.Player _player = null;
    private int _index = -1;
    private int _number = -1;

    private Image _image = null;
    private Button _button = null;


    void Awake()
    {
        _image = GetComponent<Image>();

        _button = GetComponent<Button>();
        _button.onClick.AddListener(onClick);

        Manager.Instance.Object.BoardGameMode.RemoveAllDefenseEvent.Attach(onRemoveAllDefense);
    }

    public void Set(BoardGame.Player player)
    {
        if (null == player)
        {
            Log.Error("not found player");
            return;
        }

        _player = player;

        switch (_player.Owner)
        {
            case BoardGame.Cell.EOwner.User:
                Text.gameObject.SetActive(true);
                _button.interactable = true;
                break;

            case BoardGame.Cell.EOwner.AI:
                Text.gameObject.SetActive(false);
                _button.interactable = false;
                break;

            default:
                Log.Error("invalid player");
                break;
        }
    }

    public void SetIndex(int index)
    {
        _index = index;
    }

    public void SetCard(BoardGame.Card card)
    {
        if (null == card)
        {
            Log.Error("card is null");
            return;
        }

        switch (card.Type)
        {
            case BoardGame.Card.EType.Number:
                BoardGame.NumberCard numCard = card as BoardGame.NumberCard;
                setNumber(numCard.No);
                break;

            case BoardGame.Card.EType.Attack:
                setAttack();
                break;

            case BoardGame.Card.EType.Defense:
                setDefense();
                break;

            default:
                Log.Error("invalid card type");
                return;
        }

        Show();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void setAttack()
    {
        setColor(AttackColor);
        setText(AttackText);
    }

    private void setDefense()
    {
        setColor(DefenseColor);
        setText(DefenseText);
    }

    private void setNumber(int number)
    {
        _number = number;

        setColor(NumberColor);
        setText(_number.ToString());
    }

    private void onClick()
    {
        if (null == _player)
        {
            Log.Error("not found player");
            return;
        }

        _player.Pop(_index);

        Manager.Instance.Object.BoardGameMode.DrawEvent.Invoke();
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

    private void setColor(Color c)
    {
        if (null == _image)
        {
            Log.Error("not found image");
            return;
        }

        _image.color = c;
    }

    private void onRemoveAllDefense(int playerId, int index)
    {
        if (null == _player)
            return;

        if (_player.Id != playerId)
            return;

        if (_index != index)
            return;

        Hide();
    }
}
