using System;
using System.Windows.Input; // for ICommand


namespace ScenarioEditor.ViewModel
{
    public abstract class Command : ModelBase, ITreeViewItem
    {
        protected Command(Sugarism.Command model)
        {
            _model = model;
            _owner = null;

            _isExpanded = false;
            _isSelected = false;

            _inputBindings = new InputBindingCollection();

            // not expand key
            _inputBindings.Add(new KeyBinding(CmdEdit,      Key.E,      ModifierKeys.Control));
            _inputBindings.Add(new KeyBinding(CmdAddNext,   Key.Q,      ModifierKeys.Control));
            _inputBindings.Add(new KeyBinding(CmdDelete,    Key.D,      ModifierKeys.Control));
            _inputBindings.Add(new KeyBinding(CmdUp,        Key.Up,     ModifierKeys.Control));
            _inputBindings.Add(new KeyBinding(CmdDown,      Key.Down,   ModifierKeys.Control));
            // not addChild key
        }


        #region Static

        /// <summary>
        /// Create ViewModel from model.Type 
        /// </summary>
        /// <param name="model"></param>
        /// <returns>ViewModel corresponding to model.Type. 
        /// If model is null or model.Type is Sugarism.Command.Type.MAX, return null.</returns>
        public static Command Create(Sugarism.Command model)
        {
            if (null == model)
                return null;

            switch (model.CmdType)
            {
                case Sugarism.Command.Type.Lines:
                        return new CmdLines(model as Sugarism.CmdLines);

                case Sugarism.Command.Type.Switch:
                        return new CmdSwitch(model as Sugarism.CmdSwitch);

                case Sugarism.Command.Type.Case:
                        return new CmdCase(model as Sugarism.CmdCase);

                default:    // Sugarism.Command.Type.MAX
                        return null;
            }
        }

        #endregion //Static


        #region Field

        private Sugarism.Command _model;
        private IOwner<Command> _owner;

        private bool _isExpanded;
        private bool _isSelected;
        private InputBindingCollection _inputBindings;

        private ICommand _cmdExpand;
        private ICommand _cmdEdit;
        private ICommand _cmdAddNext;
        private ICommand _cmdDelete;
        private ICommand _cmdUp;
        private ICommand _cmdDown;

        #endregion //Field


        #region Property

        public Sugarism.Command Model
        {
            get { return _model; }
            set { _model = value; OnPropertyChanged(); OnPropertyChanged("StrCmdType"); }
        }
        
        public virtual IOwner<Command> Owner
        {
            get { return _owner; }
            set { _owner = value; OnPropertyChanged(); }
        }

        public bool IsExpanded
        {
            get { return _isExpanded; }
            set { _isExpanded = value; OnPropertyChanged(); }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; OnPropertyChanged(); }
        }

        public InputBindingCollection InputBindings
        {
            get { return _inputBindings; }
        }

        public string StrCmdType
        {
            get { return _model.CmdType.ToString(); }
        }
        
        public abstract string ToText { get; }

        #endregion //Property


        #region Command

        public ICommand CmdExpand
        {
            get
            {
                if (null == _cmdExpand)
                {
                    _cmdExpand = new RelayCommand(param => { IsExpanded = !IsExpanded; }, param => IsSelected);
                }
                return _cmdExpand;
            }
        }

        public ICommand CmdEdit
        {
            get
            {
                if (null == _cmdEdit)
                {
                    _cmdEdit = new RelayCommand(param => Edit(), param => IsSelected);
                }
                return _cmdEdit;
            }
        }
        
        public ICommand CmdAddNext
        {
            get
            {
                if (null == _cmdAddNext)
                {
                    _cmdAddNext = new RelayCommand(param => AddNext(), param => IsSelected);
                }
                return _cmdAddNext;
            }
        }
        
        public ICommand CmdDelete
        {
            get
            {
                if (null == _cmdDelete)
                {
                    _cmdDelete = new RelayCommand(param => Delete(), param => IsSelected);
                }
                return _cmdDelete;
            }
        }
        
        public ICommand CmdUp
        {
            get
            {
                if (null == _cmdUp)
                {
                    _cmdUp = new RelayCommand(param => Up(), param => IsSelected);
                }
                return _cmdUp;
            }
        }
        
        public ICommand CmdDown
        {
            get
            {
                if (null == _cmdDown)
                {
                    _cmdDown = new RelayCommand(param => Down(), param => IsSelected);
                }
                return _cmdDown;
            }
        }

        #endregion //Command



        #region Public Method

        public abstract void Edit();

        public virtual void AddNext()
        {
            if (false == canGetOwner())
                return;

            int nextIndex = Owner.GetIndexOf(this) + 1;

            Command newCmd = Popup.AddCmd.Instance.Show();
            if (null == newCmd)
                return;

            Owner.Insert(nextIndex, newCmd);
        }

        public virtual void Delete()
        {
            if (false == canGetOwner())
                return;

            Owner.Delete(this);
        }

        public virtual void Up()
        {
            if (false == canGetOwner())
                return;

            Owner.Up(this);
        }

        public virtual void Down()
        {
            if (false == canGetOwner())
                return;

            Owner.Down(this);
        }

        #endregion //Public Method



        #region Protected Method

        // return string attached Tag containing CmdType info.
        protected string ToString(string content)
        {
            int intCmdType = Convert.ToInt32(Model.CmdType);
            string tag = string.Format("{0}:{1}", intCmdType, StrCmdType);

            return string.Format("{0} {1}", tag, content);
        }

        protected bool canGetOwner()
        {
            if (null != Owner)
            {
                return true;
            }
            else
            {
                Log.Error(Properties.Resources.ErrNotFoundCmdOwner);
                return false;
            }
        }

        #endregion //Protected Method
    }
}
