using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardGame
{
    public class BoardGameMode
    {
        // const
        public const byte MIN_NUM_CARD = 3;
        public const byte MAX_NUM_CARD = 6;

        public const byte MAX_NUM_ATTACK_CARD = 1;
        public const byte MAX_NUM_DEFENSE_CARD = 1;

        public const int DEFAULT_ATTACK_CARD_SHUFFLE_PROBABILITY = 10;
        public const int DEFAULT_DEFENSE_CARD_SHUFFLE_PROBABILITY = 10;
        public const int STAT_WEIGHT = 20;

        public const byte PUSH_WEIGHT = 1;
        public const byte BINGO_WEIGHT = 2;
        public const byte ATTACK_WEIGHT = 2;

        public const int MIN_FLOW = 0;
        public const int MAX_FLOW = 100;
        public const int INIT_FLOW = 50;

        public const int INIT_REMAIN_TURN = 10;
        public const int URGENT_REMAIN_TURN = 3;

        public enum ENumberCriterion { Low, High, MAX }
        public const ENumberCriterion INIT_CRITERION = ENumberCriterion.High;

        public enum EUserGameState { Unknown, Win, Lose }

        // 1 vs 1
        private UserPlayer _user = null;
        private AIPlayer _ai = null;

        private Board _board = null;
        public Board Board { get { return _board; } }

        private int _flow = MIN_FLOW;
        public int Flow
        {
            get { return _flow; }
            private set
            {
                _flow = value;

                if (_flow < MIN_FLOW)
                    _flow = MIN_FLOW;
                else if (_flow > MAX_FLOW)
                    _flow = MAX_FLOW;

                FlowChangeEvent.Invoke(_flow);
            }
        }

        private int _remainTurn = INIT_REMAIN_TURN;
        public int RemainTurn
        {
            get { return _remainTurn; }
            private set
            {
                _remainTurn = value;

                if (_remainTurn < 0)
                    _remainTurn = 0;

                TurnChangeEvent.Invoke(_remainTurn);
            }
        }

        private ENumberCriterion _criterion = ENumberCriterion.MAX;
        public ENumberCriterion Criterion
        {
            get { return _criterion; }
            private set
            {
                _criterion = value;
                CriterionChangeEvent.Invoke(_criterion);
            }
        }


        #region Events

        private StartEvent _startEvent = null;
        public StartEvent StartEvent { get { return _startEvent; } }

        private ShuffleEvent _shuffleEvent = null;
        public ShuffleEvent ShuffleEvent { get { return _shuffleEvent; } }

        private DrawEvent _drawEvent = null;
        public DrawEvent DrawEvent { get { return _drawEvent; } }

        private EndEvent _endEvent = null;
        public EndEvent EndEvent { get { return _endEvent; } }

        private BingoEvent _bingoEvent = null;
        public BingoEvent BingoEvent { get { return _bingoEvent; } }

        private AttackEvent _attackEvent = null;
        public AttackEvent AttackEvent { get { return _attackEvent; } }

        private CounterAttackEvent _counterAttackEvent = null;
        public CounterAttackEvent CounterAttackEvent { get { return _counterAttackEvent; } }

        private RemoveAllDefenseEvent _removeAllDefenseEvent = null;
        public RemoveAllDefenseEvent RemoveAllDefenseEvent { get { return _removeAllDefenseEvent; } }

        private TurnChangeEvent _turnChangeEvent = null;
        public TurnChangeEvent TurnChangeEvent { get { return _turnChangeEvent; } }

        private CriterionChangeEvent _criterionChangeEvent = null;
        public CriterionChangeEvent CriterionChangeEvent { get { return _criterionChangeEvent; } }

        private FlowChangeEvent _flowChangeEvent = null;
        public FlowChangeEvent FlowChangeEvent { get { return _flowChangeEvent; } }

        private CellOwnerChangeEvent _cellOwnerChangeEvent = null;
        public CellOwnerChangeEvent CellOwnerChangeEvent { get { return _cellOwnerChangeEvent; } }

        private CellBingoChangeEvent _cellBingoChangeEvent = null;
        public CellBingoChangeEvent CellBingoChangeEvent { get { return _cellBingoChangeEvent; } }

        #endregion


        // constructor
        public BoardGameMode()
        {
            _startEvent = new StartEvent();
            _shuffleEvent = new ShuffleEvent();
            _drawEvent = new DrawEvent();
            _endEvent = new EndEvent();

            _bingoEvent = new BingoEvent();
            _attackEvent = new AttackEvent();
            _counterAttackEvent = new CounterAttackEvent();
            _removeAllDefenseEvent = new RemoveAllDefenseEvent();
            _turnChangeEvent = new TurnChangeEvent();
            _criterionChangeEvent = new CriterionChangeEvent();
            _flowChangeEvent = new FlowChangeEvent();
            _cellOwnerChangeEvent = new CellOwnerChangeEvent();
            _cellBingoChangeEvent = new CellBingoChangeEvent();

            _board = new Board();
        }


        public void Start(int playerId)
        {
            if (false == ExtBoardGamePlayer.isValid(playerId))
            {
                Log.Error(string.Format("invalid player id ({0})", playerId));
                return;
            }

            initialize();

            _user = new UserPlayer(this);
            _ai = new AIPlayer(this, playerId);

            _user.Start(_ai);
            _ai.Start(_user);
            
            StartEvent.Invoke(_user, _ai);
        }

        public void Shuffle()
        {
            _ai.Shuffle();
            _user.Shuffle();

            ShuffleEvent.Invoke();

            _ai.Draw();
        }

        private IEnumerator _iterJudge = null;
        public void StartJudge()
        {
            _iterJudge = Judge();   // @NOTE: not support operation Reset();
            JudgeIter();
        }

        public void JudgeIter()
        {
            if (null == _iterJudge)
                return;

            if (_iterJudge.MoveNext())
                return;

            EUserGameState state = isOver();
            switch (state)
            {
                case EUserGameState.Win:
                case EUserGameState.Lose:
                    End(state);
                    return;

                default:
                    Shuffle();
                    break;
            }
        }
        
        IEnumerator Judge()
        {
            IEnumerator iter = judge();
            while (iter.MoveNext())
                yield return null;
            
            --RemainTurn;
        }

        public void End(EUserGameState state)
        {
            EndEvent.Invoke(state);
        }

        public void PlusFlow(int value)
        {
            if (value <= 0)
                return;

            Flow += value;
        }

        public void MinusFlow(int value)
        {
            if (value <= 0)
                return;

            Flow -= value;
        }


        private void initialize()
        {
            _board.Initialize();

            Flow = INIT_FLOW;
            RemainTurn = INIT_REMAIN_TURN;
            Criterion = INIT_CRITERION;
        }


        IEnumerator judge()
        {
            Card.EType userCardType = _user.DrawCard.Type;
            Card.EType aiCardType = _ai.DrawCard.Type;

            if ((Card.EType.Number == userCardType) && (Card.EType.Number == aiCardType))
            {
                IEnumerator iter = judgeUserNumAINum();
                while (iter.MoveNext())
                    yield return null;
                yield break;
            }
            else if ((Card.EType.Number == userCardType) && (Card.EType.Attack == aiCardType))
            {
                IEnumerator iter = judgeUserNumAIAttack();
                while (iter.MoveNext())
                    yield return null;
                yield break;
            }
            else if ((Card.EType.Number == userCardType) && (Card.EType.Defense == aiCardType))
            {
                IEnumerator iter = judgeUserNumAIDefense();
                while (iter.MoveNext())
                    yield return null;
                yield break;
            }
            else if ((Card.EType.Attack == userCardType) && (Card.EType.Number == aiCardType))
            {
                IEnumerator iter = judgeUserAttackAINum();
                while (iter.MoveNext())
                    yield return null;
                yield break;
            }
            else if ((Card.EType.Attack == userCardType) && (Card.EType.Attack == aiCardType))
            {
                judgeUserAttackAIAttack();
                yield break;
            }
            else if ((Card.EType.Attack == userCardType) && (Card.EType.Defense == aiCardType))
            {
                IEnumerator iter = judgeUserAttackAIDefense();
                while (iter.MoveNext())
                    yield return null;
                yield break;
            }
            else if ((Card.EType.Defense == userCardType) && (Card.EType.Number == aiCardType))
            {
                IEnumerator iter = judgeUserDefenseAINum();
                while (iter.MoveNext())
                    yield return null;
                yield break;
            }
            else if ((Card.EType.Defense == userCardType) && (Card.EType.Attack == aiCardType))
            {
                IEnumerator iter = judgeUserDefenseAIAttack();
                while (iter.MoveNext())
                    yield return null;
                yield break;
            }
            else if ((Card.EType.Defense == userCardType) && (Card.EType.Defense == aiCardType))
            {
                IEnumerator iter = judgeUserDefenseAIDefense();
                while (iter.MoveNext())
                    yield return null;
                yield break;
            }
            else
            {
                Log.Error("invalid card type to draw");
                yield break;
            }
        }

        /*** user.defense ***/
        IEnumerator judgeUserDefenseAINum()
        {
            NumberCard aiNumberCard = _ai.DrawCard as NumberCard;
            Log.Debug(string.Format("judge; user(D), ai({0})", aiNumberCard.No));

            int removeCount = _user.RemoveAllDefenseOfOpponent();
            if (removeCount > 0)
                yield return null;

            _ai.Push();
            yield return null;

            if (EUserGameState.Unknown != isOver())
                yield break;

            IEnumerator iter = processBoard(aiNumberCard.No, _ai);
            while (iter.MoveNext())
                yield return null;
        }

        IEnumerator judgeUserDefenseAIAttack()
        {
            Log.Debug("judge; user(D), ai(A)");

            _user.CounterAttack();
            yield return null;
        }

        IEnumerator judgeUserDefenseAIDefense()
        {
            Log.Debug("judge; user(D), ai(D)");

            int removeCount = 0;
            removeCount = _user.RemoveAllDefenseOfOpponent();
            if (removeCount > 0)
                yield return null;

            removeCount = _ai.RemoveAllDefenseOfOpponent();
            if (removeCount > 0)
                yield return null;
        }

        /*** user.attack ***/
        IEnumerator judgeUserAttackAINum()
        {
            Log.Debug("judge; user(A), ai(N)");

            _user.Attack();
            yield return null;
        }

        void judgeUserAttackAIAttack()
        {
            Log.Debug("judge; user(A), ai(A)");
        }

        IEnumerator judgeUserAttackAIDefense()
        {
            Log.Debug("judge; user(A), ai(D)");

            _ai.CounterAttack();
            yield return null;
        }

        /*** user.number ***/
        IEnumerator judgeUserNumAIAttack()
        {
            Log.Debug("judge; user(N), ai(A)");

            _ai.Attack();
            yield return null;
        }

        IEnumerator judgeUserNumAIDefense()
        {
            NumberCard userNumberCard = _user.DrawCard as NumberCard;
            Log.Debug(string.Format("judge; user({0}), ai(D)", userNumberCard.No));

            int removeCount = _ai.RemoveAllDefenseOfOpponent();
            if (removeCount > 0)
                yield return null;

            _user.Push();
            yield return null;

            if (EUserGameState.Unknown != isOver())
                yield break;

            IEnumerator iter= processBoard(userNumberCard.No, _user);
            while (iter.MoveNext())
                yield return null;
        }

        IEnumerator judgeUserNumAINum()
        {
            Player superiority;
            byte superNum;

            Player inferiority;
            byte inferNum;

            bool isCompared = compare(out superiority, out superNum, out inferiority, out inferNum);
            
            if (false == isCompared)
            {
                // tie or error
                yield break;
            }
            
            superiority.Push();
            yield return null;

            if (EUserGameState.Unknown != isOver())
                yield break;

            IEnumerator superIter = processBoard(superNum, superiority);
            while (superIter.MoveNext())
                yield return null;

            if (EUserGameState.Unknown != isOver())
                yield break;

            IEnumerator inferIter = processBoard(inferNum, inferiority);
            while (inferIter.MoveNext())
                yield return null;
        }

        private bool compare(out Player superiority, out byte superNum,
                            out Player inferiority, out byte inferNum)
        {
            superiority = null;
            superNum = 0;

            inferiority = null;
            inferNum = 0;

            NumberCard userNumCard = _user.DrawCard as NumberCard;
            byte userNum = userNumCard.No;

            NumberCard aiNumCard = _ai.DrawCard as NumberCard;
            byte aiNum = aiNumCard.No;

            Log.Debug(string.Format("judge; user({0}), ai({1})", userNum, aiNum));

            if (userNum == aiNum)
                return false; // tie

            switch (Criterion)
            {
                case ENumberCriterion.Low:
                    {
                        if (userNum < aiNum)
                        {
                            superiority = _user;    superNum = userNum;
                            inferiority = _ai;      inferNum = aiNum;
                        }
                        else
                        {
                            superiority = _ai;      superNum = aiNum;
                            inferiority = _user;    inferNum = userNum;
                        }
                    }
                    return true;

                case ENumberCriterion.High:
                    {
                        if (userNum < aiNum)
                        {
                            superiority = _ai;      superNum = aiNum;
                            inferiority = _user;    inferNum = userNum;
                        }
                        else
                        {
                            superiority = _user;    superNum = userNum;
                            inferiority = _ai;      inferNum = aiNum;
                        }
                    }
                    return true;

                default:
                    {
                        Log.Error("invalid number criterion");
                    }
                    return false;
            }
        }

        IEnumerator processBoard(byte num, Player player)
        {
            _board.SetCellOwner(num, player.Owner);
            yield return null;

            int count = _board.GetBingoCount(player.Owner);
            if (count <= 0)
                yield break;

            BingoEvent.Invoke();
            yield return null;

            player.Bingo(count);
            yield return null;

            if (EUserGameState.Unknown != isOver())
                yield break;

            switchCriterion();
            _board.InitBingo();
        }

        private void switchCriterion()
        {
            switch (Criterion)
            {
                case ENumberCriterion.Low:
                    Criterion = ENumberCriterion.High;
                    break;

                case ENumberCriterion.High:
                    Criterion = ENumberCriterion.Low;
                    break;

                default:
                    return;
            }
        }

        private EUserGameState isOver()
        {
            if (Flow <= MIN_FLOW)
            {
                return EUserGameState.Lose;
            }                
            else if (Flow >= MAX_FLOW)
            {
                return EUserGameState.Win;
            }
            else if (RemainTurn <= 0)
            {
                if (Flow > INIT_FLOW)
                    return EUserGameState.Win;
                else if (Flow < INIT_FLOW)
                    return EUserGameState.Lose;
                else
                    return EUserGameState.Lose; // is it cruel?
            }
            else
            {
                return EUserGameState.Unknown;
            }
        }

    }   // class
    
}   // namespace