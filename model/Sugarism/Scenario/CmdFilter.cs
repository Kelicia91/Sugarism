
namespace Sugarism
{
    public enum EFilter
    {
        None = 0,
        Grayscale
    }

    public class CmdFilter : Command
    {
        // property
        private EFilter _filter;
        public EFilter Filter
        {
            get { return _filter; }
            set { _filter = value; OnPropertyChanged("Filter"); }
        }

        // default constructor for JSON Deserializer
        public CmdFilter() : this(EFilter.None) { }

        public CmdFilter(EFilter filter) : base(Command.Type.Filter)
        {
            _filter = filter;
        }
    }
}
