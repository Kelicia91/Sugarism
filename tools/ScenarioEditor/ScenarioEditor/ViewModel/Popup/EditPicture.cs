
namespace ScenarioEditor.ViewModel.Popup
{
    public class EditPicture : ModelBase
    {
        #region Singleton

        private static EditPicture _instance = null;
        public static EditPicture Instance
        {
            get
            {
                if (null == _instance)
                    _instance = new EditPicture();

                return _instance;
            }
        }

        private EditPicture()
        {
            // do
        }

        #endregion //Singleton



        #region Property

        public Model.ArtsResource[] PictureList
        {
            get { return Common.Instance.PictureList; }
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
        /// Show View.Popup.EditPicture.
        /// </summary>
        /// <param name="pictureId">Picture Id before editing.</param>
        /// <returns>Whether edit or not.</returns>
        public bool Show(int pictureId)
        {
            reset(pictureId);

            View.Popup.EditPicture view = new View.Popup.EditPicture(this);

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

        private void reset(int pictureId)
        {
            if (Common.Instance.IsValidPicture(pictureId))
            {
                SelectedItem = PictureList[pictureId];
            }
            else
            {
                if (null == PictureList)
                    SelectedItem = null;
                else if (PictureList.Length > 0)
                    SelectedItem = PictureList[0];
                else
                    SelectedItem = null;
            }
        }

        #endregion //Private Method
    }
}
