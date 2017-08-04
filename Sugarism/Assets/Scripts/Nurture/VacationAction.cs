
namespace Nurture
{
    public class VacationAction : ActionController
    {
        //
        private ESeason _season = ESeason.MAX;
        private int _seasonId = -1;
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

            _seasonId = (int)_season;
            _vacation = Manager.Instance.DT.Vacation[_seasonId];
        }

        protected override void first()
        {
            string prefixKey = null;
            if (_mode.Character.IsChildHood())
                prefixKey = PlayerPrefsKey.ISLOCKED_VACATION_CHILD;
            else
                prefixKey = PlayerPrefsKey.ISLOCKED_VACATION_ADULT;

            string key = PlayerPrefsKey.GetKey(prefixKey, _seasonId);
            int value = PlayerPrefsKey.GetBoolToInt(false);
            CustomPlayerPrefs.SetInt(key, value);

            _mode.Schedule.ActionFirstEvent.Invoke();
        }

        protected override void doing()
        {
            _mode.Character.Money += _action.money;

            updateStats(_vacation);

            base.doing();
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

    }   // class

}   // namespace