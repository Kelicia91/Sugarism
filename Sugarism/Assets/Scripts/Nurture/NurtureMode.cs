using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nurture
{
    public interface ICurrency
    {
        int Money { get; set; }
    }

    public class Mode
    {
        // field, property
        private ICurrency _currency = null;
        public ICurrency Currency { get { return _currency; } }

        private Calendar _calendar = null;
        public Calendar Calendar { get { return _calendar; } }

        private Character _character = null;
        public Character Character { get { return _character; } }

        private Schedule _schedule = null;
        public Schedule Schedule { get { return _schedule; } }
        

        // constructor
        public Mode(ICurrency currency)
        {
            _currency = currency;
            _calendar = new Calendar(Def.INIT_YEAR, Def.INIT_MONTH, Def.INIT_DAY);
            _character = new Character();
            _schedule = new Schedule(this, Def.MAX_NUM_ACTION_IN_MONTH);
        }

    }   // class

}   // namespace
