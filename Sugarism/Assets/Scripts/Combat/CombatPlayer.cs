using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{

    public class Player
    {
        // const
        public const int MP_CONSUMPTION = 30;
        public const int MIN_CRITICAL = 100;
        public const int MIN_ATTACK = 10;
        public const int MIN_TRICK = 20;

        //
        private int _id = -1;
        public int Id { get { return _id; } }

        protected string _name = null;
        public string Name { get { return _name; } }

        protected int _hp = 0;
        public int Hp
        {
            get { return _hp; }
            set
            {
                _hp = value;

                if (_hp < Def.MIN_STAT)
                    _hp = Def.MIN_STAT;
                else if (_hp > Def.MAX_STAT)
                    _hp = Def.MAX_STAT;

                _mode.HpChangeEvent.Invoke(Id, _hp);
            }
        }

        protected int _mp = 0;
        public int Mp
        {
            get { return _mp; }
            protected set
            {
                _mp = value;

                if (_mp < Def.MIN_STAT)
                    _mp = Def.MIN_STAT;
                else if (_mp > Def.MAX_STAT)
                    _mp = Def.MAX_STAT;

                _mode.MpChangeEvent.Invoke(Id, _mp);
            }
        }

        private int _criticalProbability = 0;
        public int CriticalProbability { get { return _criticalProbability; } }

        protected int _attack = 0;
        public int AttackPower { get { return _attack; } }  // name: avoid Attack()

        protected int _defense = 0;
        public int Defense { get { return _defense; } }

        protected int _intellect = 0;
        public int Intellect { get { return _intellect; } }

        protected int _tactic = 0;
        public int Tactic { get { return _tactic; } }

        private CombatMode _mode = null;
        protected CombatMode Mode { get { return _mode; } }

        private Player _opponent = null;
        protected Player Opponent { get { return _opponent; } }

        private int _attackDamage = 0;
        protected int AttackDamage { get { return _attackDamage; } }

        private int _criticalAttackDamage = 0;
        protected int CriticalAttackDamage { get { return _criticalAttackDamage; } }

        private int _trickDamage = 0;
        protected int TrickDamage { get { return _trickDamage; } }

        private int _criticalTrickDamage = 0;
        protected int CriticalTrickDamage { get { return _criticalTrickDamage; } }


        // constructor
        public Player(CombatMode mode, int id)
        {
            _mode = mode;
            _id = id;

            CombatPlayer p = Manager.Instance.DTCombatPlayer[Id];
            _criticalProbability = p.criticalProbability;
        }

        public virtual void Start(Player opponent)
        {
            _opponent = opponent;

            _attackDamage = getAttackDamage();
            _criticalAttackDamage = getCriticalAttackDamage();
            _trickDamage = getTrickDamage();
            _criticalTrickDamage = getCriticalTrickDamage();
        }

        public void Attack()
        {
            int damage = 0;

            if (true == isCritical())
            {
                damage = CriticalAttackDamage;
                _mode.CriticalAttackEvent.Invoke(Id, damage);
            }
            else
            {
                damage = AttackDamage;
                _mode.AttackEvent.Invoke(Id, damage);
            }

            Opponent.Hp -= damage;
        }

        public void Trick()
        {
            int damage = 0;

            if (true == isCritical())
            {
                damage = CriticalTrickDamage;
                _mode.CriticalTrickEvent.Invoke(Id, damage);
            }
            else
            {
                damage = TrickDamage;
                _mode.TrickEvent.Invoke(Id, damage);
            }

            Mp -= MP_CONSUMPTION;
            Opponent.Hp -= damage;
        }

        public bool CanTrick()
        {
            if (Mp > Def.MIN_STAT)
                return true;
            else
                return false;
        }

        private bool isCritical()
        {
            int random = Random.Range(0, 100);  // 0~99

            if (random < CriticalProbability)
                return true;
            else
                return false;
        }

        private int getCriticalAttackDamage()
        {
            int diff = AttackPower - Opponent.Defense;
            if (diff <= 0)
                return MIN_CRITICAL;
            else
                return diff / 4 + MIN_CRITICAL;
        }

        private int getAttackDamage()
        {
            int diff = AttackPower - Opponent.Defense;
            if (diff <= 0)
                return MIN_ATTACK;
            else
                return diff / 5 + MIN_ATTACK;
        }

        private int getCriticalTrickDamage()
        {
            int diff = Intellect - Opponent.Tactic;
            if (diff <= 0)
                return MIN_CRITICAL;
            else
                return diff / 4 + MIN_CRITICAL;
        }

        private int getTrickDamage()
        {
            int diff = Intellect - Opponent.Tactic;
            if (diff <= 0)
                return MIN_TRICK;
            else
                return diff / 5 + MIN_TRICK;
        }

    }   // class
    
}   // namespace