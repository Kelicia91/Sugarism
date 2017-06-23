using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Nurture
{
    // 돈을 소비하는 action을 스케줄에 넣었는데 은전이 부족한 경우 강제로 셋팅.
    public class IdleAction : ActionController
    {
        public IdleAction(int id, MainCharacter mainCharacter) : base(id, mainCharacter)
        {
            // do nothing
        }

        protected override bool doing()
        {
            return true;
        }

    }   // class

}   // namespace
