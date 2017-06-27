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

    }   // class

}   // namespace
