using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class AIPlayer : Player
    {
        // const
        public enum EOrient { Attack, Trick, All, MAX }

        //
        private EOrient _orient = EOrient.MAX;
        public EOrient Orient { get { return _orient; } }

        private delegate void Handler();
        private event Handler _battleEvent = null;


        // constructor
        public AIPlayer(CombatMode mode, int id) : base(mode, id)
        {
            CombatPlayer p = Manager.Instance.DTCombatPlayer[id];
            _orient = p.orient;

            Character c = Manager.Instance.DTCharacter[p.characterId];
            _name = c.name;

            _hp = p.hp;
            _mp = p.intellect;
            _criticalProbability = p.criticalProbability;
            _attack = p.attack;
            _defense = p.defense;
            _intellect = p.intellect;
            _tactic = p.tactic;   
        }

        public AIPlayer(CombatMode mode, int id, int hp, int mp, int criticalProbability, int attack, int defense, int intellect, int tactic) : base(mode, id)
        {
            CombatPlayer p = Manager.Instance.DTCombatPlayer[id];
            _orient = p.orient;

            Character c = Manager.Instance.DTCharacter[p.characterId];
            _name = c.name;

            _hp = hp;
            _mp = mp;
            _criticalProbability = criticalProbability;
            _attack = attack;
            _defense = defense;
            _intellect = intellect;
            _tactic = tactic;
        }

        public override void Start(Player opponent)
        {
            base.Start(opponent);

            switch (Orient)
            {
                case EOrient.Attack:
                    _battleEvent = new Handler(Attack);
                    break;

                case EOrient.Trick:
                    _battleEvent = new Handler(trick);
                    break;

                case EOrient.All:
                    if (AttackDamage > TrickDamage)
                        _battleEvent = new Handler(Attack);
                    else
                        _battleEvent = new Handler(trick);
                    break;

                default:
                    Log.Error("invalid Combat.AI orient type");
                    break;
            }
        }

        public void Battle()
        {
            _battleEvent.Invoke();
        }

        private void trick()
        {
            if (false == CanTrick())
                Attack();
            else
                Trick();
        }

    }   // class

}   // namespace
