
namespace Story
{
    public class CmdMiniPicture : Command
    {
        private Sugarism.CmdMiniPicture _model = null;

        public CmdMiniPicture(Sugarism.CmdMiniPicture model, Mode mode) : base(model, mode)
        {
            _model = model;
        }


        #region Property

        public int Id
        {
            get { return _model.Id; }
        }

        #endregion


        public override void Execute()
        {
            Log.Debug(ToString());
        }

        public override bool Play()
        {
            Log.Debug(ToString());

            Mode.CmdMiniPictureEvent.Invoke(Id);

            return false;   // no more child to play
        }

        public override string ToString()
        {
            string s = string.Format("Id({0})", Id);

            return ToString(s);
        }

    }   // class

}   // namespace
