
namespace ScenarioEditor.ViewModel.Popup
{
    public class Settings : ModelBase
    {
        #region Singleton

        private static Settings _instance = null;
        public static Settings Instance
        {
            get
            {
                if (null == _instance)
                    _instance = new Settings();

                return _instance;
            }
        }

        private Settings()
        {
            _characterFilePath = Properties.Resources.GuideFindPath;
        }

        #endregion //Singleton



        #region Properties

        private string _characterFilePath;
        public string CharacterFilePath
        {
            get { return _characterFilePath; }
            set { _characterFilePath = value; OnPropertyChanged(); }
        }

        #endregion //Properties
        


        #region Public Methods

        /// <summary>
        /// Show View.Popup.Settings.
        /// </summary>
        /// <param name="prevCharacterFilePath">CharacterFilePath before editing.</param>
        /// <returns>Edited CharacterFilePath. If window is cancled, return string.Empty.</returns>
        public string Show(string prevCharacterFilePath)
        {
            CharacterFilePath = prevCharacterFilePath;

            View.Popup.Settings view = new View.Popup.Settings(this);

            bool? result = view.ShowDialog();
            switch (result)
            {
                case true:
                    return CharacterFilePath;

                default:
                    return string.Empty;
            }
        }

        #endregion //Public Methods
    }
}
