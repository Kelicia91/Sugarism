using System;

namespace Sugarism
{
    /// <summary>
    /// minimum logic unit for scenario designer
    /// </summary>
    public class Command
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
            Disappear,

            MAX
        }


        // property
        private Command.Type _cmdType = Command.Type.MAX;
        public Command.Type CmdType
        {
            get { return _cmdType; }
            set { _cmdType = value; }
        }


        // default constructor for JSON Deserializer
        public Command() : this(Command.Type.MAX) { }

        protected Command(Command.Type cmdType)
        {
            _cmdType = cmdType;
        }
    }
}
