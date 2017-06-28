using System.Collections;

namespace Exam
{
    public class BoardGameExam : Exam
    {
        //
        private readonly BoardGame.EValuationBasis _valuationBasis = BoardGame.EValuationBasis.MAX;
        private readonly OneToOneExam _exam;

        private int _rivalId = 1; // temp
        private string _userName = null;
        private string _rivalName = "라이벌";  // temp
        private string _userResultMsg = null;

        // constructor
        public BoardGameExam(EType type, int id, int npcId, bool isFirst) : base(type, id, npcId, isFirst)
        {
            switch (type)
            {
                case EType.BOARD_GAME_TRICKER:
                    _valuationBasis = BoardGame.EValuationBasis.Tricker;
                    break;

                case EType.BOARD_GAME_POLITICIAN:
                    _valuationBasis = BoardGame.EValuationBasis.Politician;
                    break;

                default:
                    Log.Error(string.Format("not converted from ExamType({0}) to BoardGame.EValuationBasis", type));
                    break;
            }

            if (false == ExtOneToOneExam.isValid(id))
                Log.Error(string.Format("invalid one-to-one exam.id : {0}", id));
            else
                _exam = Manager.Instance.DTOneToOneExam[id];

            _userName = Manager.Instance.Object.MainCharacter.Name;

            Manager.Instance.Object.BoardGameMode.EndEvent.Attach(onEnd);
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
            Manager.Instance.Object.BoardGameMode.Start(_valuationBasis, _rivalId);
            yield return null;

            //
            DialogueEvent.Invoke(_userResultMsg);
            yield return null;

            Manager.Instance.Object.BoardGameMode.EndEvent.Detach(onEnd);
        }
        
        private void onEnd(BoardGame.EUserGameState state)
        {
            string winnerName = null;
            switch (state)
            {
                case BoardGame.EUserGameState.Win:
                    winnerName = _userName;
                    _userResultMsg = Def.EXAM_USER_WIN;
                    break;

                case BoardGame.EUserGameState.Lose:
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
    }
}
