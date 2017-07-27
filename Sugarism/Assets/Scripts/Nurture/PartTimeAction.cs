using UnityEngine;


namespace Nurture
{
    public class PartTimeAction : ActionController
    {
        //
        private int _partTimeId = -1;
        public int PartTimeId { get { return _partTimeId; } }

        private readonly ActionPartTime _parttime;

        //
        private int _successCount = 0;
        private int _actionPeriod = 0;


        // constructor
        public PartTimeAction(int id, Mode mode) : base(id, mode)
        {
            _partTimeId = getPartTimeId(Id);

            if (ExtActionPartTime.isValid(_partTimeId))
                _parttime = Manager.Instance.DT.ActionPartTime[_partTimeId];
            else
                Log.Error(string.Format("not found parttime Id; action Id({0})", Id));
        }

        private int getPartTimeId(int actionId)
        {
            ActionPartTimeObject table = Manager.Instance.DT.ActionPartTime;
            for (int i = 0; i < table.Count; ++i)
            {
                if (table[i].actionId == actionId)
                    return i;
            }

            return -1;
        }

        protected override void first()
        {
            _mode.Schedule.ActionFirstEvent.Invoke(_parttime.npcId);
        }

        protected override void doing()
        {
            bool isSuccessed = isSuccess();
            if (isSuccessed)
            {
                ++_successCount;
                _mode.Character.Money += _action.money;
            }

            ++_actionPeriod;
            _mode.Schedule.ActionDoEvent.Invoke(isSuccessed);
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
                _mode.Character.Stress += _parttime.terribleStress;

                string stressMsg = string.Format(Def.STRESS_FORMAT, _parttime.terribleStress);
                msg = string.Format("{0}, {1}", achieveMsg, stressMsg);
            }
            else if (achievementRatio < 100)
            {
                msg = achieveMsg;
            }
            else
            {
                _mode.Character.Money += _parttime.perfectMoney;

                string moneyMsg = string.Format(Def.MONEY_FORMAT, _parttime.perfectMoney);
                msg = string.Format("{0}, {1}", achieveMsg, moneyMsg);
            }

            _mode.Schedule.ActionEndEvent.Invoke(achievementRatio, _parttime.npcId, msg);
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
            int currentVal = _mode.Character.Get(_parttime.criticalStat);
            int baseVal = _parttime.criticalStatBaseValue;
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

    }   // class

}   // namespace