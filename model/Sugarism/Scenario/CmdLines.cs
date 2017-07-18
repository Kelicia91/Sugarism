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
            set { _characterId = value; OnPropertyChanged("CharacterId"); }
        }

        private bool _isAnonymous = false;
        public bool IsAnonymous
        {
            get { return _isAnonymous; }
            set { _isAnonymous = value; OnPropertyChanged("IsAnonymous"); }
        }

        private string _lines = null;
        public string Lines
        {
            get { return _lines; }
            set { _lines = value; OnPropertyChanged("Lines"); }
        }

        private ELinesEffect _linesEffect = ELinesEffect.None;
        public ELinesEffect LinesEffect
        {
            get { return _linesEffect; }
            set { _linesEffect = value; OnPropertyChanged("LinesEffect"); }
        }


        // default constructor for JSON Deserializer
        public CmdLines() : this(-1, null) { }

        public CmdLines(int characterId) : this(characterId, string.Empty) { }

        public CmdLines(int characterId, string lines) : base(Command.Type.Lines)
        {
            _characterId = characterId;
            _isAnonymous = false;
            _lines = lines;            
            _linesEffect = ELinesEffect.None;
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
