
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
            _filePath = Properties.Resources.GuideFindPath;
        }

        #endregion //Singleton



        #region Properties

        private string _label;
        public string Label
        {
            get { return _label; }
            set { _label = value;  OnPropertyChanged(); }
        }

        private string _prevFilePath;   // before editing
        public string PrevFilePath
        {
            get { return _prevFilePath; }
        }

        private string _filePath;
        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = value; OnPropertyChanged(); }
        }

        #endregion //Properties
        


        #region Public Methods

        /// <summary>
        /// Show View.Popup.Settings.
        /// </summary>
        /// <param name="prevFilePath">FilePath before editing.</param>
        /// <returns>Edited FilePath. If window is cancled, return string.Empty.</returns>
        public string Show(string label, string prevFilePath)
        {
            Label = label;
            _prevFilePath = prevFilePath;
            FilePath = _prevFilePath;

            View.Popup.Settings view = new View.Popup.Settings(this);

            bool? result = view.ShowDialog();
            switch (result)
            {
                case true:
                    return FilePath;

                default:
                    return string.Empty;
            }
        }

        #endregion //Public Methods
    }
}
