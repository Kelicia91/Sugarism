using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exam
{
    public class ScoreExam : Exam
    {
        //
        private Nurture.ScoreExamLessonArts _lesson = null;
        private Score.ScoreMode _mode = null;
        

        // constructor
        public ScoreExam(bool isFirst, int npcId, Nurture.ScoreExamLessonArts lesson) : base(EType.SCORE, isFirst, npcId)
        {
            _lesson = lesson;
            _mode = new Score.ScoreMode();
        }

        protected override IEnumerator ExamRoutine()
        {
            DialogueEvent.Invoke(NPCId, Def.ARTS_EXAM_START_NPC_ALARM);
            yield return null;

            if (IsFirst)
            {
                DialogueEvent.Invoke(NPCId, Def.ARTS_EXAM_START_NPC_PROCESS_METHOD);
                yield return null;

                DialogueEvent.Invoke(NPCId, Def.ARTS_EXAM_START_NPC_EVALUATE_METHOD);
                yield return null;
            }

            DialogueEvent.Invoke(NPCId, Def.ARTS_EXAM_START_NPC_START);
            yield return null;

            DialogueEvent.Invoke(Def.SCORE_EXAM_USER_START);
            yield return null;

            // SCORE
            Score.UserPlayer user = new Score.UserPlayer();

            int userScore = _mode.GetScore(user, _lesson.ScoreExamStatWeightArray);
            Log.Debug(string.Format("user score: {0}", userScore));

            Score.EGrade userGrade = _mode.GetGrade(userScore);
            Log.Debug(string.Format("user grade : {0}", userGrade));

            //
            string userPoem = user.GetPoem(userGrade);
            DialogueEvent.Invoke(userPoem);
            yield return null;

            DialogueEvent.Invoke(NPCId, Def.ARTS_EXAM_END_NPC_ALARM);
            yield return null;

            string userName = Manager.Instance.Object.MainCharacter.Name;
            string usernameMsg = string.Format(Def.ARTS_EXAM_END_NPC_PLAYER, userName);
            DialogueEvent.Invoke(NPCId, usernameMsg);
            yield return null;

            string scoreMsg = string.Format(Def.ARTS_EXAM_END_NPC_SCORE, userScore);
            DialogueEvent.Invoke(NPCId, scoreMsg);
            yield return null;

            DialogueEvent.Invoke(NPCId, _lesson.GetNPCComment(userGrade));
            yield return null;

            // REWARD
            string rewardMsg = _lesson.Reward(userGrade);

            //
            string userReactMsg = string.Format("{0}{1}", user.GetCommentReact(userGrade), rewardMsg);
            DialogueEvent.Invoke(userReactMsg);
            yield return null;
        }

    }   // class

}   // namespace