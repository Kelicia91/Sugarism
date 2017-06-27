using UnityEngine;

namespace Score
{
    public class AIPlayer : Player
    {
        public AIPlayer(int id) : base(id)
        {
            _stress = getRandomStress();
            ;   // @todo
        }

        public AIPlayer(int charm, int sensibility, int arts) : base(-1)
        {
            _stress = getRandomStress();
            _charm = charm;
            _sensibility = sensibility;
            _arts = arts;
        }

        private int getRandomStress()
        {
            int stress = Random.Range(Def.MIN_STAT, Def.MAX_STAT);
            return stress;
        }

    }   // class

}   // namespace
