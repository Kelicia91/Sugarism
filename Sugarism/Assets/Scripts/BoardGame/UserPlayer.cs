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

            MainCharacter character = Manager.Instance.Object.MainCharacter;
            _name = character.Name;

            _intellect = character.Intellect;
            _tactic = character.Tactic;
            _leadership = character.Leadership;

            _power = character.Intellect / 100 + 1;

            _cardCapacity = setMaxNumCard(Intellect);
            _cardArray = new Card[CardCapacity];

            AttackShuffleProbability = BoardGameMode.DEFAULT_ATTACK_CARD_SHUFFLE_PROBABILITY + (BoardGameMode.STAT_WEIGHT * character.Tactic / Def.MAX_STAT);
            DefenseShuffleProbability = BoardGameMode.DEFAULT_DEFENSE_CARD_SHUFFLE_PROBABILITY + (BoardGameMode.STAT_WEIGHT * character.Leadership / Def.MAX_STAT);
        }

        public override void Push()
        {
            int value = GetHowMuchPush();
            Mode.PlusFlow(value);
        }

        public override void Bingo(int bingoCount)
        {
            int value = GetHowMuchBingo(bingoCount);
            Mode.PlusFlow(value);
        }

        public override void Attack()
        {
            int value = GetHowMuchAttack();
            Mode.PlusFlow(value);
        }

        public override void CounterAttack()
        {
            int value = Opponent.GetHowMuchAttack();
            Mode.PlusFlow(value);
        }

    }   // class

}   // namespace
