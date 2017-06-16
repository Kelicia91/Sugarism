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

        private JudgeEvent _judgeEvent = null;
        public JudgeEvent JudgeEvent { get { return _judgeEvent; } }

        private EndEvent _endEvent = null;
        public EndEvent EndEvent { get { return _endEvent; } }

        private RemoveAllDefenseEvent _removeAllDefenseEvent = null;
        public RemoveAllDefenseEvent RemoveAllDefenseEvent { get { return _removeAllDefenseEvent; } }

        private TurnChangeEvent _turnChangeEvent = null;
        public TurnChangeEvent TurnChangeEvent { get { return _turnChangeEvent; } }

        private CriterionChangeEvent _criterionChangeEvent = null;
        public CriterionChangeEvent CriterionChangeEvent { get { return _criterionChangeEvent; } }

        private FlowChangeEvent _flowChangeEvent = null;
        public FlowChangeEvent FlowChangeEvent { get { return _flowChangeEvent; } }

        private CellOwnerChangeEvent _boardUnitOwnerChangeEvent = null;
        public CellOwnerChangeEvent BoardUnitOwnerChangeEvent { get { return _boardUnitOwnerChangeEvent; } }

        private CellBingoChangeEvent _boardUnitBingoChangeEvent = null;
        public CellBingoChangeEvent BoardUnitBingoChangeEvent { get { return _boardUnitBingoChangeEvent; } }

        #endregion


        // constructor
        public BoardGameMode()
        {
            _startEvent = new StartEvent();
            _shuffleEvent = new ShuffleEvent();
            _drawEvent = new DrawEvent();
            _judgeEvent = new JudgeEvent();
            _endEvent = new EndEvent();
            _removeAllDefenseEvent = new RemoveAllDefenseEvent();
            _turnChangeEvent = new TurnChangeEvent();
            _criterionChangeEvent = new CriterionChangeEvent();
            _flowChangeEvent = new FlowChangeEvent();
            _boardUnitOwnerChangeEvent = new CellOwnerChangeEvent();
            _boardUnitBingoChangeEvent = new CellBingoChangeEvent();

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

            Draw();
        }

        public void Draw()
        {
            _ai.Draw();

            DrawEvent.Invoke();
        }

        public void Judge()
        {
            judge();

            --RemainTurn;

            EUserGameState state = isOver();
            switch (state)
            {
                case EUserGameState.Win:
                case EUserGameState.Lose:
                    End(state);
                    break;

                default:
                    Shuffle();
                    break;
            }
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


        private void judge()
        {
            Card.EType userCardType = _user.DrawCard.Type;
            Card.EType aiCardType = _ai.DrawCard.Type;

            switch (userCardType)
            {
                case Card.EType.Number:
                    switch (aiCardType)
                    {
                        case Card.EType.Number:
                            judgeUserNumAINum();
                            break;

                        case Card.EType.Attack:
                            judgeUserNumAIAttack();
                            break;

                        case Card.EType.Defense:
                            judgeUserNumAIDefense();
                            break;

                        default:
                            Log.Error("invalid card type that ai draw");
                            return;
                    }
                    break;

                case Card.EType.Attack:
                    switch (aiCardType)
                    {
                        case Card.EType.Number:
                            judgeUserAttackAINum();
                            break;

                        case Card.EType.Attack:
                            judgeUserAttackAIAttack();
                            break;

                        case Card.EType.Defense:
                            judgeUserAttackAIDefense();
                            break;

                        default:
                            Log.Error("invalid card type that ai draw");
                            return;
                    }
                    break;

                case Card.EType.Defense:
                    switch (aiCardType)
                    {
                        case Card.EType.Number:
                            judgeUserDefenseAINum();
                            break;

                        case Card.EType.Attack:
                            judgeUserDefenseAIAttack();
                            break;

                        case Card.EType.Defense:
                            judgeUserDefenseAIDefense();
                            break;

                        default:
                            Log.Error("invalid card type that ai draw");
                            return;
                    }
                    break;

                default:
                    Log.Error("invalid card type that user draw");
                    return;
            }
        }

        /*** user.defense ***/
        private void judgeUserDefenseAINum()
        {
            NumberCard aiNumberCard = _ai.DrawCard as NumberCard;
            Log.Debug(string.Format("judge; user(D), ai({0})", aiNumberCard.No));

            _user.RemoveAllDefenseOfOpponent();

            _ai.Push();
            if (EUserGameState.Unknown != isOver())
                return;

            processBoard(aiNumberCard.No, _ai);
        }

        private void judgeUserDefenseAIAttack()
        {
            Log.Debug("judge; user(D), ai(A)");

            _user.CounterAttack();
        }

        private void judgeUserDefenseAIDefense()
        {
            Log.Debug("judge; user(D), ai(D)");

            _user.RemoveAllDefenseOfOpponent();
            _ai.RemoveAllDefenseOfOpponent();
        }

        /*** user.attack ***/
        private void judgeUserAttackAINum()
        {
            Log.Debug("judge; user(A), ai(N)");

            _user.Attack();
        }

        private void judgeUserAttackAIAttack()
        {
            Log.Debug("judge; user(A), ai(A)");
        }

        private void judgeUserAttackAIDefense()
        {
            Log.Debug("judge; user(A), ai(D)");

            _ai.CounterAttack();
        }

        /*** user.number ***/
        private void judgeUserNumAIAttack()
        {
            Log.Debug("judge; user(N), ai(A)");

            _ai.Attack();
        }

        private void judgeUserNumAIDefense()
        {
            NumberCard userNumberCard = _user.DrawCard as NumberCard;
            Log.Debug(string.Format("judge; user({0}), ai(D)", userNumberCard.No));

            _ai.RemoveAllDefenseOfOpponent();

            _user.Push();
            if (EUserGameState.Unknown != isOver())
                return;

            processBoard(userNumberCard.No, _user);
        }

        private void judgeUserNumAINum()
        {
            Player superiority;
            byte superNum;

            Player inferiority;
            byte inferNum;

            bool isCompared = compare(out superiority, out superNum, out inferiority, out inferNum);
            
            if (false == isCompared)
                return; // tie or error
            
            superiority.Push();
            if (EUserGameState.Unknown != isOver())
                return;

            EUserGameState state = processBoard(superNum, superiority);
            if (EUserGameState.Unknown != state)
                return;

            processBoard(inferNum, inferiority);
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

        private EUserGameState processBoard(byte num, Player player)
        {
            _board.SetCellOwner(num, player.Owner);

            int count = _board.GetBingoCount(player.Owner);
            if (count <= 0)
                return EUserGameState.Unknown;

            player.Bingo(count);

            EUserGameState state = isOver();
            if (EUserGameState.Unknown != state)
                return state;

            switchCriterion();
            _board.InitBingo();

            return EUserGameState.Unknown;
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