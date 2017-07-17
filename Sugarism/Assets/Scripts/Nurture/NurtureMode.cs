
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

        private void onYearChanged(int year)
        {
            int yearDiff = year - Calendar.INIT_YEAR;

            Character.Age += yearDiff;
        }

    }   // class

}   // namespace
