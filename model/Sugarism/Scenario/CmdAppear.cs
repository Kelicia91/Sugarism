
namespace Sugarism
{
    public enum EPosition
    {
        Middle = 0,
        Left,
        Right,
        Front,
        Back
    }

    public class CmdAppear : Command
    {
        // property
        private int _characterId = -1;
        public int CharacterId
        {
            get { return _characterId; }
            set { _characterId = value; OnPropertyChanged("CharacterId"); }
        }

        private EPosition _position = EPosition.Middle;
        public EPosition Position
        {
            get { return _position; }
            set { _position = value; OnPropertyChanged("Position"); }
        }


        // default constructor for JSON Deserializer
        public CmdAppear() : this(-1) { }

        public CmdAppear(int characterId) : this(characterId, EPosition.Middle) { }

        public CmdAppear(int characterId, EPosition pos) : base(Command.Type.Appear)
        {
            _characterId = characterId;
            _position = pos;
        }
    }
}
