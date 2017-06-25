using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nurture
{
    public class RelaxAction : ActionController
    {
        public RelaxAction(int id, Mode mode) : base(id, mode) { }

        protected override void start()
        {
            _mode.Schedule.ActionStartEvent.Invoke(Id);
        }

        protected override void first()
        {
            _mode.Schedule.ActionFirstEvent.Invoke();
        }

        protected override void doing()
        {
            _mode.Currency.Money += _action.money;

            _mode.Schedule.ActionDoEvent.Invoke();
        }

        protected override void end()
        {
            _mode.Schedule.ActionEndEvent.Invoke();
        }

    }   // class

}   // namespace
