
namespace Story
{
    public class CmdTargetAppear : Command
    {
        private Sugarism.CmdTargetAppear _model = null;

        public CmdTargetAppear(Sugarism.CmdTargetAppear model, Mode mode) : base(model, mode)
        {
            _model = model;
        }


        #region Property

        public int TargetId
        {
            get { return _model.TargetId; }
        }

        public bool IsBlush
        {
            get { return _model.IsBlush; }
        }

        public Sugarism.EFace Face
        {
            get { return _model.Face; }
        }

        public Sugarism.ECostume Costume
        {
            get { return _model.Costume; }
        }

        public Sugarism.EPosition Position
        {
            get { return _model.Position; }
        }

        #endregion


        public override void Execute()
        {
            Log.Debug(ToString());
        }

        public override bool Play()
        {
            Log.Debug(ToString());

            Mode.CmdTargetAppearEvent.Invoke(TargetId, IsBlush, Face, Costume, Position);

            return false;   // no more child to play
        }

        public override string ToString()
        {
            string s = string.Format(
                        "TargetId: {0}, Face: {1}, IsBlush: {2}, Costume: {3}, Pos: {4}",
                        TargetId, Face, IsBlush, Costume, Position);

            return ToString(s);
        }

    }   // class

}   // namespace
