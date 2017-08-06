using System;

namespace Sugarism
{
    public enum ELinesEffect
    {
        None,
        Shake
    }

    public class CmdLines : Command
    {
        // const
        public const int MAX_COUNT_LINE_END = 3;
        public const int MAX_LENGTH_LINE = 30;

        public const string ANONYMOUS = "???";


        // property
        private int _characterId = -1;
        public int CharacterId
        {
            get { return _characterId; }
            set { _characterId = value; }
        }

        private bool _isAnonymous = false;
        public bool IsAnonymous
        {
            get { return _isAnonymous; }
            set { _isAnonymous = value; }
        }

        private string _lines = null;
        public string Lines
        {
            get { return _lines; }
            set { _lines = value; }
        }

        private ELinesEffect _linesEffect = ELinesEffect.None;
        public ELinesEffect LinesEffect
        {
            get { return _linesEffect; }
            set { _linesEffect = value; }
        }


        // default constructor for JSON Deserializer
        public CmdLines() : base(Command.Type.Lines) { }
    }
}
