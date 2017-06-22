using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class UserPlayer : Player
    {
        // constructor
        public UserPlayer(CombatMode mode) : base(mode, Def.MAIN_CHARACTER_ID)
        {
            MainCharacter main = Manager.Instance.Object.MainCharacter;
            _hp = main.Stamina;
            _mp = main.Intellect;
            _attack = main.Attack;
            _defense = main.Defence;
            _intellect = main.Intellect;
            _tactic = main.Tactic;

            _name = main.Name;
        }

    }   // class

}   // namespace