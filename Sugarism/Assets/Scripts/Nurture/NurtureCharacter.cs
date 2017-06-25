using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nurture
{
    public partial class Character
    {
        // fields, property
        private int[] _actionCount = null;

        private EZodiac _zodiac = EZodiac.MAX;
        public EZodiac Zodiac
        {
            get { return _zodiac; }
            set { _zodiac = value; init(_zodiac); }
        }

        #region Events

        private CharacterStatEvent _statEvent = null;
        public CharacterStatEvent StatEvent { get { return _statEvent; } }

        #endregion


        // constructor
        public Character()
        {
            _actionCount = new int[Manager.Instance.DTAction.Count];
            int numActCount = _actionCount.Length;
            for (int i = 0; i < numActCount; ++i)
            {
                _actionCount[i] = 0;
            }

            _statEvent = new CharacterStatEvent();

            // TEST
            Zodiac = EZodiac.DRAGON;
            //
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

            Stamina = Manager.Instance.DTZodiac[zodiacId].stamina;
            Intellect = Manager.Instance.DTZodiac[zodiacId].intellect;
            Grace = Manager.Instance.DTZodiac[zodiacId].grace;
            Charm = Manager.Instance.DTZodiac[zodiacId].charm;

            Attack = Manager.Instance.DTZodiac[zodiacId].attack;
            Defense = Manager.Instance.DTZodiac[zodiacId].defense;

            Leadership = Manager.Instance.DTZodiac[zodiacId].leadership;
            Tactic = Manager.Instance.DTZodiac[zodiacId].tactic;

            Morality = Manager.Instance.DTZodiac[zodiacId].morality;
            Goodness = Manager.Instance.DTZodiac[zodiacId].goodness;

            Sensibility = Manager.Instance.DTZodiac[zodiacId].sensibility;
            Arts = Manager.Instance.DTZodiac[zodiacId].arts;
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
                int adjustedValue = adjust(value);
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
                int adjustedValue = adjust(value);
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
                int adjustedValue = adjust(value);
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
                int adjustedValue = adjust(value);
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
                int adjustedValue = adjust(value);
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
                int adjustedValue = adjust(value);
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
                int adjustedValue = adjust(value);
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
                int adjustedValue = adjust(value);
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
                int adjustedValue = adjust(value);
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
                int adjustedValue = adjust(value);
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
                int adjustedValue = adjust(value);
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
                int adjustedValue = adjust(value);
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
                int adjustedValue = adjust(value);
                if (_arts.Equals(adjustedValue))
                    return;

                _arts = adjustedValue;
                StatEvent.Invoke(EStat.ARTS, _arts);
            }
        }


        private int adjust(int value)
        {
            if (value < Def.MIN_STAT)
                return Def.MIN_STAT;
            else if (value > Def.MAX_STAT)
                return Def.MAX_STAT;
            else
                return value;
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

    }   // class

}   // namespace
