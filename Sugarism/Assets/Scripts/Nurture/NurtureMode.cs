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

        public int GetEndingId()
        {
            int endingId = 0;
            
            Array statLineEnumArray = Enum.GetValues(typeof(EStatLine));
            int STAT_LINE_ENUM_COUNT = statLineEnumArray.Length;

            for (int i = 0; i < STAT_LINE_ENUM_COUNT; ++i)
            {
                EStatLine statLine = (EStatLine) statLineEnumArray.GetValue(i);
                if (EStatLine.MAX == statLine)
                    continue;
                
                int statAvg = Character.GetAverage(statLine);
                byte isFlagOn = getStatLineFlag(statAvg);
                //Log.Debug(string.Format("avg: {0}, isFlagOn: {1}", statAvg, isFlagOn));
                
                int statLineWeight = Convert.ToByte(statLine) & isFlagOn;
                //Log.Debug(string.Format("stat line weight: {0}", statLineWeight));

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
