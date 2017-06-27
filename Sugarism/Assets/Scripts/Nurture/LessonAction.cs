using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nurture
{ 
    public class LessonAction : ActionController
    {
        //
        private int _lessonId = -1;
        public int LessonId { get { return _lessonId; } }

        private readonly ActionLesson _lesson;

        //
        private int _successCount = 0;
        private int _actionPeriod = 0;


        // constructor
        public LessonAction(int id, Mode mode) : base(id, mode)
        {
            _lessonId = getLessonId(Id);

            if (ExtActionLesson.isValid(_lessonId))
                _lesson = Manager.Instance.DTActionLesson[_lessonId];
            else
                Log.Error(string.Format("not found lesson Id; action Id({0})", Id));
        }

        private int getLessonId(int actionId)
        {
            ActionLessonObject table = Manager.Instance.DTActionLesson;
            for (int i = 0; i < table.Count; ++i)
            {
                if (table[i].actionId == actionId)
                    return i;
            }
            
            return -1;
        }

        protected override void first()
        {
            _mode.Schedule.ActionFirstEvent.Invoke(_lesson.npcId);
        }

        protected override void doing()
        {
            _mode.Currency.Money += _action.money;

            bool isSuccessed = isSuccess();
            if (isSuccessed)
            {
                ++_successCount;
            }

            ++_actionPeriod;
            _mode.Schedule.ActionDoEvent.Invoke(isSuccessed);
        }

        protected override void beforeEnd()
        {
            if (false == isExamDay())
            {
                base.beforeEnd();
                return;
            }

            Exam.Exam exam = null;
            switch (_lesson.examType)
            {
                case Exam.EType.COMBAT:
                    // @todo
                    break;

                case Exam.EType.BOARD_GAME:
                    // @todo
                    break;

                case Exam.EType.SCORE:
                    exam = new Exam.ScoreExam(isFirstExam(), _lesson.npcId, new ScoreExamLessonArts());
                    break;

                default:
                    Log.Error("not found exam type");
                    base.beforeEnd();
                    return;
            }

            _mode.Schedule.ActionBeforeEndEvent.Invoke(exam);
        }

        protected override void end()
        {
            float quotient = ((float)_successCount) / _actionPeriod;

            int achievementRatio = Mathf.RoundToInt(quotient * 100);

            string achieveMsg = string.Format(Def.ACHIVEMENT_RATIO_FORMAT, achievementRatio);
            Log.Debug(achieveMsg);

            string msg = null;
            if (achievementRatio <= 0)
            {
                _mode.Character.Stress += _lesson.terribleStress;
                
                string stressMsg = string.Format(Def.STRESS_FORMAT, _lesson.terribleStress);
                msg = string.Format("{0}, {1}", achieveMsg, stressMsg);
            }
            else if (achievementRatio < 100)
            {
                msg = achieveMsg;
            }
            else
            {
                _mode.Character.Stress += _lesson.perfectStress;
                
                string stressMsg = string.Format(Def.STRESS_FORMAT, _lesson.perfectStress);
                msg = string.Format("{0}, {1}", achieveMsg, stressMsg);
            }

            _mode.Schedule.ActionEndEvent.Invoke(achievementRatio, _lesson.npcId, msg);
        }


        //
        private bool isSuccess()
        {
            int min = 0;    // inclusive
            int max = 100;  // exclusive

            int randomValue = Random.Range(min, max);

            string msg = string.Format("Random Value : {0}", randomValue);
            Log.Debug(msg);

            int successProbability = getSuccessProbability();

            if (randomValue < successProbability)
                return true;
            else
                return false;
        }

        private int getSuccessProbability()
        {
            // 성공율 = 각 element 합 * 100
            //
            // 1 element = 가중치1 * (criticalStat.current / criticalStat.base)
            //                       (단, current >= base 이면 1)
            // 2 element = 가중치2 * (1 - stress / stamina)
            //                      (단, stress >= stamina 이면 1)
            //
            // 조건. SUM(각 element 에 부여된 가중치) = 1
            // 조건. stat (min,max) = (0, 999)

            const int NUM_OF_ELEMENT = 2;
            float[] elemArray = new float[NUM_OF_ELEMENT];

            float quotient = 0;

            // set element[0]
            int currentVal = _mode.Character.Get(_lesson.criticalStat);
            int baseVal = _lesson.criticalStatBaseValue;
            if (currentVal >= baseVal)
            {
                elemArray[0] = Def.CRITICAL_WEIGHT; // * 1
            }
            else
            {
                quotient = ((float)currentVal) / baseVal;  // float=(float/int)
                elemArray[0] = Def.CRITICAL_WEIGHT * quotient;
            }

            // set element[1]
            const float REMAIN_WEIGHT = 1.0f - Def.CRITICAL_WEIGHT;
            if (_mode.Character.Stress >= _mode.Character.Stamina)
            {
                elemArray[1] = 0.0f;
            }
            else
            {
                quotient = ((float)_mode.Character.Stress) / _mode.Character.Stamina;
                elemArray[1] = REMAIN_WEIGHT * (1.0f - quotient);
            }

            //
            float sum = 0;
            for (int i = 0; i < NUM_OF_ELEMENT; ++i)
                sum += elemArray[i];

            //
            int successProbability = Mathf.RoundToInt(sum * 100);

            string msg = string.Format("Success Probability : {0}", successProbability);
            Log.Debug(msg);

            return successProbability;
        }


        private bool isExamDay()
        {
            int remainder = _mode.Character.GetActionCount(Id) % Def.EXAM_PERIOD;
            if (0 == remainder)
                return true;
            else
                return false;
        }

        private bool isFirstExam()
        {
            int quotient = _mode.Character.GetActionCount(Id) / Def.EXAM_PERIOD;
            if (1 == quotient)
                return true;
            else
                return false;
        }

    }   // class

    
    // @note: Temp class..
    public class ScoreExamLessonArts
    {
        public const int EXCELLENT_STRESS = -30;
        public const int BAD_STRESS = 30;

        public readonly Score.StatWeight[] ScoreExamStatWeightArray = 
        {
            new Score.StatWeight(EStat.STRESS, 10),
            new Score.StatWeight(EStat.CHARM, 30),
            new Score.StatWeight(EStat.SENSIBILITY, 30),
            new Score.StatWeight(EStat.ARTS, 30)
        };

        public string GetNPCComment(Score.EGrade grade)
        {
            switch (grade)
            {
                case Score.EGrade.S:
                    return Def.ARTS_EXAM_END_NPC_COMMENT_S;

                case Score.EGrade.A:
                    return Def.ARTS_EXAM_END_NPC_COMMENT_A;

                case Score.EGrade.B:
                    return Def.ARTS_EXAM_END_NPC_COMMENT_B;

                case Score.EGrade.C:
                    return Def.ARTS_EXAM_END_NPC_COMMENT_C;

                case Score.EGrade.D:
                    return Def.ARTS_EXAM_END_NPC_COMMENT_D;

                default:
                    Log.Error(string.Format("invalid grade; {0}", grade));
                    return null;
            }
        }
        
        public string Reward(Score.EGrade grade)
        {
            const string REWARD_MSG_FORMAT = "\n({0})";
            string rewardMsg = null;

            Character nChacater = Manager.Instance.Object.NurtureMode.Character;
            switch (grade)
            {
                case Score.EGrade.S:
                    nChacater.Stress += EXCELLENT_STRESS;
                    rewardMsg = string.Format(REWARD_MSG_FORMAT, string.Format(Def.STRESS_FORMAT, EXCELLENT_STRESS));
                    break;

                case Score.EGrade.D:
                    nChacater.Stress += BAD_STRESS;
                    rewardMsg = string.Format(REWARD_MSG_FORMAT, string.Format(Def.STRESS_FORMAT, BAD_STRESS));
                    break;

                default:
                    rewardMsg = string.Empty;
                    break;
            }

            return rewardMsg;
        }

    }   // class

}   // namespace