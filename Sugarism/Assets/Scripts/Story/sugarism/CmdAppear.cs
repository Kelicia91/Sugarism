
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
            set { _characterId = value; }
        }

        private EPosition _position = EPosition.Middle;
        public EPosition Position
        {
            get { return _position; }
            set { _position = value; }
        }


        // default constructor for JSON Deserializer
        public CmdAppear() : base(Command.Type.Appear) { }
    }
}
