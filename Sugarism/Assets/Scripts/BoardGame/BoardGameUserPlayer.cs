using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardGame
{
    public class UserPlayer : Player
    {
        // constructor
        public UserPlayer(BoardGameMode mode) : base(mode, Cell.EOwner.User)
        {
            _id = Def.MAIN_CHARACTER_ID;

            _name = Manager.Instance.Object.MainCharacter.Name;

            Nurture.Character character = Manager.Instance.Object.NurtureMode.Character;

            _intellect = character.Intellect;
            _tactic = character.Tactic;
            _leadership = character.Leadership;

            _grace = character.Grace;
            _morality = character.Morality;
            _goodness = character.Goodness;

            switch (Mode.ValuationBasis)
            {
                case EValuationBasis.Tricker:
                    initialize(Intellect, Intellect, Tactic, Leadership);
                    break;

                case EValuationBasis.Politician:
                    initialize(Grace, Grace, Morality, Goodness);
                    break;

                default:
                    break;
            }
        }

        public override void Push()
        {
            Mode.AttackEvent.Invoke(Id);

            int value = GetHowMuchPush();
            Mode.PlusFlow(value);
        }

        public override void Bingo(int bingoCount)
        {
            Mode.AttackEvent.Invoke(Id);

            int value = GetHowMuchBingo(bingoCount);
            Mode.PlusFlow(value);
        }

        public override void Attack()
        {
            Mode.AttackEvent.Invoke(Id);

            int value = GetHowMuchAttack();
            Mode.PlusFlow(value);
        }

        public override void CounterAttack()
        {
            Mode.CounterAttackEvent.Invoke(Id);

            int value = Opponent.GetHowMuchAttack();
            Mode.PlusFlow(value);
        }

    }   // class

}   // namespace
