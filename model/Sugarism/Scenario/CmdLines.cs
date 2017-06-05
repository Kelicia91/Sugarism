using System;

namespace Sugarism
{
    public enum ELinesEffect
    {
        None,
        Shake
    }

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

    public class CmdLines : Command
    {
        // const
        public const int MAX_COUNT_LINE_END = 3;
        public const int MAX_LENGTH_LINE = 30;

        public readonly static string[] LINE_SEPARATORS = { Environment.NewLine };


        // property
        private int _characterId;
        public int CharacterId
        {
            get { return _characterId; }
            set { _characterId = value; OnPropertyChanged("CharacterId"); }
        }

        private string _lines;
        public string Lines
        {
            get { return _lines; }
            set { _lines = value; OnPropertyChanged("Lines"); }
        }

        private bool _isAnonymous;
        public bool IsAnonymous
        {
            get { return _isAnonymous; }
            set { _isAnonymous = value; OnPropertyChanged("IsAnonymous"); }
        }

        private ELinesEffect _linesEffect;
        public ELinesEffect LinesEffect
        {
            get { return _linesEffect; }
            set { _linesEffect = value; OnPropertyChanged("LinesEffect"); }
        }

        private EFace _face;
        public EFace Face
        {
            get { return _face; }
            set { _face = value;  OnPropertyChanged("Face"); }
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
        public CmdLines() : this(-1, null) { }

        public CmdLines(int characterId) : this(characterId, string.Empty) { }

        public CmdLines(int characterId, string lines) : base(Command.Type.Lines)
        {
            _characterId = characterId;
            _lines = lines;

            _isAnonymous = false;
            _linesEffect = ELinesEffect.None;
            _face = EFace.Normal;
            _costume = ECostume.Normal;
            _position = EPosition.Middle;
        }
        

        // method : validation
        public enum ValidationResult
        {
            Success,

            LinesIsNull,
            OverMaxCountLineEnd,
            OverMaxLengthLine
        }

        public static ValidationResult IsValid(string lines)
        {
            ValidationResult result;

            result = IsValidLines(lines);
            if (ValidationResult.Success != result)
                return result;

            return ValidationResult.Success;
        }

        public static ValidationResult IsValidLines(string lines)
        {
            if (null == lines)
                return ValidationResult.LinesIsNull;

            string[] linesPerNewLine = lines.Split(LINE_SEPARATORS, StringSplitOptions.None);

            int numOfNewLines = linesPerNewLine.Length;
            if (numOfNewLines > MAX_COUNT_LINE_END)
            {
                return ValidationResult.OverMaxCountLineEnd;
            }

            foreach (string line in linesPerNewLine)
            {
                if (line.Length > MAX_LENGTH_LINE)
                {
                    return ValidationResult.OverMaxLengthLine;
                }
            }

            return ValidationResult.Success;
        }
    }
}
