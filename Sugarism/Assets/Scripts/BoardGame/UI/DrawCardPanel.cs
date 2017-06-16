using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DrawCardPanel : Panel
{
    /********* Editor Interface *********/
    // prefabs
    public Text Text;

    //
    private BoardGame.Player _player = null;
    private Image _image = null;
    
    //
    void Awake()
    {
        _image = GetComponent<Image>();

        var mode = Manager.Instance.Object.BoardGameMode;
        mode.ShuffleEvent.Attach(onShuffle);
        mode.DrawEvent.Attach(onDraw);
        mode.AttackEvent.Attach(onAttack);
        mode.RemoveAllDefenseEvent.Attach(onRemoveAllDefense);
    }

    public void Set(BoardGame.Player player)
    {
        _player = player;

        Hide();
    }

    private void onShuffle()
    {
        Hide();
    }

    private void onDraw()
    {
        if (null == _player)
        {
            Log.Error("not found player");
            return;
        }

        set(_player.DrawCard);
        show();
    }

    private void onRemoveAllDefense(int playerId, int index)
    {
        if (_player.Id != playerId)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void onAttack(int playerId)
    {
        if (_player.Id != playerId)
            return;

        Show();
    }

    private void show()
    {
        if (null == Text)
        {
            Log.Error("not found text");
            return;
        }

        Text.color = new Color(Text.color.r, Text.color.g, Text.color.b, 0.3f);

        Show();

        StartCoroutine(floating());
    }

    private IEnumerator floating()
    {
        for (float alpha = 0.3f; alpha <= 1.0f; alpha += 0.1f)
        {
            Text.color = new Color(Text.color.r, Text.color.g, Text.color.b, alpha);

            yield return new WaitForSeconds(.2f);
        }

        if (Def.MAIN_CHARACTER_ID == _player.Id)
            Manager.Instance.Object.BoardGameMode.StartJudge();  // just called once!
    }

    private void set(BoardGame.Card card)
    {
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
                break;
        }
    }

    private void setAttack()
    {
        setColor(CardButton.AttackColor);
        setText(CardButton.AttackText);
    }

    private void setDefense()
    {
        setColor(CardButton.DefenseColor);
        setText(CardButton.DefenseText);
    }

    private void setNumber(int number)
    {
        setColor(CardButton.NumberColor);
        setText(number.ToString());
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
}
