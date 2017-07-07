using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Combat
{
    public class CombatMode
    {
        // const
        public const byte MIN_REMAIN_TURN = 0;
        public const byte INIT_REMAIN_TURN = 10;
        public const byte URGENT_REMAIN_TURN = 3;

        public enum EUserGameState { Unknown, Win, Lose, MAX }

        //
        private byte _remainTurn = 0;
        public byte RemainTurn
        {
            get { return _remainTurn; }
            private set
            {
                _remainTurn = value;

                if (_remainTurn < MIN_REMAIN_TURN)
                    _remainTurn = MIN_REMAIN_TURN;

                TurnChangeEvent.Invoke(_remainTurn);
            }
        }

        //
        private UserPlayer _user = null;
        public UserPlayer UserPlayer { get { return _user; } }

        private AIPlayer _ai = null;

        #region Events

        private StartEvent _startEvent = null;
        public StartEvent StartEvent { get { return _startEvent; } }

        private StartUserBattleEvent _startUserBattleEvent = null;
        public StartUserBattleEvent StartUserBattleEvent { get { return _startUserBattleEvent; } }

        private EndUserBattleEvent _endUserBattleEvent = null;
        public EndUserBattleEvent EndUserBattleEvent { get { return _endUserBattleEvent; } }

        private StartAIBattleEvent _startAIBattleEvent = null;
        public StartAIBattleEvent StartAIBattleEvent { get { return _startAIBattleEvent; } }

        private EndAIBattleEvent _endAIBattleEvent = null;
        public EndAIBattleEvent EndAIBattleEvent { get { return _endAIBattleEvent; } }

        private EndEvent _endEvent = null;
        public EndEvent EndEvent { get { return _endEvent; } }

        private TurnChangeEvent _turnChangeEvent = null;
        public TurnChangeEvent TurnChangeEvent { get { return _turnChangeEvent; } }

        private HpChangeEvent _hpChangeEvent = null;
        public HpChangeEvent HpChangeEvent { get { return _hpChangeEvent; } }

        private MpChangeEvent _mpChangeEvent = null;
        public MpChangeEvent MpChangeEvent { get { return _mpChangeEvent; } }

        private CriticalAttackEvent _criticalAttackEvent = null;
        public CriticalAttackEvent CriticalAttackEvent { get { return _criticalAttackEvent; } }

        private AttackEvent _attackEvent = null;
        public AttackEvent AttackEvent { get { return _attackEvent; } }

        private CriticalTrickEvent _criticalTrickEvent = null;
        public CriticalTrickEvent CriticalTrickEvent { get { return _criticalTrickEvent; } }

        private TrickEvent _trickEvent = null;
        public TrickEvent TrickEvent { get { return _trickEvent; } }

        #endregion


        // constructor
        public CombatMode()
        {
            _startEvent = new StartEvent();
            _startUserBattleEvent = new StartUserBattleEvent();
            _endUserBattleEvent = new EndUserBattleEvent();
            _startAIBattleEvent = new StartAIBattleEvent();
            _endAIBattleEvent = new EndAIBattleEvent();
            _endEvent = new EndEvent();
            _turnChangeEvent = new TurnChangeEvent();
            _hpChangeEvent = new HpChangeEvent();
            _mpChangeEvent = new MpChangeEvent();
            _criticalAttackEvent = new CriticalAttackEvent();
            _attackEvent = new AttackEvent();
            _criticalTrickEvent = new CriticalTrickEvent();
            _trickEvent = new TrickEvent();
        }

        public void Start(int playerId)
        {
            if (false == ExtCombatPlayer.IsValid(playerId))
            {
                Log.Error(string.Format("invalid player id: {0}", playerId));
                return;
            }
            
            UserPlayer user = new UserPlayer(this);
            AIPlayer ai = new AIPlayer(this, playerId);

            start(user, ai);
        }

        public void Start(UserPlayer userPlayer, AIPlayer aiPlayer)
        {
            if (null == userPlayer)
            {
                Log.Error("not found user player");
                return;
            }

            if (null == aiPlayer)
            {
                Log.Error("not found ai player");
                return;
            }

            start(userPlayer, aiPlayer);
        }

        private void start(UserPlayer userPlayer, AIPlayer aiPlayer)
        {
            initialize();

            _user = userPlayer;
            _ai = aiPlayer;

            _user.Start(_ai);
            _ai.Start(_user);

            StartEvent.Invoke(_user, _ai);
        }

        private IEnumerator _battleIterator = null;
        public void StartBattleRoutine()
        {
            _battleIterator = Battle();
            BattleIterate();
        }

        public void BattleIterate()
        {
            if (null == _battleIterator)
                return;

            if (_battleIterator.MoveNext())
                return;

            EUserGameState state = isOver();
            switch (state)
            {
                case EUserGameState.Win:
                case EUserGameState.Lose:
                    end(state);
                    return;

                default:
                    StartBattleRoutine();
                    break;
            }
        }
        
        IEnumerator Battle()
        {
            StartUserBattleEvent.Invoke();
            yield return null;

            // EndUserBattleEvent.Invoke -> from UI.

            EUserGameState userBattleResult = isOver();
            if (EUserGameState.Unknown != userBattleResult)
                yield break;

            StartAIBattleEvent.Invoke();
            yield return null;

            _ai.Battle();
            yield return null;

            EndAIBattleEvent.Invoke();

            EUserGameState aiBattleResult = isOver();
            if (EUserGameState.Unknown != aiBattleResult)
                yield break;

            --RemainTurn;
        }

        private void end(EUserGameState state)
        {
            EndEvent.Invoke(state);
        }

        private void initialize()
        {
            RemainTurn = INIT_REMAIN_TURN;

            _battleIterator = null;
        }

        // NOTE : Do NOT return EUserGameState.MAX
        private EUserGameState isOver()
        {
            if (_user.Hp <= 0)
                return EUserGameState.Lose;
            else if (_ai.Hp <= 0)
                return EUserGameState.Win;

            if (RemainTurn <= MIN_REMAIN_TURN)
            {
                if (_user.Hp > _ai.Hp)
                    return EUserGameState.Win;
                else if (_user.Hp == _ai.Hp)
                    return EUserGameState.Lose; // is cruel?
                else
                    return EUserGameState.Lose;
            }

            return EUserGameState.Unknown;
        }

    }   // class

}   // namespace
