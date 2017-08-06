
namespace Story
{
    public class CmdFeeling : Command
    {
        private Sugarism.CmdFeeling _model = null;

        public CmdFeeling(Sugarism.CmdFeeling model, Mode mode) : base(model, mode)
        {
            _model = model;
        }


        #region Property

        public int TargetId
        {
            get { return _model.TargetId; }
        }

        public Sugarism.EOperation Op
        {
            get { return _model.Op; }
        }

        public int Value
        {
            get { return _model.Value; }
        }

        #endregion


        public override void Execute()
        {
            Log.Debug(ToString());
        }

        public override bool Play()
        {
            Log.Debug(ToString());

            Mode.CmdFeelingEvent.Invoke(TargetId, Op, Value);

            return false;   // no more child to play
        }

    }   // class
    
}   // namespace
