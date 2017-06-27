
namespace Score
{
    public class StatWeight
    {
        private EStat _statType = EStat.MAX;
        public EStat StatType { get { return _statType; } }

        private int _weight = 0;
        public int Weight { get { return _weight; } }

        public StatWeight(EStat statType, int weight)
        {
            _statType = statType;
            _weight = weight;
        }

    }   // class

}   // namespace