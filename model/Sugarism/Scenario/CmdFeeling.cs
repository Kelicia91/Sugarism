
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
        private int _targetId = -1;
        public int TargetId
        {
            get { return _targetId; }
            set { _targetId = value; OnPropertyChanged("TargetId"); }
        }

        private EOperation _op = EOperation.Add;
        public EOperation Op
        {
            get { return _op; }
            set { _op = value; OnPropertyChanged("Op"); }
        }

        private int _value = 0;
        public int Value
        {
            get { return _value; }
            set { _value = value;  OnPropertyChanged("Value"); }
        }


        // default constructor for JSON Deserializer
        public CmdFeeling() : this(-1) { }

        public CmdFeeling(int targetId) : this(targetId, EOperation.Add, 0) { }

        public CmdFeeling(int targetId, EOperation op, int value) : base(Command.Type.Feeling)
        {
            _targetId = targetId;
            _op = op;
            _value = value;
        }
    }
}
