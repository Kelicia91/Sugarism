
namespace Nurture
{
    public class Calendar
    {
        public const int MIN_MONTH = 1;
        public const int MAX_MONTH = 12;
        public const int MIN_DAY = 1;

        // LastDay[0] is garbage value.
        public static readonly int[] LastDay = { -1, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        public readonly int INIT_YEAR = -1;

        #region Events

        private YearChangeEvent _yearChangeEvent = null;
        public YearChangeEvent YearChangeEvent { get { return _yearChangeEvent; } }

        private MonthChangeEvent _monthChangeEvent = null;
        public MonthChangeEvent MonthChangeEvent { get { return _monthChangeEvent; } }

        private DayChangeEvent _dayChangeEvent = null;
        public DayChangeEvent DayChangeEvent { get { return _dayChangeEvent; } }

        #endregion


        // constructor
        public Calendar(int year, int month, int day)
        {
            INIT_YEAR = year;
            _year = INIT_YEAR;
            _month = month;
            _day = day;
            
            _yearChangeEvent = new YearChangeEvent();
            _monthChangeEvent = new MonthChangeEvent();
            _dayChangeEvent = new DayChangeEvent();
        }

        private int _year = 0;
        public int Year
        {
            get { return _year; }
            private set
            {
                _year = value;
                YearChangeEvent.Invoke(_year);
            }
        }

        private int _month = 0;
        public int Month
        {
            get { return _month; }
            private set
            {
                _month = value;

                if (_month > MAX_MONTH)
                {
                    _month = MIN_MONTH;
                    ++Year;
                }

                MonthChangeEvent.Invoke(_month);
            }
        }

        private int _day = 0;
        public int Day
        {
            get { return _day; }
            set
            {
                _day = value;

                if (_day > LastDay[Month])
                {
                    _day = MIN_DAY;
                    ++Month;
                }

                DayChangeEvent.Invoke(_day);
            }
        }


        public ESeason Get()
        {
            if (Month <= 0)
                return ESeason.MAX;
            else if (Month <= 2)
                return ESeason.WINTER;
            else if (Month <= 5)
                return ESeason.SPRING;
            else if (Month <= 8)
                return ESeason.SUMMER;
            else if (Month <= 11)
                return ESeason.FALL;
            else if (Month <= 12)
                return ESeason.WINTER;
            else
                return ESeason.MAX;
        }
    }   // class

}   // namespace
