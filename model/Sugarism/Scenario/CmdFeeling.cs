
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
        private int _characterId;
        public int CharacterId
        {
            get { return _characterId; }
            set { _characterId = value; OnPropertyChanged("CharacterId"); }
        }

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
        public CmdFeeling() : this(-1, EOperation.Add, 0) { }

        public CmdFeeling(int characterId, EOperation op, int value) : base(Command.Type.Feeling)
        {
            _characterId = characterId;
            _op = op;
            _value = value;
        }
    }
}
