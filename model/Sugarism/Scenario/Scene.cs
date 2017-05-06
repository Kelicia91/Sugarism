using System.Collections.Generic;


namespace Sugarism
{
    /// <summary>
    /// set of command, but it isn't command.
    /// </summary>
    public class Scene : Base.Model
    {
        // const
        public const int MAX_LENGTH_DESCRIPTION = 30;


        // property
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
        public Scene()
        {
            _description = null;
            _cmdList = null;
        }

        public Scene(string description)
        {
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
