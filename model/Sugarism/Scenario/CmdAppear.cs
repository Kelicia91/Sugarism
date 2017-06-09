using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sugarism
{
    public enum EFace
    {
        Normal,
        Angry,
        Happy,
        Surprise,
        Shy
    }

    public enum ECostume
    {
        Normal,
        Special // temp value
    }

    public enum EPosition
    {
        Middle = 0,
        Left,
        Right,
        Front,
        Back,
        None
    }

    public class CmdAppear : Command
    {
        // property
        private int _characterId;
        public int CharacterId
        {
            get { return _characterId; }
            set { _characterId = value; OnPropertyChanged("CharacterId"); }
        }

        private EFace _face;
        public EFace Face
        {
            get { return _face; }
            set { _face = value; OnPropertyChanged("Face"); }
        }

        private ECostume _costume;
        public ECostume Costume
        {
            get { return _costume; }
            set { _costume = value; OnPropertyChanged("Costume"); }
        }

        private EPosition _position;
        public EPosition Position
        {
            get { return _position; }
            set { _position = value; OnPropertyChanged("Position"); }
        }


        // default constructor for JSON Deserializer
        public CmdAppear() : this(-1, EFace.Normal, ECostume.Normal, EPosition.Middle) { }

        public CmdAppear(int characterId, EFace face, ECostume costume, EPosition pos) : base(Command.Type.Appear)
        {
            _characterId = characterId;
            _face = face;
            _costume = costume;
            _position = pos;
        }
    }
}
