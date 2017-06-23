using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nurture
{
    public class VacationAction : ActionController
    {
        ESeason _season = ESeason.MAX;
        Vacation _vacation;

        public VacationAction(int id, MainCharacter mainCharacter) : base(id, mainCharacter)
        {
            _season = Manager.Instance.Object.Calendar.Get();
            if (ESeason.MAX == _season)
            {
                Log.Error("invalid season");
                return;
            }

            int seasonId = (int)_season;
            _vacation = Manager.Instance.DTVacation[seasonId];
        }

        protected override bool doing()
        {
            updateStat(_vacation);
            _MainCharacter.Money += _Action.money;

            return true;
        }

        private void updateStat(Vacation vacation)
        {
            _MainCharacter.Stamina += vacation.stamina;
            _MainCharacter.Intellect += vacation.intellect;
            _MainCharacter.Grace += vacation.grace;
            _MainCharacter.Charm += vacation.charm;

            _MainCharacter.Attack += vacation.attack;
            _MainCharacter.Defence += vacation.defense;

            _MainCharacter.Leadership += vacation.leadership;
            _MainCharacter.Tactic += vacation.tactic;

            _MainCharacter.Morality += vacation.morality;
            _MainCharacter.Goodness += vacation.goodness;

            _MainCharacter.Sensibility += vacation.sensibility;
            _MainCharacter.Arts += vacation.arts;
        }

    }   // class

}   // namespace