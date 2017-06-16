using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardGame
{
    public class Cell
    {
        public enum EOwner { Empty, User, AI }

        // field, property
        private int _row = -1;
        public int Row { get { return _row; } }

        private int _col = -1;
        public int Col { get { return _col; } }

        private byte _number = 0;
        public byte Number { get { return _number; } }

        private EOwner _owner = EOwner.Empty;
        public EOwner Owner
        {
            get { return _owner; }
            set
            {
                _owner = value;
                Manager.Instance.Object.BoardGameMode.BoardUnitOwnerChangeEvent.Invoke(Row, Col, _owner);
            }
        }

        private bool _isBingo = false;
        public bool IsBingo
        {
            get { return _isBingo; }
            set
            {
                _isBingo = value;
                Manager.Instance.Object.BoardGameMode.BoardUnitBingoChangeEvent.Invoke(Row, Col, _isBingo);
            }
        }

        // constructor
        public Cell(int row, int col, byte number)
        {
            _row = row;
            _col = col;
            _number = number;

            _owner = EOwner.Empty;
            _isBingo = false;
        }

        public void Initialize()
        {
            Owner = EOwner.Empty;
            IsBingo = false;
        }

    }   // class

}   // namespace