using System;

namespace Sugarism
{
    public class CmdText : Command
    {
        // const
        public const int MAX_COUNT_LINE_END = 3;
        public const int MAX_LENGTH_LINE = 30;


        // property
        private string _text = null;
        public string Text
        {
            get { return _text; }
            set { _text = value; OnPropertyChanged("Text"); }
        }


        // default constructor for JSON Deserializer
        public CmdText() : this(null) { }

        public CmdText(string text) : base(Command.Type.Text)
        {
            _text = text;
        }


        // method : validation
        public enum ValidationResult
        {
            Success,

            TextIsNull,
            OverMaxCountLineEnd,
            OverMaxLengthLine
        }

        public static ValidationResult IsValid(string text)
        {
            ValidationResult result;

            result = IsValidText(text);
            if (ValidationResult.Success != result)
                return result;

            return ValidationResult.Success;
        }

        public static ValidationResult IsValidText(string text)
        {
            if (null == text)
                return ValidationResult.TextIsNull;

            string[] textPerNewLine = text.Split(LINE_SEPARATORS, StringSplitOptions.None);

            int numOfNewLines = textPerNewLine.Length;
            if (numOfNewLines > MAX_COUNT_LINE_END)
            {
                return ValidationResult.OverMaxCountLineEnd;
            }

            foreach (string line in textPerNewLine)
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
