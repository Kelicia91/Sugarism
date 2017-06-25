using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nurture
{ 
    public class LessonAction : ActionController
    {
        private int _successCount = 0;
        private int _actionPeriod = 0;

        public LessonAction(int id, Mode mode) : base(id, mode)
        {

        }

        protected override void start()
        {
            _mode.Schedule.ActionStartEvent.Invoke(Id);
        }

        protected override void first()
        {
            _mode.Schedule.ActionFirstEvent.Invoke(_action.npcId);
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

        protected override void end()
        {
            float quotient = ((float)_successCount) / _actionPeriod;

            int achievementRatio = Mathf.RoundToInt(quotient * 100);

            string s = string.Format(Def.ACHIVEMENT_RATIO_FORMAT, achievementRatio);
            Log.Debug(s);   //@todo: 성취도 ui에서도 출력해주자.

            string msg = null;
            if (achievementRatio <= 0)
            {
                _mode.Character.Stress += _action.failStress;
                msg = string.Format(Def.STRESS_FORMAT, _action.failStress);
            }
            else if (achievementRatio < 100)
            {
                msg = string.Empty;
            }
            else
            {
                _mode.Character.Stress += _action.bonus;
                msg = string.Format(Def.STRESS_FORMAT, _action.bonus);
            }

            _mode.Schedule.ActionEndEvent.Invoke(achievementRatio, _action.npcId, msg);
        }

    }   // class

}   // namespace