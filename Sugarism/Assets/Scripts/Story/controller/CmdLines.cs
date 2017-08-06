
namespace Story
{
    public class CmdLines : Command
    {
        private Sugarism.CmdLines _model = null;

        public CmdLines(Sugarism.CmdLines model, Mode mode) : base(model, mode)
        {
            _model = model;
        }


        #region Property

        public int CharacterId
        {
            get { return _model.CharacterId; }
        }

        public string Lines
        {
            get { return _model.Lines; }
        }

        public bool IsAnonymous
        {
            get { return _model.IsAnonymous; }
        }

        public Sugarism.ELinesEffect LinesEffect
        {
            get { return _model.LinesEffect; }
        }

        #endregion


        public override void Execute()
        {
            Log.Debug(ToString());
        }

        public override bool Play()
        {
            Log.Debug(ToString());

            Mode.CmdLinesEvent.Invoke(CharacterId, IsAnonymous, Lines, LinesEffect);

            return false;   // no more child to play
        }

        public override string ToString()
        {
            string s = string.Format(
                        "CharacterId({0}), IsAnonymous({1}), LinesEffect({2}), Lines: \"{3}\"",
                        CharacterId, IsAnonymous, LinesEffect, Lines);

            return ToString(s);
        }

    }   // class

}   // namespace
