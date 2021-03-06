﻿using System.Collections;

namespace Exam
{
    public enum EType
    {
        COMBAT = 0,
        BOARD_GAME_TRICKER,
        BOARD_GAME_POLITICIAN,
        SCORE,

        MAX
    }

    // Nurture.Action.Lesson <---> (Exam) <---> Mode
    public abstract class Exam
    {
        //
        private EType _type = EType.MAX;
        public EType Type { get { return _type; } }

        private int _id = -1;
        public int Id { get { return _id; } }

        private int _npcId = -1;
        public int NPCId { get { return _npcId; } }

        private int _rivalId = -1;
        public int RivalId { get { return _rivalId; } }

        private bool _isFirst = false;
        public bool IsFirst { get { return _isFirst; } }

        //
        protected readonly Rival _rival;
        private IEnumerator _iterator = null;


        #region Events

        private StartEvent _startEvent = null;
        public StartEvent StartEvent { get { return _startEvent; } }

        private EndEvent _endEvent = null;
        public EndEvent EndEvent { get { return _endEvent; } }

        private DialogueEvent _dialogueEvent = null;
        public DialogueEvent DialogueEvent { get { return _dialogueEvent; } }

        #endregion


        // constructor
        protected Exam(EType type, int id, int npcId, int rivalId, bool isFirst)
        {
            _type = type;
            _id = id;
            _npcId = npcId;
            _rivalId = rivalId;
            _isFirst = isFirst;

            if (ExtRival.isValid(_rivalId))
                _rival = Manager.Instance.DT.Rival[_rivalId];
            else
                Log.Error(string.Format("invalid rival id: {0}", _rivalId));

            _iterator = null;

            _startEvent = new StartEvent();
            _endEvent = new EndEvent();
            _dialogueEvent = new DialogueEvent();
        }

        public void Start()
        {
            start();
            Iterate();
        }

        private void start()
        {
            _iterator = ExamRoutine();
            StartEvent.Invoke();
        }

        private void end()
        {
            _iterator = null;
            EndEvent.Invoke();
        }

        public void Iterate()
        {
            if (null == _iterator)
            {
                Log.Error("not found iterator");
                return;
            }

            if (_iterator.MoveNext())
                return;

            end();
        }

        protected abstract IEnumerator ExamRoutine();

    }   // class

}   // namespace