
namespace Sugarism
{
    public enum EOperation
    {
        Add,        // +
        Subtract,   // -
        Assign      // =
    }

    public class CmdFeeling : Command
    {
        // property
        private EOperation _op;
        public EOperation Op
        {
            get { return _op; }
            set { _op = value; OnPropertyChanged("Op"); }
        }

        private int _value;
        public int Value
        {
            get { return _value; }
            set { _value = value;  OnPropertyChanged("Value"); }
        }


        // default constructor for JSON Deserializer
        public CmdFeeling() : this(EOperation.Add, 0) { }

        public CmdFeeling(EOperation op, int value) : base(Command.Type.Feeling)
        {
            _op = op;
            _value = value;
        }
    }
}
