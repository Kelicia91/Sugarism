using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nurture
{
    public class VacationAction : ActionController
    {
        //
        private ESeason _season = ESeason.MAX;
        private readonly Vacation _vacation;

        // constructor
        public VacationAction(int id, Mode mode) : base(id, mode)
        {
            _season = _mode.Calendar.Get();
            if (ESeason.MAX == _season)
            {
                Log.Error("invalid season");
                return;
            }

            int seasonId = (int)_season;
            _vacation = Manager.Instance.DTVacation[seasonId];
        }

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
            updateStats(_vacation);

            _mode.Currency.Money += _action.money;

            _mode.Schedule.ActionDoEvent.Invoke();
        }

        private void updateStats(Vacation vacation)
        {
            Character c = _mode.Character;

            c.Stamina += vacation.stamina;
            c.Intellect += vacation.intellect;
            c.Grace += vacation.grace;
            c.Charm += vacation.charm;

            c.Attack += vacation.attack;
            c.Defense += vacation.defense;

            c.Leadership += vacation.leadership;
            c.Tactic += vacation.tactic;

            c.Morality += vacation.morality;
            c.Goodness += vacation.goodness;

            c.Sensibility += vacation.sensibility;
            c.Arts += vacation.arts;
        }

        protected override void end()
        {
            _mode.Schedule.ActionEndEvent.Invoke();
        }

    }   // class

}   // namespace