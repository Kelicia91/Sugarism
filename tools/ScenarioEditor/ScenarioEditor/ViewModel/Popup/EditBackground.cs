
namespace ScenarioEditor.ViewModel.Popup
{
    public class EditBackground : ModelBase
    {
        #region Singleton

        private static EditBackground _instance = null;
        public static EditBackground Instance
        {
            get
            {
                if (null == _instance)
                    _instance = new EditBackground();

                return _instance;
            }
        }

        private EditBackground()
        {
            // do
        }

        #endregion //Singleton



        #region Property

        public Model.ArtsResource[] BackgroundList
        {
            get { return Common.Instance.BackgroundList; }
        }

        private Model.ArtsResource _selectedItem;
        public Model.ArtsResource SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged(); }
        }

        #endregion //Property



        #region Public Method

        /// <summary>
        /// Show View.Popup.EditBackground.
        /// </summary>
        /// <param name="backgroundId">Background Id before editing.</param>
        /// <returns>Whether edit or not.</returns>
        public bool Show(int backgroundId)
        {
            reset(backgroundId);

            View.Popup.EditBackground view = new View.Popup.EditBackground(this);

            bool? result = view.ShowDialog();
            switch (result)
            {
                case true:
                    return true;

                default:
                    return false;
            }
        }

        #endregion //Public Method



        #region Private Method

        private void reset(int backgroundId)
        {
            if (Common.Instance.IsValidBackground(backgroundId))
            {
                SelectedItem = BackgroundList[backgroundId];
            }
            else
            {
                if (null == BackgroundList)
                    SelectedItem = null;
                else if (BackgroundList.Length > 0)
                    SelectedItem = BackgroundList[0];
                else
                    SelectedItem = null;
            }
        }

        #endregion //Private Method
    }
}
