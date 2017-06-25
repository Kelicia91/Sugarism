using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Nurture
{
    // If true = Schedule.isLackMoney, Force to replace current action with this.
    public class IdleAction : ActionController
    {
        public IdleAction(int id, Mode mode) : base(id, mode) { }

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
            _mode.Schedule.ActionDoEvent.Invoke();
        }

        protected override void end()
        {
            _mode.Schedule.ActionEndEvent.Invoke();
        }

    }   // class

}   // namespace
