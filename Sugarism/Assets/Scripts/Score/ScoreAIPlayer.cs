using UnityEngine;

namespace Score
{
    public class AIPlayer : Player
    {
        public AIPlayer(int id) : base(id)
        {
            _stress = getRandomStress();
        }

        public AIPlayer(int id, int charm, int sensibility, int arts) : base(id)
        {
            _stress = getRandomStress();
            _charm = charm;
            _sensibility = sensibility;
            _arts = arts;

            Log.Debug(string.Format("score.ai; stress({0}) charm({1}) sensibility({2}) arts({3})",
                                                Stress, Charm, Sensibility, Arts));
        }

        private int getRandomStress()
        {
            int stress = Random.Range(Def.MIN_STAT, Def.MAX_STAT);
            return stress;
        }

    }   // class

}   // namespace
