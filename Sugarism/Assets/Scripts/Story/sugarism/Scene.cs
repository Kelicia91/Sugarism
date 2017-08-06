using System.Collections.Generic;


namespace Sugarism
{
    /// <summary>
    /// set of command, but it isn't command.
    /// </summary>
    public class Scene
    {
        // const
        public const int MAX_LENGTH_DESCRIPTION = 30;


        // property
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
        public Scene() { }
    }
}
