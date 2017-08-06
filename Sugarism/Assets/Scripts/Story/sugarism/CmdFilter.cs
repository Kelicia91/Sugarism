
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
        private EFilter _filter = EFilter.None;
        public EFilter Filter
        {
            get { return _filter; }
            set { _filter = value; }
        }

        // default constructor for JSON Deserializer
        public CmdFilter() : base(Command.Type.Filter) { }
    }
}
