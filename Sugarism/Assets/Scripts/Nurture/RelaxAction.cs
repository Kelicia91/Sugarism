
namespace Nurture
{
    public class RelaxAction : ActionController
    {
        public RelaxAction(int id, Mode mode) : base(id, mode) { }

        protected override void doing()
        {
            _mode.Character.Money += _action.money;

            base.doing();
        }

    }   // class

}   // namespace
