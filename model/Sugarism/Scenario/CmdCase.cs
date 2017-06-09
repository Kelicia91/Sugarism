using System.Collections.Generic;

namespace Sugarism
{
    public class CmdCase : Command
    {
        // const
        public const int START_KEY = 0;
        public const int MAX_LENGTH_DESCRIPTION = 30;


        // property
        private int _key;
        public int Key
        {
            get { return _key; }
            set { _key = value; OnPropertyChanged("Key"); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged("Description"); }
        }

        private List<Command> _cmdList;
        public List<Command> CmdList
        {
            get { return _cmdList; }
            set { _cmdList = value; OnPropertyChanged("CmdList"); }
        }


        // default constructor for JSON Deserializer
        public CmdCase() : base(Command.Type.Case)
        {
            _key = -1;
            _description = null;
            _cmdList = null;
        }

        public CmdCase(int key) : this(key, string.Empty) { }

        public CmdCase(int key, string description) : base(Command.Type.Case)
        {
            _key = key;
            _description = description;
            _cmdList = new List<Command>();
        }


        // method : validation
        public enum ValidationResult
        {
            Success,

            DescriptionIsNull,
            OverMaxLengthDescription
        }

        public static ValidationResult IsValid(string description)
        {
            ValidationResult result;

            result = IsValidDescription(description);
            if (ValidationResult.Success != result)
                return result;

            return ValidationResult.Success;
        }

        public static ValidationResult IsValidDescription(string description)
        {
            if (null == description)
                return ValidationResult.DescriptionIsNull;

            if (description.Length > MAX_LENGTH_DESCRIPTION)
                return ValidationResult.OverMaxLengthDescription;

            return ValidationResult.Success;
        }
    }

}
