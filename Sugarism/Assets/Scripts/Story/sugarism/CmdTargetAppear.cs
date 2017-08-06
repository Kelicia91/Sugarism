
namespace Sugarism
{
    public enum EFace
    {
        Default = 0,
        Happy,
        Sad,
        Angry,
        Shock
    }

    public enum ECostume
    {
        Default = 0,
        Private
    }

    public class CmdTargetAppear : Command
    {
        // property
        private int _targetId = -1;
        public int TargetId
        {
            get { return _targetId; }
            set { _targetId = value; }
        }

        private bool _isBlush = false;
        public bool IsBlush
        {
            get { return _isBlush; }
            set { _isBlush = value;}
        }

        private EFace _face = EFace.Default;
        public EFace Face
        {
            get { return _face; }
            set { _face = value; }
        }

        private ECostume _costume = ECostume.Default;
        public ECostume Costume
        {
            get { return _costume; }
            set { _costume = value; }
        }

        private EPosition _position = EPosition.Middle;
        public EPosition Position
        {
            get { return _position; }
            set { _position = value; }
        }
        

        // default constructor for JSON Deserializer
        public CmdTargetAppear() : base(Command.Type.TargetAppear) { }
    }
}
