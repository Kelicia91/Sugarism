using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exam
{
    public enum EType
    {
        COMBAT = 0,
        BOARD_GAME,
        SCORE,

        MAX
    }

    // Nurture.Action.Lesson <---> (Exam) <---> Mode
    public abstract class Exam
    {
        //
        private EType _type = EType.MAX;
        public EType Type { get { return _type; } }

        private bool _isFirst = false;
        public bool IsFirst { get { return _isFirst; } }

        private int _npcId = -1;
        public int NPCId { get { return _npcId; } }

        //
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
        protected Exam(EType type, bool isFirst, int npcId)
        {
            _type = type;
            _isFirst = isFirst;
            _npcId = npcId;

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