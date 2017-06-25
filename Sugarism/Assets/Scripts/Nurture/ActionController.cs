using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nurture
{
    public abstract class ActionController
    {
        private int _id = -1;
        public int Id { get { return _id; } }
        
        protected readonly Action _action;

        protected readonly Mode _mode = null;


        // constructor
        protected ActionController(int id, Mode mode)
        {
            _id = id;
            _action = Manager.Instance.DTAction[_id];

            _mode = mode;
        }

        public void Start()
        {
            start();
        }

        protected abstract void start();

        public void First()
        {
            first();
        }

        protected abstract void first();

        public void Do()
        {
            updateStats();

            doing();
        }

        protected abstract void doing();

        protected bool isSuccess()
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
            int currentVal = _mode.Character.Get(_action.criticalStat);
            int baseVal = _action.criticalStatBaseValue;
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

        private void updateStats()
        {
            Character c = _mode.Character;

            c.Stress += _action.stress;

            c.Stamina += _action.stamina;
            c.Intellect += _action.intellect;
            c.Grace += _action.grace;
            c.Charm += _action.charm;

            c.Attack += _action.attack;
            c.Defense += _action.defense;

            c.Leadership += _action.leadership;
            c.Tactic += _action.tactic;

            c.Morality += _action.morality;
            c.Goodness += _action.goodness;

            c.Sensibility += _action.sensibility;
            c.Arts += _action.arts;
        }

        public void End()
        {
            end();
        }

        protected abstract void end();

    }   // class

}   // namespace
