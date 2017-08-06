
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
            set { _targetId = value; }
        }

        private EOperation _op = EOperation.Add;
        public EOperation Op
        {
            get { return _op; }
            set { _op = value; }
        }

        private int _value = 0;
        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }


        // default constructor for JSON Deserializer
        public CmdFeeling() : base(Command.Type.Feeling) { }
    }
}
