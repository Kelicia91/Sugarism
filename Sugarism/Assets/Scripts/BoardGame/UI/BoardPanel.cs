using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BoardPanel : Panel
{
    /********* Editor Interface *********/
    // prefabs
    public GameObject PrefCellPanel;

    //
    private GridLayoutGroup _gridLayout = null;


    void Awake()
    {
        _gridLayout = GetComponent<GridLayoutGroup>();
        _gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        _gridLayout.constraintCount = BoardGame.Board.SIZE;

        create();
    }

    private void create()
    {
        if (null == PrefCellPanel)
        {
            Log.Error("not found prefab cell panel");
            return;
        }

        BoardGame.Board board = Manager.Instance.Object.BoardGameMode.Board;

        for (int row = 0; row < BoardGame.Board.SIZE; ++row)
        {
            for (int col = 0; col < BoardGame.Board.SIZE; ++col)
            {
                GameObject o = Instantiate(PrefCellPanel);
                o.transform.SetParent(transform, false);

                CellPanel p = o.GetComponent<CellPanel>();
                p.Set(board.Get(row, col));
            }
        }
    }
}
