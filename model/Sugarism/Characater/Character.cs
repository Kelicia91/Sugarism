
namespace Sugarism
{
    public class Character : Base.Model
    {
        // const
        public const int START_ID = 1;

        public const int MIN_LENGTH_OF_NAME = 1;
        public const int MAX_LENGTH_OF_NAME = 5;

        public const string STR_UNKNOWN = "unknown";


        // property
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged("Id"); }
        }
        
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("Name"); }
        }


        // default constructor for JSON Deserializer
        public Character() : this(-1, string.Empty) { }

        public Character(string name) : this(-1, name) { }

        public Character(int id, string name)
        {
            _id = id;
            _name = name;
        }


        // method : validation
        public enum ValidationResult
        {
            Success,

            NameIsNull,
            UnderMinLengthName,
            OverMaxLengthName
        }

        public static ValidationResult IsValid(string name)
        {
            ValidationResult result;

            result = IsValidName(name);
            if (ValidationResult.Success != result)
                return result;

            return ValidationResult.Success;
        }

        public static ValidationResult IsValidName(string name)
        {
            if (null == name)
                return ValidationResult.NameIsNull;

            if (name.Length < MIN_LENGTH_OF_NAME)
                return ValidationResult.UnderMinLengthName;

            if (name.Length > MAX_LENGTH_OF_NAME)
                return ValidationResult.OverMaxLengthName;

            return ValidationResult.Success;
        }
    }
}
