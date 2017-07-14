
namespace Story
{
    public class CmdFilter : Command
    {
        private Sugarism.CmdFilter _model = null;

        public CmdFilter(Sugarism.CmdFilter model, Mode mode) : base(model, mode)
        {
            _model = model;
        }


        #region Property

        public Sugarism.EFilter Filter
        {
            get { return _model.Filter; }
        }

        #endregion


        public override void Execute()
        {
            Log.Debug(ToString());
        }

        public override bool Play()
        {
            Log.Debug(ToString());

            Mode.CmdFilterEvent.Invoke(Filter);

            return false;   // no more child to play
        }

    }   // class

}   // namespace

