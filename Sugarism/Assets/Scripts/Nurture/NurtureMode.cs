using System;
using UnityEngine;


namespace Nurture
{
    public class Mode
    {
        // field, property
        private Calendar _calendar = null;
        public Calendar Calendar { get { return _calendar; } }

        private Character _character = null;
        public Character Character { get { return _character; } }

        private Schedule _schedule = null;
        public Schedule Schedule { get { return _schedule; } }
        

        // constructor
        public Mode(Character character)
        {
            _calendar = new Calendar(Def.INIT_YEAR, Def.INIT_MONTH, Def.INIT_DAY);
            _character = character;
            _schedule = new Schedule(this, Def.MAX_NUM_ACTION_IN_MONTH);

            Calendar.YearChangeEvent.Attach(onYearChanged);
        }

        public TextAsset GetEndingScenario()
        {
            int endingId = getEndingId();
            Log.Debug(string.Format("ending id: {0}", endingId));

            if (false == ExtNurtureEnding.isValid(endingId))
            {
                Log.Error(string.Format("invalid nurture.ending id; {0}", endingId));
                return null;
            }

            NurtureEnding ending = Manager.Instance.DTNurtureEnding[endingId];
            Log.Debug(string.Format("ending name: {0}", ending.name));

            return ending.scenario;
        }

        private int getEndingId()
        {
            int endingId = 0;

            Log.Debug("getEndingId()");

            int STAT_COUNT = Manager.Instance.DTStat.Count;

            Array statLineEnumArray = Enum.GetValues(typeof(EStatLine));
            int STAT_LINE_ENUM_COUNT = statLineEnumArray.Length;

            for (int i = 0; i < STAT_LINE_ENUM_COUNT; ++i)
            {
                EStatLine statLine = (EStatLine) statLineEnumArray.GetValue(i);
                if (EStatLine.MAX == statLine)
                    continue;

                Log.Debug(string.Format("stat line: {0}", statLine));

                int count = 0, sum = 0;
                for (int statId = 0; statId < STAT_COUNT; ++statId)
                {
                    if (statLine != Manager.Instance.DTStat[statId].statLine)
                        continue;

                    if (false == Enum.IsDefined(typeof(EStat), statId))
                    {
                        Log.Error(string.Format("{0} can't convert to EStat", statId));
                        return -1;
                    }

                    EStat stat = (EStat)statId;
                    int statValue = Character.Get(stat);

                    Log.Debug(string.Format("stat: {0} ({1})", stat, statValue));

                    sum += statValue;
                    ++count;
                }

                Log.Debug(string.Format("sum: {0}, count: {1}", sum, count));
                if (count <= 0)
                    continue;

                int avgStat = Mathf.RoundToInt(sum / count);
                byte isFlagOn = getStatLineFlag(avgStat);

                Log.Debug(string.Format("avg: {0}, isFlagOn: {1}", avgStat, isFlagOn));
                
                int statLineWeight = Convert.ToByte(statLine) & isFlagOn;

                Log.Debug(string.Format("stat line weight: {0}", statLineWeight));

                endingId += statLineWeight;
            }

            return endingId;
        }

        public readonly int HALF_MAX_STAT = Mathf.RoundToInt(Def.MAX_STAT * 0.5f);
        private byte getStatLineFlag(int avgStat)
        {
            if (avgStat >= HALF_MAX_STAT)
                return byte.MaxValue;
            else
                return byte.MinValue;
        }

        //
        private void onYearChanged(int year)
        {
            int yearDiff = year - Calendar.INIT_YEAR;

            Character.Age += yearDiff;
        }

    }   // class

}   // namespace
