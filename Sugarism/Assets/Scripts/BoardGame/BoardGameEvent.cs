namespace BoardGame
{
    public class StartEvent
    {
        public delegate void Handler(UserPlayer user, AIPlayer ai);
        private event Handler _event;

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

    public class ShuffleEvent
    {
        public delegate void Handler();
        private event Handler _event;

        public ShuffleEvent()
        {
            _event = new Handler(onShuffle);
        }

        // default handler
        private void onShuffle()
        {
            Log.Debug("onShuffle");
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

    public class DrawEvent
    {
        public delegate void Handler();
        private event Handler _event;

        public DrawEvent()
        {
            _event = new Handler(onDraw);
        }

        // default handler
        private void onDraw()
        {
            Log.Debug("onDraw");
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
        public delegate void Handler(BoardGameMode.EUserGameState state);
        private event Handler _event;

        public EndEvent()
        {
            _event = new Handler(onEnd);
        }

        // default handler
        private void onEnd(BoardGameMode.EUserGameState state)
        {
            Log.Debug(string.Format("onEnd; UserGameState({0})", state));
        }

        public void Invoke(BoardGameMode.EUserGameState state) { _event.Invoke(state); }

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

    public class BingoEvent
    {
        public delegate void Handler();
        private event Handler _event;

        public BingoEvent()
        {
            _event = new Handler(onBingo);
        }

        // default handler
        private void onBingo()
        {
            Log.Debug("onBingo;");
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

    public class AttackEvent
    {
        public delegate void Handler(int playerId);
        private event Handler _event;

        public AttackEvent()
        {
            _event = new Handler(onAttack);
        }

        // default handler
        private void onAttack(int playerId)
        {
            Log.Debug(string.Format("onAttack; playerId({0})", playerId));
        }

        public void Invoke(int playerId) { _event.Invoke(playerId); }

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

    public class CounterAttackEvent
    {
        public delegate void Handler(int playerId);
        private event Handler _event;

        public CounterAttackEvent()
        {
            _event = new Handler(onCounterAttack);
        }

        // default handler
        private void onCounterAttack(int playerId)
        {
            Log.Debug(string.Format("onCounterAttack; playerId({0})", playerId));
        }

        public void Invoke(int playerId) { _event.Invoke(playerId); }

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

    public class RemoveAllDefenseEvent
    {
        public delegate void Handler(int playerId, int index);
        private event Handler _event;

        public RemoveAllDefenseEvent()
        {
            _event = new Handler(onRemoveAllDefense);
        }

        // default handler
        private void onRemoveAllDefense(int playerId, int index)
        {
            Log.Debug(string.Format("onRemoveAllDefense; playerId({0}), index({1})", playerId, index));
        }

        public void Invoke(int playerId, int index) { _event.Invoke(playerId, index); }

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
        public delegate void Handler(int turn);
        private event Handler _event;

        public TurnChangeEvent()
        {
            _event = new Handler(onTurnChanged);
        }

        // default handler
        private void onTurnChanged(int turn)
        {
            Log.Debug(string.Format("onTurnChanged; {0}", turn));
        }

        public void Invoke(int turn) { _event.Invoke(turn); }

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

    public class CriterionChangeEvent
    {
        public delegate void Handler(BoardGameMode.ENumberCriterion criterion);
        private event Handler _event;

        public CriterionChangeEvent()
        {
            _event = new Handler(onCriterionChanged);
        }

        // default handler
        private void onCriterionChanged(BoardGameMode.ENumberCriterion criterion)
        {
            Log.Debug(string.Format("onCriterionChanged; {0}", criterion));
        }

        public void Invoke(BoardGameMode.ENumberCriterion criterion) { _event.Invoke(criterion); }

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

    public class FlowChangeEvent
    {
        public delegate void Handler(int flow);
        private event Handler _event;

        public FlowChangeEvent()
        {
            _event = new Handler(onFlowChanged);
        }

        // default handler
        private void onFlowChanged(int flow)
        {
            Log.Debug(string.Format("onFlowChanged; {0}", flow));
        }

        public void Invoke(int flow) { _event.Invoke(flow); }

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

    public class CellOwnerChangeEvent
    {
        public delegate void Handler(int row, int col, Cell.EOwner owner);
        private event Handler _event;

        public CellOwnerChangeEvent()
        {
            _event = new Handler(onCellOwnerChanged);
        }

        // default handler
        private void onCellOwnerChanged(int row, int col, Cell.EOwner owner)
        {
            Log.Debug(string.Format("onCellOwnerChanged; (row, col) = ({0}, {1}), owner({2})", row, col, owner));
        }

        public void Invoke(int row, int col, Cell.EOwner owner) { _event.Invoke(row, col, owner); }

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

    public class CellBingoChangeEvent
    {
        public delegate void Handler(int row, int col, bool isBingo);
        private event Handler _event;

        public CellBingoChangeEvent()
        {
            _event = new Handler(onCellBingoChanged);
        }

        // default handler
        private void onCellBingoChanged(int row, int col, bool isBingo)
        {
            Log.Debug(string.Format("onCellBingoChanged; (row, col) = ({0}, {1}), isBingo({2})", row, col, isBingo));
        }

        public void Invoke(int row, int col, bool isBingo) { _event.Invoke(row, col, isBingo); }

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
}
