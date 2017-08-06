
namespace Story
{
    public class CmdAppear : Command
    {
        private Sugarism.CmdAppear _model = null;

        public CmdAppear(Sugarism.CmdAppear model, Mode mode) : base(model, mode)
        {
            _model = model;
        }


        #region Property

        public int CharacterId
        {
            get { return _model.CharacterId; }
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

            Mode.CmdAppearEvent.Invoke(CharacterId, Position);

            return false;   // no more child to play
        }

        public override string ToString()
        {
            string s = string.Format(
                        "CharacterId: {0}, Pos: {1}",
                        CharacterId, Position);

            return ToString(s);
        }

    }   // class

}   // namespace
