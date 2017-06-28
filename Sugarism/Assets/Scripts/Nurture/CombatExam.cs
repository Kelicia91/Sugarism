using System.Collections;

namespace Exam
{
    public class CombatExam : Exam
    {
        //
        private readonly OneToOneExam _exam;
        private int _rivalId = 1;   // temp
        private string _userName = null;
        private string _rivalName = "라이벌";  // temp
        private string _userResultMsg = null;

        // constructor
        public CombatExam(int id, int npcId, bool isFirst) : base(EType.COMBAT, id, npcId, isFirst)
        {
            if (false == ExtOneToOneExam.isValid(id))
                Log.Error(string.Format("invalid one-to-one exam.id : {0}", id));
            else
                _exam = Manager.Instance.DTOneToOneExam[id];
            
            _userName = Manager.Instance.Object.MainCharacter.Name;

            Manager.Instance.Object.CombatMode.EndEvent.Attach(onEnd);
        }

        protected override IEnumerator ExamRoutine()
        {
            DialogueEvent.Invoke(NPCId, _exam.NPCAlarm);
            yield return null;

            if (IsFirst)
            {
                DialogueEvent.Invoke(NPCId, _exam.NPCProcessMethod);
                yield return null;

                DialogueEvent.Invoke(NPCId, _exam.NPCEvaluateMethod);
                yield return null;
            }

            string startMsg = string.Format(_exam.NPCStartPlayersName, _userName, _rivalName); 
            DialogueEvent.Invoke(NPCId, startMsg);
            yield return null;

            DialogueEvent.Invoke(Def.EXAM_USER_START);
            yield return null;

            //
            Manager.Instance.Object.CombatMode.Start(_rivalId);
            yield return null;
            
            //
            DialogueEvent.Invoke(_userResultMsg);
            yield return null;

            Manager.Instance.Object.CombatMode.EndEvent.Detach(onEnd);
        }

        private void onEnd(Combat.CombatMode.EUserGameState state)
        {
            string winnerName = null;
            switch (state)
            {
                case Combat.CombatMode.EUserGameState.Win:
                    winnerName = _userName;
                    _userResultMsg = Def.EXAM_USER_WIN;
                    break;

                case Combat.CombatMode.EUserGameState.Lose:
                    winnerName = _rivalName;
                    _userResultMsg = Def.EXAM_USER_LOSE;
                    break;

                default:
                    Log.Error(string.Format("invalid user game state: {0}", state));
                    break;
            }

            string resultMsg = string.Format(_exam.NPCEndWinnerName, winnerName);
            DialogueEvent.Invoke(NPCId, resultMsg);
        }

    }   // class

}   // namespace

