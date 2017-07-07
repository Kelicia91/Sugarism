using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BoardGame
{
    public abstract class Player
    {
        protected int _id = -1;
        public int Id { get { return _id; } }   // DTBoardGamePlayer.id

        protected string _name = null;
        public string Name { get { return _name; } }

        //
        protected int _intellect = 0;
        public int Intellect { get { return _intellect; } }

        protected int _tactic = 0;
        public int Tactic { get { return _tactic; } }

        protected int _leadership = 0;
        public int Leadership { get { return _leadership; } }

        protected int _grace = 0;
        public int Grace { get { return _grace; } }

        protected int _morality = 0;
        public int Morality { get { return _morality; } }

        protected int _goodness = 0;
        public int Goodness { get { return _goodness; } }
        //

        private int _power = 0;
        public int Power { get { return _power; } }

        private byte _cardCapacity = 0;
        public byte CardCapacity
        {
            get { return _cardCapacity; }
            private set
            {
                _cardCapacity = value;

                if (_cardCapacity < BoardGameMode.MIN_NUM_CARD)
                    _cardCapacity = BoardGameMode.MIN_NUM_CARD;
                else if (_cardCapacity > BoardGameMode.MAX_NUM_CARD)
                    _cardCapacity = BoardGameMode.MAX_NUM_CARD;
            }
        }

        private Card[] _cardArray = null;
        public Card[] CardArray { get { return _cardArray; } }

        private BoardGameMode _mode = null;
        public BoardGameMode Mode { get { return _mode; } }

        private Player _opponent = null;
        public Player Opponent { get { return _opponent; } }

        private Card _drawCard = null;
        public Card DrawCard
        {
            get { return _drawCard; }
            protected set { _drawCard = value; }
        }

        private int _attackShuffleProbability = 0;
        protected int AttackShuffleProbability
        {
            get { return _attackShuffleProbability; }
            private set
            {
                _attackShuffleProbability = value;

                if (_attackShuffleProbability < 0)
                    _attackShuffleProbability = 0;
                else if (_attackShuffleProbability > 100)
                    _attackShuffleProbability = 100;
            }
        }

        private int _defenseShuffleProbability = 0;
        protected int DefenseShuffleProbability
        {
            get { return _defenseShuffleProbability; }
            private set
            {
                _defenseShuffleProbability = value;

                if (_defenseShuffleProbability < 0)
                    _defenseShuffleProbability = 0;
                else if (_defenseShuffleProbability > 100)
                    _defenseShuffleProbability = 100;
            }
        }

        private byte _numAttack = 0;
        public byte NumAttack
        {
            get { return _numAttack; }
            protected set
            {
                _numAttack = value;

                if (_numAttack < 0)
                    _numAttack = 0;
                else if (_numAttack > BoardGameMode.MAX_NUM_ATTACK_CARD)
                    _numAttack = BoardGameMode.MAX_NUM_ATTACK_CARD;
            }
        }

        private byte _numDefense = 0;
        public byte NumDefense
        {
            get { return _numDefense; }
            set
            {
                _numDefense = value;

                if (_numDefense < 0)
                    _numDefense = 0;
                else if (_numDefense > BoardGameMode.MAX_NUM_DEFENSE_CARD)
                    _numDefense = BoardGameMode.MAX_NUM_DEFENSE_CARD;
            }
        }

        private Cell.EOwner _owner = Cell.EOwner.Empty;
        public Cell.EOwner Owner { get { return _owner; } }


        // constructor
        protected Player(BoardGameMode mode, Cell.EOwner owner)
        {
            _mode = mode;
            _owner = owner;
        }

        private void initialize(int powerBaseStat, int cardCapacityBaseStat, int attackShuffleProbabilityBaseStat, int defenseShuffleProbabilityBaseStat)
        {
            _power = powerBaseStat / 100 + 1;

            _cardCapacity = setMaxNumCard(cardCapacityBaseStat);
            _cardArray = new Card[CardCapacity];

            AttackShuffleProbability = BoardGameMode.DEFAULT_ATTACK_CARD_SHUFFLE_PROBABILITY + (BoardGameMode.STAT_WEIGHT_SHUFFLE_PROBABILITY * attackShuffleProbabilityBaseStat / Def.MAX_STAT);
            DefenseShuffleProbability = BoardGameMode.DEFAULT_DEFENSE_CARD_SHUFFLE_PROBABILITY + (BoardGameMode.STAT_WEIGHT_SHUFFLE_PROBABILITY * defenseShuffleProbabilityBaseStat / Def.MAX_STAT);
        }

        private void initialize(EValuationBasis valuationBasis)
        {
            switch (valuationBasis)
            {
                case EValuationBasis.Tricker:
                    initialize(Intellect, Intellect, Tactic, Leadership);
                    break;

                case EValuationBasis.Politician:
                    initialize(Grace, Grace, Morality, Goodness);
                    break;

                default:
                    break;
            }
        }

        public abstract void Push();
        public abstract void Bingo(int bingoCount);
        public abstract void Attack();
        public abstract void CounterAttack();

        public void Start(Player opponent)
        {
            _opponent = opponent;

            initialize(Mode.ValuationBasis);
        }

        public void Shuffle()
        {
            NumAttack = shuffleAttack(NumAttack);
            NumDefense = shuffleDefense(NumDefense);

            byte numNumberCard = (byte) (CardCapacity - NumAttack - NumDefense);
            if (numNumberCard > CardCapacity)
            {
                Log.Error(string.Format("invalid num card; numNumberCard({0}, CardCapacity({1})", numNumberCard, CardCapacity));
                return;
            }

            int NumberCardMaxNo = NumberCard.MAX_NO + 1;    // consider range.exclusive
            for (int i = 0; i < numNumberCard; ++i)
            {
                byte randomNumber = (byte)Random.Range(NumberCard.MIN_NO, NumberCardMaxNo);
                _cardArray[i] = new NumberCard(randomNumber);
            }

            for (int i = numNumberCard; i < (numNumberCard + NumAttack); ++i)
            {
                _cardArray[i] = new Card(Card.EType.Attack);
            }

            for (int i = (numNumberCard + NumAttack); i < CardCapacity; ++i)
            {
                _cardArray[i] = new Card(Card.EType.Defense);
            }

            Log.Debug(string.Format("player id:{0}, num:{1}, att:{2}, def:{3}", Id, numNumberCard, NumAttack, NumDefense));
        }

        public int RemoveAllDefenseOfOpponent()
        {
            int removeCount = 0;

            Card[] oppCardArray = Opponent.CardArray;

            int numCard = oppCardArray.Length;
            for (int i = 0; i < numCard; ++i)
            {
                if (null == oppCardArray[i])  // = draw card
                    continue;

                if (Card.EType.Defense == oppCardArray[i].Type)
                {
                    oppCardArray[i] = null;
                    --Opponent.NumDefense;

                    ++removeCount;
                    Manager.Instance.Object.BoardGameMode.RemoveAllDefenseEvent.Invoke(Opponent.Id, i);
                }
            }

            return removeCount;
        }

        public void Pop(int drawCardIndex)
        {
            if (false == isValid(drawCardIndex))
                return;

            DrawCard = _cardArray[drawCardIndex];
            switch (DrawCard.Type)
            {
                case Card.EType.Attack:
                    Log.Debug(string.Format("player id: {0}, draw card: {1}", Id, DrawCard.Type));
                    --NumAttack;
                    break;

                case Card.EType.Defense:
                    Log.Debug(string.Format("player id: {0}, draw card: {1}", Id, DrawCard.Type));
                    --NumDefense;
                    break;

                case Card.EType.Number:
                    NumberCard numCard = DrawCard as NumberCard;
                    Log.Debug(string.Format("player id: {0}, draw card: {1}", Id, numCard.No));
                    break;

                default:
                    break;
            }

            _cardArray[drawCardIndex] = null;
        }

        public int GetHowMuchPush()
        {
            return Power * BoardGameMode.PUSH_WEIGHT;
        }

        public int GetHowMuchBingo(int bingoCount)
        {
            if (bingoCount <= 0)
                return 0;

            return Power * BoardGameMode.BINGO_WEIGHT * bingoCount;
        }

        public int GetHowMuchAttack()
        {
            return Power * BoardGameMode.ATTACK_WEIGHT;
        }

        protected byte setMaxNumCard(int currentStat)
        {
            const byte divisor = BoardGameMode.MAX_NUM_CARD - BoardGameMode.MIN_NUM_CARD + 1;

            int quotient = Def.MAX_STAT / divisor;

            for (byte i = (divisor - 1); i >= 0; --i)
            {
                int min = quotient * i;
                if (currentStat >= min)
                {
                    return (byte)(i + BoardGameMode.MIN_NUM_CARD);
                }
            }

            return 0;
        }

        protected bool isValid(int cardIndex)
        {
            if (cardIndex < 0)
                return false;
            else if (cardIndex >= _cardArray.Length)
                return false;
            else
                return true;
        }

        private byte shuffleAttack(byte numAttack)
        {
            byte shuffledNumAttack = numAttack;

            for (byte i = numAttack; i < BoardGameMode.MAX_NUM_ATTACK_CARD; ++i)
            {
                int random = Random.Range(0, 100);
                if (random <= _attackShuffleProbability)
                    ++shuffledNumAttack;
            }

            return shuffledNumAttack;
        }

        private byte shuffleDefense(byte numDefense)
        {
            byte shuffledNumDefense = numDefense;

            for (byte i = numDefense; i < BoardGameMode.MAX_NUM_DEFENSE_CARD; ++i)
            {
                int random = Random.Range(0, 100);
                if (random <= _defenseShuffleProbability)
                    ++shuffledNumDefense;
            }

            return shuffledNumDefense;
        }

    }   // class

}   // namespace