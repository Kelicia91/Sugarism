using System.Collections.Generic;

namespace Sugarism
{
    public class CmdCase : Command
    {
        // const
        public const int START_KEY = 0;
        public const int MAX_LENGTH_DESCRIPTION = 30;


        // property
        private int _key = -1;
        public int Key
        {
            get { return _key; }
            set { _key = value; }
        }

        private string _description = null;
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        private List<Command> _cmdList = null;
        public List<Command> CmdList
        {
            get { return _cmdList; }
            set { _cmdList = value; }
        }


        // default constructor for JSON Deserializer
        public CmdCase() : base(Command.Type.Case) { }
    }

}
