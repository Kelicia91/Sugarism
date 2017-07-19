
namespace Story
{
    public class CmdDisappear : Command
    {
        private Sugarism.CmdDisappear _model = null;

        public CmdDisappear(Sugarism.CmdDisappear model, Mode mode) : base(model, mode)
        {
            _model = model;
        }

        #region Property
        #endregion

        public override void Execute()
        {
            Log.Debug(ToString());
        }

        public override bool Play()
        {
            Log.Debug(ToString());

            Mode.CmdDisappearEvent.Invoke();

            return false;   // no more child to play
        }
    }
}
