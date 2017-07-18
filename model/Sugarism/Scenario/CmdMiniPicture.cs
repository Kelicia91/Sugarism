
namespace Sugarism
{
    public class CmdMiniPicture : Command
    {
        // const
        public const int START_ID = 0;
        public const string STR_UNKNOWN = "unknown";


        // property
        private int _id = -1;
        public int Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged("Id"); }
        }


        // default constructor for JSON Deserializer
        public CmdMiniPicture() : this(-1) { }

        public CmdMiniPicture(int id) : base(Command.Type.MiniPicture)
        {
            _id = id;
        }
    }
}
