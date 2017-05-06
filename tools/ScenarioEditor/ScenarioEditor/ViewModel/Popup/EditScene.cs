
namespace ScenarioEditor.ViewModel.Popup
{
    public class EditScene : ModelBase
    {
        #region Singleton

        private static EditScene _instance = null;
        public static EditScene Instance
        {
            get
            {
                if (null == _instance)
                    _instance = new EditScene();

                return _instance;
            }
        }

        private EditScene()
        {
            _description = string.Empty;
        }

        #endregion //Singleton



        #region Property

        private string _description;    // for textbox
        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(); }
        }

        public string GuideHowToInputDescription
        {
            get { return string.Format(Properties.Resources.GuideHowToInputDescription, Sugarism.Scene.MAX_LENGTH_DESCRIPTION); }
        }

        #endregion //Property



        #region Public Method

        /// <summary>
        /// Show View.Popup.EditScene.
        /// </summary>
        /// <param name="prevDescription">Description before editing.</param>
        /// <returns>Edited Description. If window is cancled, return string.Empty.</returns>
        public string Show(string prevDescription)
        {
            Description = prevDescription;
            
            View.Popup.EditScene view = new View.Popup.EditScene(this);

            bool? result = view.ShowDialog();
            switch(result)
            {
                case true:
                    return Description;

                default:
                    return string.Empty;
            }
        }

        #endregion //Public Method
    }
}
