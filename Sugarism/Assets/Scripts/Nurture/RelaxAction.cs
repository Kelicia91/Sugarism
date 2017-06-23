using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nurture
{
    public class RelaxAction : ActionController
    {
        public RelaxAction(int id, MainCharacter mainCharacter) : base(id, mainCharacter)
        {
        }

        protected override bool doing()
        {
            _MainCharacter.Money += _Action.money;

            return true;
        }

    }   // class

}   // namespace
