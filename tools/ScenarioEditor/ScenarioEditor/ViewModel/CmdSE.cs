
namespace ScenarioEditor.ViewModel
{
    public sealed class CmdSE : Command
    {
        public CmdSE(Sugarism.CmdSE model) : base(model)
        {
            _model = model;

            Common.Instance.SEListChangeEvent.Attach(onSEListChanged);
        }

        ~CmdSE()
        {
            Common.Instance.SEListChangeEvent.Detach(onSEListChanged);
        }


        #region Field

        private Sugarism.CmdSE _model;

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
                if (Common.Instance.IsValidSE(Id))
                    return Common.Instance.SEList[Id].Description;
                else
                    return Sugarism.CmdSE.STR_UNKNOWN;
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
            bool isEdited = Popup.EditSE.Instance.Show(Id);
            if (false == isEdited)
                return;

            if (null != Popup.EditSE.Instance.SelectedItem)
                Id = Popup.EditSE.Instance.SelectedItem.Id;
            else
                Log.Error(Properties.Resources.ErrNotFoundSE);
        }

        public override string ToString()
        {
            string content = string.Format("{0}:{1}", Id, RefDescription);
            return ToString(content);
        }

        #endregion //Public Method



        #region Private Method

        // callback handler
        private void onSEListChanged()
        {
            OnPropertyChanged("ToText");
        }

        #endregion //Private Method
    }
}
