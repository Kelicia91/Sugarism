
namespace Combat
{
    public class UserPlayer : Player
    {
        // constructor
        public UserPlayer(CombatMode mode) : base(mode, Def.MAIN_CHARACTER_ID)
        {
            _name = Manager.Instance.Object.MainCharacter.Name;

            CombatPlayer p = Manager.Instance.DT.CombatPlayer[Id];
            _criticalProbability = p.criticalProbability;

            Nurture.Character c = Manager.Instance.Object.NurtureMode.Character;
            _hp = c.Stamina;
            _mp = c.Intellect;
            _attack = c.Attack;
            _defense = c.Defense;
            _intellect = c.Intellect;
            _tactic = c.Tactic;
        }

    }   // class

}   // namespace