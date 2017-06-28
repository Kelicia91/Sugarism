using System.Collections;

namespace Exam
{
    public class ScoreExam : Exam
    {
        //
        private readonly global::ScoreExam _exam;   // DataTable.Row
        private Score.ScoreMode _mode = null;
        

        // constructor
        public ScoreExam(int id, int npcId, bool isFirst) : base(EType.SCORE, id, npcId, isFirst)
        {
            if (false == ExtScoreExam.isValid(id))
                Log.Error(string.Format("invalid score exam.id : {0}", id));
            else
                _exam = Manager.Instance.DTScoreExam[Id];
            
            _mode = new Score.ScoreMode();
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

            DialogueEvent.Invoke(NPCId, _exam.NPCStart);
            yield return null;

            DialogueEvent.Invoke(Def.EXAM_USER_START);
            yield return null;

            // SCORE
            Score.UserPlayer user = new Score.UserPlayer();

            int userScore = _mode.GetScore(user, getStatWeight());
            Log.Debug(string.Format("user score: {0}", userScore));

            Score.EGrade userGrade = _mode.GetGrade(userScore);
            Log.Debug(string.Format("user grade : {0}", userGrade));

            //
            string userPoem = user.GetPoem(userGrade);
            DialogueEvent.Invoke(userPoem);
            yield return null;

            DialogueEvent.Invoke(NPCId, _exam.NPCEnd);
            yield return null;

            string userName = Manager.Instance.Object.MainCharacter.Name;
            string usernameMsg = string.Format(_exam.NPCUserNameMsg, userName);
            DialogueEvent.Invoke(NPCId, usernameMsg);
            yield return null;

            string scoreMsg = string.Format(_exam.NPCScoreMsg, userScore);
            DialogueEvent.Invoke(NPCId, scoreMsg);
            yield return null;

            DialogueEvent.Invoke(NPCId, getNPCComment(userGrade));
            yield return null;

            // REWARD
            string rewardMsg = reward(userGrade);

            //
            string userReactMsg = string.Format("{0}{1}", user.GetCommentReact(userGrade), rewardMsg);
            DialogueEvent.Invoke(userReactMsg);
            yield return null;
        }

        private Score.StatWeight[] getStatWeight()
        {
            Score.StatWeight[] statWeight =
            {
                new Score.StatWeight(EStat.STRESS, _exam.stressWeight),
                new Score.StatWeight(EStat.CHARM, _exam.charmWeight),
                new Score.StatWeight(EStat.SENSIBILITY, _exam.sensibilityWeight),
                new Score.StatWeight(EStat.ARTS, _exam.artsWeight)
            };

            return statWeight;
        }

        private string getNPCComment(Score.EGrade grade)
        {
            switch (grade)
            {
                case Score.EGrade.S:
                    return _exam.NPCCommentS;

                case Score.EGrade.A:
                    return _exam.NPCCommentA;

                case Score.EGrade.B:
                    return _exam.NPCCommentB;

                case Score.EGrade.C:
                    return _exam.NPCCommentC;

                case Score.EGrade.D:
                    return _exam.NPCCommentD;

                default:
                    Log.Error(string.Format("invalid grade; {0}", grade));
                    return null;
            }
        }

        private string reward(Score.EGrade grade)
        {
            const string REWARD_MSG_FORMAT = "\n({0})";
            string rewardMsg = null;

            Nurture.Character chacater = Manager.Instance.Object.NurtureMode.Character;
            switch (grade)
            {
                case Score.EGrade.S:
                    chacater.Stress += Def.SCORE_EXAM_EXCELLENT_STRESS;
                    rewardMsg = string.Format(REWARD_MSG_FORMAT, string.Format(Def.STRESS_FORMAT, Def.SCORE_EXAM_EXCELLENT_STRESS));
                    break;

                case Score.EGrade.D:
                    chacater.Stress += Def.SCORE_EXAM_BAD_STRESS;
                    rewardMsg = string.Format(REWARD_MSG_FORMAT, string.Format(Def.STRESS_FORMAT, Def.SCORE_EXAM_BAD_STRESS));
                    break;

                default:
                    rewardMsg = string.Empty;
                    break;
            }

            return rewardMsg;
        }

    }   // class

}   // namespace