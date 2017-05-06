
namespace Sugarism
{
    /// <summary>
    /// minimum logic unit for scenario designer
    /// </summary>
    public class Command : Base.Model
    {
        // enum : all child class inherited Command
        public enum Type
        {
            // Command
            Lines = 0,
            
            // Composite
            Switch,
            Case,

            MAX
        }


        // property
        private Command.Type _cmdType;
        public Command.Type CmdType
        {
            get { return _cmdType; }
            set { _cmdType = value; OnPropertyChanged("CmdType"); }
        }


        // default constructor for JSON Deserializer
        //public Command() : this(Command.Type.MAX) { }

        protected Command(Command.Type cmdType)
        {
            _cmdType = cmdType;
        }
    }
}
