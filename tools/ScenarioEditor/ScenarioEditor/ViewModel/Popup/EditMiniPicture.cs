
namespace ScenarioEditor.ViewModel.Popup
{
    public class EditMiniPicture : ModelBase
    {
        #region Singleton

        private static EditMiniPicture _instance = null;
        public static EditMiniPicture Instance
        {
            get
            {
                if (null == _instance)
                    _instance = new EditMiniPicture();

                return _instance;
            }
        }

        private EditMiniPicture()
        {
            // do
        }

        #endregion //Singleton



        #region Property

        public Model.ArtsResource[] MiniPictureList
        {
            get { return Common.Instance.MiniPictureList; }
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
        /// Show View.Popup.EditMiniPicture.
        /// </summary>
        /// <param name="miniPictureId">MiniPicture Id before editing.</param>
        /// <returns>Whether edit or not.</returns>
        public bool Show(int miniPictureId)
        {
            reset(miniPictureId);

            View.Popup.EditMiniPicture view = new View.Popup.EditMiniPicture(this);

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

        private void reset(int miniPictureId)
        {
            if (Common.Instance.IsValidMiniPicture(miniPictureId))
            {
                SelectedItem = MiniPictureList[miniPictureId];
            }
            else
            {
                if (null == MiniPictureList)
                    SelectedItem = null;
                else if (MiniPictureList.Length > 0)
                    SelectedItem = MiniPictureList[0];
                else
                    SelectedItem = null;
            }
        }

        #endregion //Private Method
    }
}
