
namespace ScenarioEditor.ViewModel.Popup
{
    public class EditSE : ModelBase
    {
        #region Singleton

        private static EditSE _instance = null;
        public static EditSE Instance
        {
            get
            {
                if (null == _instance)
                    _instance = new EditSE();

                return _instance;
            }
        }

        private EditSE()
        {
            // do
        }

        #endregion //Singleton



        #region Property

        public Model.ArtsResource[] SEList
        {
            get { return Common.Instance.SEList; }
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
        /// Show View.Popup.EditSE.
        /// </summary>
        /// <param name="backgroundId">SE Id before editing.</param>
        /// <returns>Whether edit or not.</returns>
        public bool Show(int backgroundId)
        {
            reset(backgroundId);

            View.Popup.EditSE view = new View.Popup.EditSE(this);

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
            if (Common.Instance.IsValidSE(backgroundId))
            {
                SelectedItem = SEList[backgroundId];
            }
            else
            {
                if (null == SEList)
                    SelectedItem = null;
                else if (SEList.Length > 0)
                    SelectedItem = SEList[0];
                else
                    SelectedItem = null;
            }
        }

        #endregion //Private Method
    }
}
