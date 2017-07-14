
namespace Story
{
    public class CmdPicture : Command
    {
        private Sugarism.CmdPicture _model = null;

        public CmdPicture(Sugarism.CmdPicture model, Mode mode) : base(model, mode)
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

            Mode.CmdPictureEvent.Invoke(Id);

            return false;   // no more child to play
        }

        public override string ToString()
        {
            string s = string.Format("Id({0})", Id);

            return ToString(s);
        }

    }   // class

}   // namespace
