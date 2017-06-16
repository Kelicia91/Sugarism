using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardGame
{
    public class Board
    {
        // const
        public const byte SIZE = 3;    // N x N
        public readonly byte[,] CellNumber = { { 4, 8, 2 }, { 6, 1, 9 }, { 5, 7, 3 } };

        //
        private Cell[,] _board = null;


        // constructor
        public Board()
        {
            _board = new Cell[SIZE, SIZE];
            for (int row = 0; row < SIZE; ++row)
            {
                for (int col = 0; col < SIZE; ++col)
                {
                    _board[row, col] = new Cell(row, col, CellNumber[row, col]);
                }
            }
        }

        public void Initialize()
        {
            for (int row = 0; row < SIZE; ++row)
            {
                for (int col = 0; col < SIZE; ++col)
                {
                    _board[row, col].Initialize();
                }
            }
        }

        public Cell Get(int row, int col)
        {
            if (false == isValid(row))
                return null;

            if (false == isValid(col))
                return null;

            return _board[row, col];
        }

        public void SetCellOwner(byte number, Cell.EOwner owner)
        {
            int row, col;
            convertNumToRowCol(number, out row, out col);

            setCellOwner(row, col, owner);
        }

        public int GetBingoCount(Cell.EOwner whose)
        {
            if (Cell.EOwner.Empty == whose)
                return 0;

            int count = 0;

            // rows
            for (int row = 0; row < SIZE; ++row)
            {
                if ((whose == _board[row, 0].Owner) && (whose == _board[row, 1].Owner) && (whose == _board[row, 2].Owner))
                {
                    _board[row, 0].IsBingo = _board[row, 1].IsBingo = _board[row, 2].IsBingo = true;
                    ++count;
                    break;
                }
            }

            // cols
            for (int col = 0; col < SIZE; ++col)
            {
                if ((whose == _board[0, col].Owner) && (whose == _board[1, col].Owner) && (whose == _board[2, col].Owner))
                {
                    _board[0, col].IsBingo = _board[1, col].IsBingo = _board[2, col].IsBingo = true;
                    ++count;
                    break;
                }
            }

            // back slash
            if ((whose == _board[0, 0].Owner) && (whose == _board[1, 1].Owner) && (whose == _board[2, 2].Owner))
            {
                _board[0, 0].IsBingo = _board[1, 1].IsBingo = _board[2, 2].IsBingo = true;
                ++count;
            }

            // slash
            if ((whose == _board[0, 2].Owner) && (whose == _board[1, 1].Owner) && (whose == _board[2, 0].Owner))
            {
                _board[0, 2].IsBingo = _board[1, 1].IsBingo = _board[2, 0].IsBingo = true;
                ++count;
            }

            return count;
        }

        public void InitBingo()
        {
            for (int row = 0; row < SIZE; ++row)
            {
                for (int col = 0; col < SIZE; ++col)
                {
                    if (true == _board[row, col].IsBingo)
                    {
                        _board[row, col].Initialize();
                    }
                }
            }
        }
        
        private void convertNumToRowCol(byte num, out int row, out int col)
        {
            row = -1;
            col = -1;

            // Ref. CellNumber[,]
            // 4 8 2
            // 6 1 9
            // 5 7 3

            if (4 == num) { row = 0; col = 0; }
            else if (8 == num) { row = 0; col = 1; }
            else if (2 == num) { row = 0; col = 2; }
            else if (6 == num) { row = 1; col = 0; }
            else if (1 == num) { row = 1; col = 1; }
            else if (9 == num) { row = 1; col = 2; }
            else if (5 == num) { row = 2; col = 0; }
            else if (7 == num) { row = 2; col = 1; }
            else if (3 == num) { row = 2; col = 2; }
            else { Log.Error(string.Format("invalid num({0})", num)); }
        }

        private void setCellOwner(int row, int col, Cell.EOwner owner)
        {
            if (false == isValid(row))
                return;

            if (false == isValid(col))
                return;

            _board[row, col].Owner = owner;
        }

        private bool isValid(int index)
        {
            if (index < 0)
                return false;
            else if (index >= SIZE)
                return false;
            else
                return true;
        }
    }
}
