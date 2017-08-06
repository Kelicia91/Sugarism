using System;

namespace Sugarism
{
    public class CmdText : Command
    {
        // const
        public const int MAX_COUNT_LINE_END = 3;
        public const int MAX_LENGTH_LINE = 30;


        // property
        private string _text = null;
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }


        // default constructor for JSON Deserializer
        public CmdText() : base(Command.Type.Text) { }
    }
}
