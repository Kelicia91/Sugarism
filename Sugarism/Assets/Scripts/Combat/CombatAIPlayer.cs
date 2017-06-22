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
            _hp = p.hp;
            _mp = p.intellect;
            _attack = p.attack;
            _defense = p.defense;
            _intellect = p.intellect;
            _tactic = p.tactic;

            Character c = Manager.Instance.DTCharacter[p.characterId];
            _name = c.name;

            _orient = p.orient;
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
