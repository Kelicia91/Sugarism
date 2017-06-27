
namespace Score
{
    public abstract class Player
    {
        private int _id = -1;
        public int Id { get { return _id; } }

        protected int _stress = 0;
        public int Stress { get { return _stress; } }

        protected int _charm = 0;
        public int Charm { get { return _charm; } }

        protected int _sensibility = 0;
        public int Sensibility { get { return _sensibility; } }

        protected int _arts = 0;
        public int Arts { get { return _arts; } }

        // constructor
        protected Player(int id)
        {
            _id = id;
        }

        public int GetStat(EStat statType)
        {
            switch (statType)
            {
                case EStat.STRESS:
                    return Stress;

                case EStat.CHARM:
                    return Charm;

                case EStat.SENSIBILITY:
                    return Sensibility;

                case EStat.ARTS:
                    return Arts;

                default:
                    Log.Error(string.Format("invalid stat type : {0}", statType));
                    return Def.MIN_STAT;
            }
        }

    }   // class

}   // namespace
