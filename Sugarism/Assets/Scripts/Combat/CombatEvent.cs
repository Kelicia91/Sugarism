using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Combat
{
    public class StartEvent
    {
        public delegate void Handler(UserPlayer user, AIPlayer ai);
        private event Handler _event = null;

        public StartEvent()
        {
            _event = new Handler(onStart);
        }

        // default handler
        private void onStart(UserPlayer user, AIPlayer ai)
        {
            Log.Debug(string.Format("onStart; user({0}), ai({1})", user.Name, ai.Name));
        }

        public void Invoke(UserPlayer user, AIPlayer ai) { _event.Invoke(user, ai); }

        public void Attach(Handler handler)
        {
            if (null == handler)
                return;

            _event += handler;
        }

        public void Detach(Handler handler)
        {
            if (null == handler)
                return;

            _event -= handler;
        }
    }

    public class StartUserBattleEvent
    {
        public delegate void Handler();
        private event Handler _event = null;

        public StartUserBattleEvent()
        {
            _event = new Handler(onStartUserBattle);
        }

        // default handler
        private void onStartUserBattle()
        {
            Log.Debug("onStartUserBattle;");
        }

        public void Invoke() { _event.Invoke(); }

        public void Attach(Handler handler)
        {
            if (null == handler)
                return;

            _event += handler;
        }

        public void Detach(Handler handler)
        {
            if (null == handler)
                return;

            _event -= handler;
        }
    }

    public class EndUserBattleEvent
    {
        public delegate void Handler();
        private event Handler _event = null;

        public EndUserBattleEvent()
        {
            _event = new Handler(onEndUserBattle);
        }

        // default handler
        private void onEndUserBattle()
        {
            Log.Debug("onEndUserBattle;");
        }

        public void Invoke() { _event.Invoke(); }

        public void Attach(Handler handler)
        {
            if (null == handler)
                return;

            _event += handler;
        }

        public void Detach(Handler handler)
        {
            if (null == handler)
                return;

            _event -= handler;
        }
    }

    public class StartAIBattleEvent
    {
        public delegate void Handler();
        private event Handler _event = null;

        public StartAIBattleEvent()
        {
            _event = new Handler(onStartAIBattle);
        }

        // default handler
        private void onStartAIBattle()
        {
            Log.Debug("onStartAIBattle;");
        }

        public void Invoke() { _event.Invoke(); }

        public void Attach(Handler handler)
        {
            if (null == handler)
                return;

            _event += handler;
        }

        public void Detach(Handler handler)
        {
            if (null == handler)
                return;

            _event -= handler;
        }
    }

    public class EndAIBattleEvent
    {
        public delegate void Handler();
        private event Handler _event = null;

        public EndAIBattleEvent()
        {
            _event = new Handler(onEndAIBattle);
        }

        // default handler
        private void onEndAIBattle()
        {
            Log.Debug("onEndAIBattle;");
        }

        public void Invoke() { _event.Invoke(); }

        public void Attach(Handler handler)
        {
            if (null == handler)
                return;

            _event += handler;
        }

        public void Detach(Handler handler)
        {
            if (null == handler)
                return;

            _event -= handler;
        }
    }

    public class EndEvent
    {
        public delegate void Handler(CombatMode.EUserGameState state);
        private event Handler _event = null;

        public EndEvent()
        {
            _event = new Handler(onEnd);
        }

        // default handler
        private void onEnd(CombatMode.EUserGameState state)
        {
            Log.Debug(string.Format("onEnd; UserGameState({0})", state));
        }

        public void Invoke(CombatMode.EUserGameState state) { _event.Invoke(state); }

        public void Attach(Handler handler)
        {
            if (null == handler)
                return;

            _event += handler;
        }

        public void Detach(Handler handler)
        {
            if (null == handler)
                return;

            _event -= handler;
        }
    }

    public class TurnChangeEvent
    {
        public delegate void Handler(byte turn);
        private event Handler _event = null;

        public TurnChangeEvent()
        {
            _event = new Handler(onTurnChanged);
        }

        // default handler
        private void onTurnChanged(byte turn)
        {
            Log.Debug(string.Format("onTurnChanged; {0}", turn));
        }

        public void Invoke(byte turn) { _event.Invoke(turn); }

        public void Attach(Handler handler)
        {
            if (null == handler)
                return;

            _event += handler;
        }

        public void Detach(Handler handler)
        {
            if (null == handler)
                return;

            _event -= handler;
        }
    }

    public class HpChangeEvent
    {
        public delegate void Handler(int playerId, int hp);
        private event Handler _event = null;

        public HpChangeEvent()
        {
            _event = new Handler(onHpChanged);
        }

        // default handler
        private void onHpChanged(int playerId, int hp)
        {
            Log.Debug(string.Format("onHpChanged; player id({0}), hp({1})", playerId, hp));
        }

        public void Invoke(int playerId, int hp) { _event.Invoke(playerId, hp); }

        public void Attach(Handler handler)
        {
            if (null == handler)
                return;

            _event += handler;
        }

        public void Detach(Handler handler)
        {
            if (null == handler)
                return;

            _event -= handler;
        }
    }

    public class MpChangeEvent
    {
        public delegate void Handler(int playerId, int mp);
        private event Handler _event = null;

        public MpChangeEvent()
        {
            _event = new Handler(onMpChanged);
        }

        // default handler
        private void onMpChanged(int playerId, int mp)
        {
            Log.Debug(string.Format("onMpChanged; player id({0}), mp({1})", playerId, mp));
        }

        public void Invoke(int playerId, int mp) { _event.Invoke(playerId, mp); }

        public void Attach(Handler handler)
        {
            if (null == handler)
                return;

            _event += handler;
        }

        public void Detach(Handler handler)
        {
            if (null == handler)
                return;

            _event -= handler;
        }
    }

    public class CriticalAttackEvent
    {
        public delegate void Handler(int playerId, int damage);
        private event Handler _event = null;

        public CriticalAttackEvent()
        {
            _event = new Handler(onCriticalAttack);
        }

        // default handler
        private void onCriticalAttack(int playerId, int damage)
        {
            Log.Debug(string.Format("onCriticalAttack; playerId({0}), damage({1})", playerId, damage));
        }

        public void Invoke(int playerId, int damage) { _event.Invoke(playerId, damage); }

        public void Attach(Handler handler)
        {
            if (null == handler)
                return;

            _event += handler;
        }

        public void Detach(Handler handler)
        {
            if (null == handler)
                return;

            _event -= handler;
        }
    }

    public class AttackEvent
    {
        public delegate void Handler(int playerId, int damage);
        private event Handler _event = null;

        public AttackEvent()
        {
            _event = new Handler(onAttack);
        }

        // default handler
        private void onAttack(int playerId, int damage)
        {
            Log.Debug(string.Format("onAttack; playerId({0}), damage({1})", playerId, damage));
        }

        public void Invoke(int playerId, int damage) { _event.Invoke(playerId, damage); }

        public void Attach(Handler handler)
        {
            if (null == handler)
                return;

            _event += handler;
        }

        public void Detach(Handler handler)
        {
            if (null == handler)
                return;

            _event -= handler;
        }
    }

    public class CriticalTrickEvent
    {
        public delegate void Handler(int playerId, int damage);
        private event Handler _event = null;

        public CriticalTrickEvent()
        {
            _event = new Handler(onCriticalTrick);
        }

        // default handler
        private void onCriticalTrick(int playerId, int damage)
        {
            Log.Debug(string.Format("onCriticalTrick; playerId({0}), damage({1})", playerId, damage));
        }

        public void Invoke(int playerId, int damage) { _event.Invoke(playerId, damage); }

        public void Attach(Handler handler)
        {
            if (null == handler)
                return;

            _event += handler;
        }

        public void Detach(Handler handler)
        {
            if (null == handler)
                return;

            _event -= handler;
        }
    }

    public class TrickEvent
    {
        public delegate void Handler(int playerId, int damage);
        private event Handler _event = null;

        public TrickEvent()
        {
            _event = new Handler(onTrick);
        }

        // default handler
        private void onTrick(int playerId, int damage)
        {
            Log.Debug(string.Format("onTrick; playerId({0}), damage({1})", playerId, damage));
        }

        public void Invoke(int playerId, int damage) { _event.Invoke(playerId, damage); }

        public void Attach(Handler handler)
        {
            if (null == handler)
                return;

            _event += handler;
        }

        public void Detach(Handler handler)
        {
            if (null == handler)
                return;

            _event -= handler;
        }
    }

}   // namespace
