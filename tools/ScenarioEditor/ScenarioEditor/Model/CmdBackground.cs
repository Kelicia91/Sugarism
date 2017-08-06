
namespace Sugarism
{
    public class CmdBackground : Command
    {
        // const
        public const int START_ID = 0;
        public const string STR_UNKNOWN = "unknown";


        // property
        private int _id = -1;
        public int Id
        {
            get { return _id; }
            set { _id = value;  OnPropertyChanged(); }
        }


        // default constructor for JSON Deserializer
        public CmdBackground() : this(-1) { }

        public CmdBackground(int id) : base(Command.Type.Background)
        {
            _id = id;
        }
    }
}
