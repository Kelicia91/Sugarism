
namespace ScenarioEditor.ViewModel
{
    public sealed class CmdPicture : Command
    {
        public CmdPicture(Sugarism.CmdPicture model) : base(model)
        {
            _model = model;

            Common.Instance.PictureListChangeEvent.Attach(onPictureListChanged);
        }

        ~CmdPicture()
        {
            Common.Instance.PictureListChangeEvent.Detach(onPictureListChanged);
        }


        #region Field

        private Sugarism.CmdPicture _model;

        #endregion //Field


        #region Property

        public int Id
        {
            get { return _model.Id; }
            set
            {
                _model.Id = value;
                OnPropertyChanged();

                OnPropertyChanged("RefDescription");
                OnPropertyChanged("ToText");
            }
        }

        public string RefDescription
        {
            get
            {
                if (Common.Instance.IsValidPicture(Id))
                    return Common.Instance.PictureList[Id].Description;
                else
                    return Sugarism.CmdPicture.STR_UNKNOWN;
            }
        }

        public override string ToText
        {
            get { return ToString(); }
        }

        #endregion //Property



        #region Public Method

        public override void Edit()
        {
            bool isEdited = Popup.EditPicture.Instance.Show(Id);
            if (false == isEdited)
                return;

            if (null != Popup.EditPicture.Instance.SelectedItem)
                Id = Popup.EditPicture.Instance.SelectedItem.Id;
            else
                Log.Error(Properties.Resources.ErrNotFoundPicture);
        }

        public override string ToString()
        {
            string content = string.Format("{0}:{1}", Id, RefDescription);
            return ToString(content);
        }

        #endregion //Public Method



        #region Private Method

        // callback handler
        private void onPictureListChanged()
        {
            OnPropertyChanged("ToText");
        }

        #endregion //Private Method
    }
}
