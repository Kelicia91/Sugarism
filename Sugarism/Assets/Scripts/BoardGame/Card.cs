using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardGame
{
    public class Card
    {
        public enum EType
        {
            Number,
            Attack,
            Defense,

            MAX
        }

        private EType _type = EType.MAX;
        public EType Type
        {
            get { return _type; }
        }

        public Card(EType type)
        {
            _type = type;
        }
    }

    public class NumberCard : Card
    {
        public const byte MIN_NO = 1;
        public const byte MAX_NO = Board.SIZE * Board.SIZE;

        private byte _no = 0;
        public byte No
        {
            get { return _no; }
            private set
            {
                _no = value;

                if (_no < MIN_NO)
                    _no = MIN_NO;
                else if (_no > MAX_NO)
                    _no = MAX_NO;
            }
        }

        public NumberCard(byte no) : base(EType.Number)
        {
            No = no;
        }

    }   // class

}   // namespace
