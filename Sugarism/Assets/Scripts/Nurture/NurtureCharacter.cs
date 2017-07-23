﻿using System;
using UnityEngine;


namespace Nurture
{
    public enum ECondition
    {
        Healthy = 0,
        Sick,
        Die
    }

    public abstract partial class Character
    {
        // fields, property
        private int[] _actionCount = null;

        public abstract int Money { get; set; }

        private EZodiac _zodiac = EZodiac.MAX;
        public EZodiac Zodiac
        {
            get { return _zodiac; }
            set { _zodiac = value; init(_zodiac); }
        }

        public readonly int INIT_AGE = 0;

        private int _age = 0;
        public int Age
        {
            get { return _age; }
            set { _age = value; AgeChangeEvent.Invoke(_age); }
        }

        private ECondition _condition = ECondition.Healthy;
        public ECondition Condition
        {
            get { return _condition; }
            set
            {
                if (_condition.Equals(value))
                    return;

                _condition = value;
                ConditionEvent.Invoke(_condition);

                if (ECondition.Die == _condition)
                    Die();
            }
        }


        #region Events

        private AgeChangeEvent _ageChangeEvent = null;
        public AgeChangeEvent AgeChangeEvent { get { return _ageChangeEvent; } }

        private CharacterStatEvent _statEvent = null;
        public CharacterStatEvent StatEvent { get { return _statEvent; } }

        private CharacterConditionEvent _conditionEvent = null;
        public CharacterConditionEvent ConditionEvent { get { return _conditionEvent; } }

        #endregion


        // constructor
        public Character(int age)
        {
            _actionCount = new int[Manager.Instance.DTAction.Count];
            int numActCount = _actionCount.Length;
            for (int i = 0; i < numActCount; ++i)
            {
                _actionCount[i] = 0;
            }

            INIT_AGE = age;
            _age = INIT_AGE;

            _condition = ECondition.Healthy;

            _ageChangeEvent = new AgeChangeEvent();
            _statEvent = new CharacterStatEvent();
            _conditionEvent = new CharacterConditionEvent();
        }

        public void Die()
        {
            string scenarioPath = string.Format("{0}{1}{2}",
                                RsrcLoader.SCENARIO_FOLDER_PATH, RsrcLoader.DIR_SEPARATOR,
                                RsrcLoader.SICK_BAD_ENDING_FILENAME);

            Story.Mode storyMode = Manager.Instance.Object.StoryMode;

            bool isLoaded = storyMode.LoadScenario(scenarioPath);
            if (false == isLoaded)
                return; // @todo: 에러. 게임종료.

            storyMode.ScenarioEndEvent.Attach(onScenarioEnd);
        }

        private void onScenarioEnd()
        {
            // @todo: 질병 시나리오 끝. 게임 종료.
            Log.Debug("die scenario end.");
        }

        public void UpdateCondition()
        {
            int sickness = Stress - Stamina;

            if (sickness >= Def.SICK_MAX)
                Condition = ECondition.Die;
            else if (sickness >= Def.SICK_WARNING)
                Condition = ECondition.Sick;
            else
                Condition = ECondition.Healthy;
        }

        public int GetActionCount(int actionIndex)
        {
            if (actionIndex < 0)
                return -1;
            else if (actionIndex >= _actionCount.Length)
                return -1;
            else
                return _actionCount[actionIndex];
        }

        public void IncrementActionCount(int actionIndex)
        {
            if (actionIndex < 0)
                return;
            else if (actionIndex >= _actionCount.Length)
                return;
            else
                ++_actionCount[actionIndex];
        }

        private void init(EZodiac zodiac)
        {
            int zodiacId = (int)zodiac;

            if ((zodiacId < 0) || (zodiacId >= Manager.Instance.DTZodiac.Count))
            {
                string errMsg = string.Format("invalid zodiac id: {0}", zodiacId);
                Log.Error(errMsg);
                return;
            }

            Zodiac zodiacFromDT = Manager.Instance.DTZodiac[zodiacId];

            Stamina = zodiacFromDT.stamina;
            Intellect = zodiacFromDT.intellect;
            Grace = zodiacFromDT.grace;
            Charm = zodiacFromDT.charm;

            Attack = zodiacFromDT.attack;
            Defense = zodiacFromDT.defense;

            Leadership = zodiacFromDT.leadership;
            Tactic = zodiacFromDT.tactic;

            Morality = zodiacFromDT.morality;
            Goodness = zodiacFromDT.goodness;

            Sensibility = zodiacFromDT.sensibility;
            Arts = zodiacFromDT.arts;
        }

    }   // class

    // Stats
    public partial class Character
    {
        private int _stress = 0;
        public int Stress
        {
            get { return _stress; }
            set
            {
                int adjustedValue = Adjust(value);
                if (_stress.Equals(adjustedValue))
                    return;

                _stress = adjustedValue;
                StatEvent.Invoke(EStat.STRESS, _stress);
            }
        }

        private int _stamina = 0;
        public int Stamina
        {
            get { return _stamina; }
            set
            {
                int adjustedValue = Adjust(value);
                if (_stamina.Equals(adjustedValue))
                    return;

                _stamina = adjustedValue;
                StatEvent.Invoke(EStat.STAMINA, _stamina);
            }
        }

        private int _intellect = 0;
        public int Intellect
        {
            get { return _intellect; }
            set
            {
                int adjustedValue = Adjust(value);
                if (_intellect.Equals(adjustedValue))
                    return;

                _intellect = adjustedValue;
                StatEvent.Invoke(EStat.INTELLECT, _intellect);
            }
        }

        private int _grace = 0;
        public int Grace
        {
            get { return _grace; }
            set
            {
                int adjustedValue = Adjust(value);
                if (_grace.Equals(adjustedValue))
                    return;

                _grace = adjustedValue;
                StatEvent.Invoke(EStat.GRACE, _grace);
            }
        }

        private int _charm = 0;
        public int Charm
        {
            get { return _charm; }
            set
            {
                int adjustedValue = Adjust(value);
                if (_charm.Equals(adjustedValue))
                    return;

                _charm = adjustedValue;
                StatEvent.Invoke(EStat.CHARM, _charm);
            }
        }

        private int _attack = 0;
        public int Attack
        {
            get { return _attack; }
            set
            {
                int adjustedValue = Adjust(value);
                if (_attack.Equals(adjustedValue))
                    return;

                _attack = adjustedValue;
                StatEvent.Invoke(EStat.ATTACK, _attack);
            }
        }

        private int _defense = 0;
        public int Defense
        {
            get { return _defense; }
            set
            {
                int adjustedValue = Adjust(value);
                if (_defense.Equals(adjustedValue))
                    return;

                _defense = adjustedValue;
                StatEvent.Invoke(EStat.DEFENSE, _defense);
            }
        }

        private int _leadership = 0;
        public int Leadership
        {
            get { return _leadership; }
            set
            {
                int adjustedValue = Adjust(value);
                if (_leadership.Equals(adjustedValue))
                    return;

                _leadership = adjustedValue;
                StatEvent.Invoke(EStat.LEADERSHIP, _leadership);
            }
        }

        private int _tactic = 0;
        public int Tactic
        {
            get { return _tactic; }
            set
            {
                int adjustedValue = Adjust(value);
                if (_tactic.Equals(adjustedValue))
                    return;

                _tactic = adjustedValue;
                StatEvent.Invoke(EStat.TACTIC, _tactic);
            }
        }

        private int _morality = 0;
        public int Morality
        {
            get { return _morality; }
            set
            {
                int adjustedValue = Adjust(value);
                if (_morality.Equals(adjustedValue))
                    return;

                _morality = adjustedValue;
                StatEvent.Invoke(EStat.MORALITY, _morality);
            }
        }

        private int _goodness = 0;
        public int Goodness
        {
            get { return _goodness; }
            set
            {
                int adjustedValue = Adjust(value);
                if (_goodness.Equals(adjustedValue))
                    return;

                _goodness = adjustedValue;
                StatEvent.Invoke(EStat.GOODNESS, _goodness);
            }
        }

        private int _sensibility = 0;
        public int Sensibility
        {
            get { return _sensibility; }
            set
            {
                int adjustedValue = Adjust(value);
                if (_sensibility.Equals(adjustedValue))
                    return;

                _sensibility = adjustedValue;
                StatEvent.Invoke(EStat.SENSIBILITY, _sensibility);
            }
        }

        private int _arts = 0;
        public int Arts
        {
            get { return _arts; }
            set
            {
                int adjustedValue = Adjust(value);
                if (_arts.Equals(adjustedValue))
                    return;

                _arts = adjustedValue;
                StatEvent.Invoke(EStat.ARTS, _arts);
            }
        }


        public static int Adjust(int stat)
        {
            if (stat < Def.MIN_STAT)
                return Def.MIN_STAT;
            else if (stat > Def.MAX_STAT)
                return Def.MAX_STAT;
            else
                return stat;
        }


        public int Get(EStat statType)
        {
            switch (statType)
            {
                case EStat.STRESS:
                    return Stress;

                case EStat.STAMINA:
                    return Stamina;

                case EStat.INTELLECT:
                    return Intellect;

                case EStat.GRACE:
                    return Grace;

                case EStat.CHARM:
                    return Charm;

                case EStat.ATTACK:
                    return Attack;

                case EStat.DEFENSE:
                    return Defense;

                case EStat.LEADERSHIP:
                    return Leadership;

                case EStat.TACTIC:
                    return Tactic;

                case EStat.MORALITY:
                    return Morality;

                case EStat.GOODNESS:
                    return Goodness;

                case EStat.SENSIBILITY:
                    return Sensibility;

                case EStat.ARTS:
                    return Arts;

                default:
                    return 0;
            }
        }

        public int GetAverage(EStatLine statLine)
        {
            if (EStatLine.MAX == statLine)
            {
                Log.Error("invalid stat line");
                return -1;
            }

            int STAT_COUNT = Manager.Instance.DTStat.Count;

            //Log.Debug(string.Format("GetAverage; stat line: {0}", statLine));

            int sum = 0, count = 0;
            for (int statId = 0; statId < STAT_COUNT; ++statId)
            {
                if (statLine != Manager.Instance.DTStat[statId].statLine)
                    continue;

                if (false == Enum.IsDefined(typeof(EStat), statId))
                {
                    Log.Error(string.Format("{0} can't convert to EStat", statId));
                    return -1;
                }

                EStat stat = (EStat)statId;
                int statValue = Get(stat);

                //Log.Debug(string.Format("stat: {0} ({1})", stat, statValue));

                sum += statValue;
                ++count;
            }

            //Log.Debug(string.Format("sum: {0}, count: {1}", sum, count));
            if (count <= 0)
                return 0;

            int avg = Mathf.RoundToInt(sum / count);
            //Log.Debug(string.Format("avg: {0}", avg));

            return avg;
        }

    }   // class

}   // namespace
