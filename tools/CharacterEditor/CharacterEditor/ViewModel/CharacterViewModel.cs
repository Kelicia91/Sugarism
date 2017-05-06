
using Sugarism;

namespace CharacterEditor
{
    /// <summary>
    /// Character Input Format
    /// </summary>
    class CharacterViewModel : ModelBase
    {
        #region Consts

        public string NameGuideText
        {
            get { return string.Format(Properties.Resources.CNameGuideText, Character.MIN_LENGTH_OF_NAME, Character.MAX_LENGTH_OF_NAME); }
        }

        #endregion //Consts


        #region Properties
        
        private string _name;  // for textbox
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        #endregion //Properties


        #region Methods

        public void Init()
        {
            Name = string.Empty;
        }

        
        public bool IsValid()
        {
            Character.ValidationResult result = Character.IsValid(Name);

            string errMsg = string.Empty;
            switch (result)
            {
                case Character.ValidationResult.Success:
                    return true;

                case Character.ValidationResult.UnderMinLengthName:
                    errMsg = string.Format(Properties.Resources.ErrUnderMinLengthCName, Character.MIN_LENGTH_OF_NAME);
                    break;

                case Character.ValidationResult.OverMaxLengthName:
                    errMsg = string.Format(Properties.Resources.ErrOverMaxLengthCName, Character.MAX_LENGTH_OF_NAME);
                    break;

                default:
                    errMsg = string.Format(Properties.Resources.ErrSysError);
                    break;
            }

            Log.Error(Properties.Resources.ErrMsgBoxTitle, errMsg);
            return false;
        }

        #endregion //Methods
    }
}
