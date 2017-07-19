
namespace ScenarioEditor.ViewModel
{
    public sealed class CmdDisappear : Command
    {
        public CmdDisappear(Sugarism.CmdDisappear model) : base(model)
        {
            _model = model;
        }


        #region Field

        private Sugarism.CmdDisappear _model = null;

        #endregion //Field


        #region Property

        public override string ToText
        {
            get { return ToString(); }
        }

        #endregion //Property


        #region Public Method

        public override void Edit()
        {
            return;
        }

        public override string ToString()
        {
            string content = string.Format("");

            return ToString(content);
        }

        #endregion //Public Method
    }
}
