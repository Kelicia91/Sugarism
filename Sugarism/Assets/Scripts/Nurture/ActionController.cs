using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nurture
{
    public abstract class ActionController
    {
        private int _id = -1;
        public int Id { get { return _id; } }

        protected readonly Action _Action;
        protected readonly MainCharacter _MainCharacter;

        protected ActionController(int id, MainCharacter mainCharacter)
        {
            _id = id;
            _Action = Manager.Instance.DTAction[_id];

            _MainCharacter = mainCharacter;
        }

        public void Begin()
        {
            Manager.Instance.ScheduleBeginEvent.Invoke(_id);
        }

        public void First()
        {
            first();
        }

        protected virtual void first()
        {
            Manager.Instance.Object.Schedule.Do();
        }

        public void Do()
        {
            updateStat();

            bool isSuccessed = doing();
            Manager.Instance.ScheduleDoEvent.Invoke(isSuccessed);
        }

        protected abstract bool doing();

        protected bool isSuccess()
        {
            int min = 0;    // inclusive
            int max = 100;  // exclusive

            int randomValue = UnityEngine.Random.Range(min, max);

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
            int currentVal = _MainCharacter.Get(_Action.criticalStat);
            int baseVal = _Action.criticalStatBaseValue;
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
            if (_MainCharacter.Stress >= _MainCharacter.Stamina)
            {
                elemArray[1] = 0.0f;
            }
            else
            {
                quotient = ((float)_MainCharacter.Stress) / _MainCharacter.Stamina;
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

        private void updateStat()
        {
            _MainCharacter.Stress += _Action.stress;

            _MainCharacter.Stamina += _Action.stamina;
            _MainCharacter.Intellect += _Action.intellect;
            _MainCharacter.Grace += _Action.grace;
            _MainCharacter.Charm += _Action.charm;

            _MainCharacter.Attack += _Action.attack;
            _MainCharacter.Defence += _Action.defense;

            _MainCharacter.Leadership += _Action.leadership;
            _MainCharacter.Tactic += _Action.tactic;

            _MainCharacter.Morality += _Action.morality;
            _MainCharacter.Goodness += _Action.goodness;

            _MainCharacter.Sensibility += _Action.sensibility;
            _MainCharacter.Arts += _Action.arts;
        }

        public void Finish()
        {
            _MainCharacter.Increment(Id);

            finish();
        }

        protected virtual void finish()
        {
            int achievementRatio = 0;
            int npcId = _Action.npcId;
            string msg = null;

            Manager.Instance.ScheduleFinishEvent.Invoke(achievementRatio, npcId, msg);
        }

    }   // class

}   // namespace
