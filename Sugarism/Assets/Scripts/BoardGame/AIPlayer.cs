﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BoardGame
{
    public class AIPlayer : Player
    {
        // constructor
        public AIPlayer(BoardGameMode mode, int id) : base(mode, Cell.EOwner.AI)
        {
            _id = id;

            BoardGamePlayer player = Manager.Instance.DTBoardGamePlayer[_id];
            int characterId = player.characterId;
            _name = Manager.Instance.DTCharacter[characterId].name;

            _intellect = player.intellect;
            _tactic = player.tactic;
            _leadership = player.leadership;

            _power = player.intellect / 100 + 1;

            _cardCapacity = setMaxNumCard(player.intellect);
            _cardArray = new Card[CardCapacity];

            AttackShuffleProbability = BoardGameMode.DEFAULT_ATTACK_CARD_SHUFFLE_PROBABILITY + (BoardGameMode.STAT_WEIGHT * player.tactic / Def.MAX_STAT);
            DefenseShuffleProbability = BoardGameMode.DEFAULT_DEFENSE_CARD_SHUFFLE_PROBABILITY + (BoardGameMode.STAT_WEIGHT * player.leadership / Def.MAX_STAT);
        }

        public override void Push()
        {
            int value = GetHowMuchPush();
            Mode.MinusFlow(value);
        }

        public override void Bingo(int bingoCount)
        {
            int value = GetHowMuchBingo(bingoCount);
            Mode.MinusFlow(value);
        }

        public override void Attack()
        {
            int value = GetHowMuchAttack();
            Mode.MinusFlow(value);
        }

        public override void CounterAttack()
        {
            int value = Opponent.GetHowMuchAttack();
            Mode.MinusFlow(value);
        }

        public void Draw()
        {
            int drawCardIndex = draw();
            Pop(drawCardIndex);
        }

        private int draw()
        {
            if ((Opponent.NumAttack > 0) && (Opponent.NumDefense > 0))
            {
                if ((NumAttack > 0) && (NumDefense > 0))
                {
                    const int NUMBER_PROBABILITY = 10;  // 0~9
                    const int ATTACK_PROBABILITY = 45;  // 10~54
                    //const int DEFENSE_PROBABILITY = 45; // 55~99

                    int random = Random.Range(0, 100);
                    Log.Debug(string.Format("user(A,D) ai(A,D); random({0}), num p(10), att p(45), def p(45)", random));

                    if (random < NUMBER_PROBABILITY)
                        return getBestNumCardIndex();
                    else if (random < (NUMBER_PROBABILITY + ATTACK_PROBABILITY))
                        return getAttackCardIndex();
                    else
                        return getDefenseCardIndex();
                }
                else if (NumAttack > 0)
                {
                    const int NUMBER_PROBABILITY = 20; // 0~19
                    //const int ATTACK_PROBABILITY = 80; // 20~99

                    int random = Random.Range(0, 100);
                    Log.Debug(string.Format("user(A,D) ai(A); random({0}), num p(20), att p(80)", random));

                    if (random < NUMBER_PROBABILITY)
                        return getBestNumCardIndex();
                    else
                        return getAttackCardIndex();
                }
                else if (NumDefense > 0)
                {
                    const int NUMBER_PROBABILITY = 20; // 0~19
                    //const int DEFENSE_PROBABILITY = 80; // 20~99

                    int random = Random.Range(0, 100);
                    Log.Debug(string.Format("user(A,D) ai(D); random({0}), num p(20), def p(80)", random));

                    if (random < NUMBER_PROBABILITY)
                        return getBestNumCardIndex();
                    else
                        return getDefenseCardIndex();
                }
                else
                {
                    Log.Debug("user(A,D) ai(N)");

                    return getBestNumCardIndex();
                }
            }
            else if (Opponent.NumAttack > 0)
            {
                if ((NumAttack > 0) && (NumDefense > 0))
                {
                    Log.Debug("user(A) ai(A,D)");

                    return getAttackCardIndex();
                }
                else if (NumAttack > 0)
                {
                    Log.Debug("user(A) ai(A)");

                    return getAttackCardIndex();
                }
                else if (NumDefense > 0)
                {
                    const int NUMBER_PROBABILITY = 20; // 0~19
                    //const int DEFENSE_PROBABILITY = 80; // 20~99

                    int random = Random.Range(0, 100);
                    Log.Debug(string.Format("user(A) ai(D); random({0}), num p(20), def p(80)", random));

                    if (random < NUMBER_PROBABILITY)
                        return getBestNumCardIndex();
                    else
                        return getDefenseCardIndex();
                }
                else
                {
                    Log.Debug("user(A) ai(N)");

                    return getBestNumCardIndex();
                }
            }
            else if (Opponent.NumDefense > 0)
            {
                if ((NumAttack > 0) && (NumDefense > 0))
                {
                    Log.Debug("user(D) ai(A,D)");

                    return getBestNumCardIndex();
                }
                else if (NumAttack > 0)
                {
                    const int NUMBER_PROBABILITY = 80; // 0~79
                    //const int ATTACK_PROBABILITY = 20; // 80~99

                    int random = Random.Range(0, 100);
                    Log.Debug(string.Format("user(D) ai(A); random({0}), num p(80), att p(20)", random));

                    if (random < NUMBER_PROBABILITY)
                        return getBestNumCardIndex();
                    else
                        return getAttackCardIndex();
                }
                else if (NumDefense > 0)
                {
                    Log.Debug("user(D) ai(D)");

                    return getBestNumCardIndex();
                }
                else
                {
                    Log.Debug("user(D) ai(N)");

                    return getBestNumCardIndex();
                }
            }
            else
            {
                if ((NumAttack > 0) && (NumDefense > 0))
                {
                    Log.Debug("user(N) ai(A,D)");

                    return getAttackCardIndex();
                }
                else if (NumAttack > 0)
                {
                    Log.Debug("user(N) ai(A)");

                    return getAttackCardIndex();
                }
                else if (NumDefense > 0)
                {
                    Log.Debug("user(N) ai(D)");

                    return getBestNumCardIndex();
                }
                else
                {
                    Log.Debug("user(N) ai(N)");

                    return getBestNumCardIndex();
                }
            }
        }

        private int getBestNumCardIndex()
        {
            BoardGameMode.ENumberCriterion criterion = Mode.Criterion;
            switch (criterion)
            {
                case BoardGameMode.ENumberCriterion.Low:
                    return getLowestNumCardIndex();

                case BoardGameMode.ENumberCriterion.High:
                    return getHighestNumCardIndex();

                default:
                    return -1;
            }
        }

        private int getLowestNumCardIndex()
        {
            int index = -1;
            int lowest = NumberCard.MAX_NO;

            int numCard = _cardArray.Length;
            for (int i = 0; i < numCard; ++i)
            {
                if (Card.EType.Number != _cardArray[i].Type)
                    continue;

                NumberCard numberCard = _cardArray[i] as NumberCard;

                int num = numberCard.No;
                if (num <= lowest)
                {
                    lowest = num;
                    index = i;
                }
            }

            return index;
        }

        private int getHighestNumCardIndex()
        {
            int index = -1;
            int highest = NumberCard.MIN_NO;

            int numCard = _cardArray.Length;
            for (int i = 0; i < numCard; ++i)
            {
                if (Card.EType.Number != _cardArray[i].Type)
                    continue;

                NumberCard numberCard = _cardArray[i] as NumberCard;

                int num = numberCard.No;
                if (num >= highest)
                {
                    highest = num;
                    index = i;
                }
            }

            return index;
        }

        private int getAttackCardIndex()
        {
            int numCard = _cardArray.Length;
            for (int i = 0; i < numCard; ++i)
            {
                if (Card.EType.Attack == _cardArray[i].Type)
                    return i;
            }

            return -1;
        }

        private int getDefenseCardIndex()
        {
            int numCard = _cardArray.Length;
            for (int i = 0; i < numCard; ++i)
            {
                if (Card.EType.Defense == _cardArray[i].Type)
                    return i;
            }

            return -1;
        }

    }   // class

}   // namespace
