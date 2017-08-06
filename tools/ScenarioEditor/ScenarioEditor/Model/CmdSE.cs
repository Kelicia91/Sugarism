
namespace Sugarism
{
    public class CmdSE : Command
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
        public CmdSE() : this(-1) { }

        public CmdSE(int id) : base(Command.Type.SE)
        {
            _id = id;
        }
    }
}
