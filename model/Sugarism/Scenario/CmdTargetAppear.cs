
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
            set { _targetId = value; OnPropertyChanged("TargetId"); }
        }

        private bool _isBlush = false;
        public bool IsBlush
        {
            get { return _isBlush; }
            set { _isBlush = value; OnPropertyChanged("IsBlush"); }
        }

        private EFace _face = EFace.Default;
        public EFace Face
        {
            get { return _face; }
            set { _face = value; OnPropertyChanged("Face"); }
        }

        private ECostume _costume = ECostume.Default;
        public ECostume Costume
        {
            get { return _costume; }
            set { _costume = value; OnPropertyChanged("Costume"); }
        }

        private EPosition _position = EPosition.Middle;
        public EPosition Position
        {
            get { return _position; }
            set { _position = value; OnPropertyChanged("Position"); }
        }
        

        // default constructor for JSON Deserializer
        public CmdTargetAppear() : this(-1) { }

        public CmdTargetAppear(int targetId) : this(targetId, false, EFace.Default, ECostume.Default, EPosition.Middle) { }

        public CmdTargetAppear(int targetId, bool isBlush, EFace face, ECostume costume, EPosition pos) 
            : base(Command.Type.TargetAppear)
        {
            _targetId = targetId;
            _isBlush = isBlush;
            _face = face;
            _costume = costume;
            _position = pos;
        }
    }
}
