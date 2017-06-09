
namespace ScenarioEditor.ViewModel
{
    public sealed class CmdBackground : Command
    {
        public CmdBackground(Sugarism.CmdBackground model) : base(model)
        {
            _model = model;

            Common.Instance.BackgroundListChangeEvent.Attach(onBackgroundListChanged);
        }

        ~CmdBackground()
        {
            Common.Instance.BackgroundListChangeEvent.Detach(onBackgroundListChanged);
        }

        
        #region Field

        private Sugarism.CmdBackground _model;

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
                if (Common.Instance.IsValidBackground(Id))
                    return Common.Instance.BackgroundList[Id].Description;
                else
                    return Sugarism.CmdBackground.STR_UNKNOWN;
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
            bool isEdited = Popup.EditBackground.Instance.Show(Id);
            if (false == isEdited)
                return;

            if (null != Popup.EditBackground.Instance.SelectedItem)
                Id = Popup.EditBackground.Instance.SelectedItem.Id;
            else
                Log.Error(Properties.Resources.ErrNotFoundBackground);
        }

        public override string ToString()
        {
            string content = string.Format("{0}:{1}", Id, RefDescription);
            return ToString(content);
        }

        #endregion //Public Method



        #region Private Method

        // callback handler
        private void onBackgroundListChanged()
        {
            OnPropertyChanged("ToText");
        }

        #endregion //Private Method
    }
}
