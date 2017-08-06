
namespace Story
{
    public abstract class Command : IPlayable
    {
        private Mode _mode = null;
        protected Mode Mode { get { return _mode; } }

        private Sugarism.Command _model = null;

        public Command(Sugarism.Command model, Mode mode)
        {
            if (null == model)
            {
                Log.Error("Not Found Sugarism.Command");
                return;
            }

            _model = model;
            _mode = mode;
        }


        public static Command Create(Sugarism.Command model, Mode mode)
        {
            if (null == model)
                return null;

            if (null == mode)
            {
                Log.Error("Not Found Story.Mode");
                return null;
            }

            switch (model.CmdType)
            {
                case Sugarism.Command.Type.Lines:
                    return new CmdLines(model as Sugarism.CmdLines, mode);

                case Sugarism.Command.Type.Text:
                    return new CmdText(model as Sugarism.CmdText, mode);

                case Sugarism.Command.Type.Appear:
                    return new CmdAppear(model as Sugarism.CmdAppear, mode);

                case Sugarism.Command.Type.Background:
                    return new CmdBackground(model as Sugarism.CmdBackground, mode);

                case Sugarism.Command.Type.MiniPicture:
                    return new CmdMiniPicture(model as Sugarism.CmdMiniPicture, mode);

                case Sugarism.Command.Type.Picture:
                    return new CmdPicture(model as Sugarism.CmdPicture, mode);

                case Sugarism.Command.Type.Filter:
                    return new CmdFilter(model as Sugarism.CmdFilter, mode);

                case Sugarism.Command.Type.SE:
                    return new CmdSE(model as Sugarism.CmdSE, mode);

                case Sugarism.Command.Type.Feeling:
                    return new CmdFeeling(model as Sugarism.CmdFeeling, mode);

                case Sugarism.Command.Type.Switch:
                    return new CmdSwitch(model as Sugarism.CmdSwitch, mode);

                case Sugarism.Command.Type.Case:
                    return new CmdCase(model as Sugarism.CmdCase, mode);

                case Sugarism.Command.Type.TargetAppear:
                    return new CmdTargetAppear(model as Sugarism.CmdTargetAppear, mode);

                case Sugarism.Command.Type.Disappear:
                    return new CmdDisappear(model as Sugarism.CmdDisappear, mode);

                default:
                    return null;
            }
        }


        #region Property

        public Sugarism.Command.Type CmdType
        {
            get { return _model.CmdType; }
        }

        public string StrCmdType
        {
            get { return CmdType.ToString(); }
        }

        #endregion


        public abstract void Execute();
        public abstract bool Play();

        protected string ToString(string content)
        {
            string s = string.Format("[Cmd.{0}] {1}", StrCmdType, content);
            return s;
        }

    }   // class

}   // namespace
