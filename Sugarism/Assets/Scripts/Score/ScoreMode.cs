using System;
using UnityEngine;

namespace Score
{
    public enum EGrade
    {
        D = 0,
        C, B, A, S,
        MAX
    }

    public class ScoreMode
    {
        public const int PERFECT_SCORE = 100;

        // constructor
        public ScoreMode()
        {

        }

        public EGrade GetGrade(int score)
        {
            if (score < 0)
                return EGrade.MAX;
            else if (score > PERFECT_SCORE)
                return EGrade.MAX;

            Array eGradeArray = Enum.GetValues(typeof(EGrade));

            int numGrade = eGradeArray.Length - 1;
            int score_range_between_grade = PERFECT_SCORE / numGrade;

            int quotient = score / score_range_between_grade;

            if (quotient >= numGrade)
                return EGrade.S;
            else
                return (EGrade)eGradeArray.GetValue(quotient);
        }

        public int GetScore(Player p, StatWeight[] statWeight)
        {
            if (null == p)
            {
                Log.Error("not found player");
                return 0;
            }

            if (null == statWeight)
            {
                Log.Error("not found stat weight");
                return 0;
            }

            if (false == isValidWeight(statWeight))
            {
                Log.Error("invalid SUM(stat.Weight)");
                return 0;
            }

            return getScore(p, statWeight);
        }

        private int getScore(Player p, StatWeight[] statWeight)
        {
            int statWeightArrayLength = statWeight.Length;

            int[] elements = new int[statWeightArrayLength];
            float normalized = 0.0f;

            for (int i = 0; i < statWeightArrayLength; ++i)
            {
                int statValue = p.GetStat(statWeight[i].StatType);

                if (EStat.STRESS == statWeight[i].StatType)
                    normalized = 1 - (((float)statValue) / Def.MAX_STAT);
                else
                    normalized = ((float)statValue) / Def.MAX_STAT;

                elements[i] = Mathf.RoundToInt(statWeight[i].Weight * normalized);
            }

            int score = 0;
            for (int i = 0; i < statWeightArrayLength; ++i)
            {
                score += elements[i];
            }

            return score;
        }

        private bool isValidWeight(StatWeight[] statWeight)
        {
            int sum = 0;

            int statWeightLength = statWeight.Length;
            for (int i = 0; i < statWeightLength; ++i)
            {
                sum += statWeight[i].Weight;
            }

            if (PERFECT_SCORE == sum)
                return true;
            else
                return false;
        }
        
    }   // class

}   // namespace
