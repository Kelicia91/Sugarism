using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CellPanel : Panel
{
    /********* Editor Interface *********/
    // colors
    public Color USER_COLOR = Color.red;
    public Color AI_COLOR = Color.blue;
    public Color EMPTY_COLOR = Color.white;

    // prefabs
    public GameObject BingoImage;
    public Text Text;

    //
    private BoardGame.Cell _cell = null;
    private Image _image = null;


    void Awake()
    {
        _image = GetComponent<Image>();

        Manager.Instance.Object.BoardGameMode.CellOwnerChangeEvent.Attach(onBoardUnitOwnerChanged);
        Manager.Instance.Object.BoardGameMode.CellBingoChangeEvent.Attach(onBoardUnitBingoChanged);
    }

    public void Set(BoardGame.Cell cell)
    {
        if (null == cell)
        {
            Log.Error("not found cell");
            return;
        }

        _cell = cell;
        
        setText(_cell.Number.ToString());
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

    private Color getOwnerColor(BoardGame.Cell.EOwner owner)
    {
        switch(owner)
        {
            case BoardGame.Cell.EOwner.User:
                return USER_COLOR;

            case BoardGame.Cell.EOwner.AI:
                return AI_COLOR;

            case BoardGame.Cell.EOwner.Empty:
                return EMPTY_COLOR;

            default:
                return Color.clear;
        }
    }

    private void setBingo(bool isBingo)
    {
        if (null == BingoImage)
        {
            Log.Error("not found bingo image");
            return;
        }

        if (isBingo)
            BingoImage.SetActive(true);
        else
            BingoImage.SetActive(false);
    }


    private void onBoardUnitOwnerChanged(int row, int col, BoardGame.Cell.EOwner owner)
    {
        if (null == _cell)
            return;

        if ((_cell.Row != row) || (_cell.Col != col))
            return;

        Color c = getOwnerColor(owner);

        if (gameObject.activeInHierarchy)
        {
            const float waitSeconds = 0.1f;
            StartCoroutine(coloring(waitSeconds, c));
        }
        else
        {
            setColor(c);
        }
    }

    private void onBoardUnitBingoChanged(int row, int col, bool isBingo)
    {
        if (null == _cell)
            return;

        if ((_cell.Row != row) || (_cell.Col != col))
            return;

        setBingo(isBingo);
    }

    IEnumerator coloring(float waitSeconds, Color target)
    {
        Color targetColor = target;
        for (float alpha = 0.0f; alpha <= 1.0f; alpha += 0.3f)
        {
            Color c = new Color(targetColor.r, targetColor.g, targetColor.b, alpha);
            setColor(c);

            yield return new WaitForSeconds(waitSeconds);
        }

        setColor(target);
        Manager.Instance.Object.BoardGameMode.JudgeIter();
    }
}
