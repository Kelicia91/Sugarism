using System.Windows.Input;


namespace ScenarioEditor.ViewModel.Popup
{
    public class EditCase : ModelBase
    {
        // not Singlton. Refered from EditSwitchCase.
        public EditCase(int key, string description)
        {
            _key = key;
            _description = description;

            _owner = null;
        }


        #region Property

        private int _key;
        public int Key
        {
            get { return _key; }
            private set
            {
                _key = value;
                OnPropertyChanged();

                OnPropertyChanged("CanDelete");
            }
        }

        private string _description;
        public string Description
        {
            get
            {
                if (false == string.IsNullOrEmpty(_description))
                    return _description;
                else
                    return Properties.Resources.GuideCase;
            }
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        public bool CanDelete
        {
            get { return (Key >= Sugarism.CmdSwitch.MIN_COUNT_CASE); }
        }

        private EditSwitchCase _owner;
        public EditSwitchCase Owner
        {
            get { return _owner; }
            set { _owner = value; OnPropertyChanged(); }
        }

        #endregion //Property


        #region ICommand

        private ICommand _cmdDelete;
        public ICommand CmdDelete
        {
            get
            {
                if (null == _cmdDelete)
                {
                    _cmdDelete = new RelayCommand(param => Delete());
                }
                return _cmdDelete;
            }
        }

        #endregion //ICommand



        #region Public Method

        public void Delete()
        {
            if (null == Owner)
            {
                Log.Error(Properties.Resources.ErrNotFoundCmdOwner);
                return;
            }

            Owner.Delete(this);
        }

        #endregion //Public Method
    }
}
