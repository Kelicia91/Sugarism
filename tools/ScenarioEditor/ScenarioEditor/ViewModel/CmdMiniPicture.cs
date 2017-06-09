
namespace ScenarioEditor.ViewModel
{
    public sealed class CmdMiniPicture : Command
    {
        public CmdMiniPicture(Sugarism.CmdMiniPicture model) : base(model)
        {
            _model = model;

            Common.Instance.MiniPictureListChangeEvent.Attach(onMiniPictureListChanged);
        }

        ~CmdMiniPicture()
        {
            Common.Instance.MiniPictureListChangeEvent.Detach(onMiniPictureListChanged);
        }


        #region Field

        private Sugarism.CmdMiniPicture _model;

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
                if (Common.Instance.IsValidMiniPicture(Id))
                    return Common.Instance.MiniPictureList[Id].Description;
                else
                    return Sugarism.CmdMiniPicture.STR_UNKNOWN;
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
            bool isEdited = Popup.EditMiniPicture.Instance.Show(Id);
            if (false == isEdited)
                return;

            if (null != Popup.EditMiniPicture.Instance.SelectedItem)
                Id = Popup.EditMiniPicture.Instance.SelectedItem.Id;
            else
                Log.Error(Properties.Resources.ErrNotFoundMiniPicture);
        }

        public override string ToString()
        {
            string content = string.Format("{0}:{1}", Id, RefDescription);
            return ToString(content);
        }

        #endregion //Public Method



        #region Private Method

        // callback handler
        private void onMiniPictureListChanged()
        {
            OnPropertyChanged("ToText");
        }

        #endregion //Private Method
    }
}
