
namespace Sugarism
{
    public class CmdPicture : Command
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
        public CmdPicture() : this(-1) { }

        public CmdPicture(int id) : base(Command.Type.Picture)
        {
            _id = id;
        }
    }
}
