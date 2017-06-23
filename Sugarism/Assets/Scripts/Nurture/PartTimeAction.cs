using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nurture
{
    public class PartTimeAction : ActionController
    {
        private int _successCount = 0;
        private int _actionPeriod = 0;

        public PartTimeAction(int id, MainCharacter mainCharacter) : base(id, mainCharacter)
        {

        }

        protected override void first()
        {
            Manager.Instance.ScheduleFirstEvent.Invoke(_Action.npcId);
        }

        protected override bool doing()
        {
            bool isSuccessed = isSuccess();
            if (isSuccessed)
            {
                ++_successCount;
                _MainCharacter.Money += _Action.money;
            }

            ++_actionPeriod;
            return isSuccessed;
        }

        protected override void finish()
        {
            float quotient = ((float)_successCount) / _actionPeriod;

            int achievementRatio = Mathf.RoundToInt(quotient * 100);

            string s = string.Format(Def.ACHIVEMENT_RATIO_FORMAT, achievementRatio);
            Log.Debug(s);

            string msg = null;
            if (achievementRatio <= 0)
            {
                _MainCharacter.Stress += _Action.failStress;
                msg = string.Format(Def.STRESS_FORMAT, _Action.failStress);
            }
            else if (achievementRatio < 100)
            {
                msg = string.Empty;
            }
            else
            {
                _MainCharacter.Money += _Action.bonus;
                msg = string.Format(Def.MONEY_FORMAT, _Action.bonus);
            }

            Manager.Instance.ScheduleFinishEvent.Invoke(achievementRatio, _Action.npcId, msg);
        }

    }   // class

}   // namespace