using System;

namespace Sugarism
{
    /// <summary>
    /// minimum logic unit for scenario designer
    /// </summary>
    public class Command : Base.Model
    {
        public readonly static string[] LINE_SEPARATORS = { Environment.NewLine };

        // enum : all child class inherited Command
        public enum Type
        {
            Lines = 0,
            Text,

            Appear,
            Background,
            MiniPicture,
            Picture,
            Filter,

            SE,

            Feeling,
            
            Switch,
            Case,

            TargetAppear,

            MAX
        }


        // property
        private Command.Type _cmdType = Command.Type.MAX;
        public Command.Type CmdType
        {
            get { return _cmdType; }
            set { _cmdType = value; OnPropertyChanged("CmdType"); }
        }


        // default constructor for JSON Deserializer
        public Command() : this(Command.Type.MAX) { }

        protected Command(Command.Type cmdType)
        {
            _cmdType = cmdType;
        }
    }
}
