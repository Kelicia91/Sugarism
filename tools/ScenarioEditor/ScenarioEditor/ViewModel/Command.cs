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

                case Sugarism.Command.Type.Text:
                    return new CmdText(model as Sugarism.CmdText);

                case Sugarism.Command.Type.Appear:
                    return new CmdAppear(model as Sugarism.CmdAppear);

                case Sugarism.Command.Type.Background:
                    return new CmdBackground(model as Sugarism.CmdBackground);

                case Sugarism.Command.Type.MiniPicture:
                    return new CmdMiniPicture(model as Sugarism.CmdMiniPicture);

                case Sugarism.Command.Type.Picture:
                    return new CmdPicture(model as Sugarism.CmdPicture);

                case Sugarism.Command.Type.Filter:
                    return new CmdFilter(model as Sugarism.CmdFilter);

                case Sugarism.Command.Type.SE:
                    return new CmdSE(model as Sugarism.CmdSE);

                case Sugarism.Command.Type.Feeling:
                    return new CmdFeeling(model as Sugarism.CmdFeeling);
                    
                case Sugarism.Command.Type.Switch:
                    return new CmdSwitch(model as Sugarism.CmdSwitch);

                case Sugarism.Command.Type.Case:
                    return new CmdCase(model as Sugarism.CmdCase);

                case Sugarism.Command.Type.TargetAppear:
                    return new CmdTargetAppear(model as Sugarism.CmdTargetAppear);

                case Sugarism.Command.Type.Disappear:
                    return new CmdDisappear(model as Sugarism.CmdDisappear);

                default:    // Sugarism.Command.Type.MAX
                    return null;
            }
        }

        public static Command Create(Sugarism.Command.Type cmdType)
        {
            if (Sugarism.Command.Type.MAX == cmdType)
                return null;

            switch (cmdType)
            {
                case Sugarism.Command.Type.Lines:
                    return Create(new Sugarism.CmdLines(ScenarioEditor.Model.Character.START_ID));

                case Sugarism.Command.Type.Text:
                    return Create(new Sugarism.CmdText(string.Empty));

                case Sugarism.Command.Type.Appear:
                    return Create(new Sugarism.CmdAppear(ScenarioEditor.Model.Character.START_ID));

                case Sugarism.Command.Type.Background:
                    return Create(new Sugarism.CmdBackground(Sugarism.CmdBackground.START_ID));

                case Sugarism.Command.Type.MiniPicture:
                    return Create(new Sugarism.CmdMiniPicture(Sugarism.CmdMiniPicture.START_ID));

                case Sugarism.Command.Type.Picture:
                    return Create(new Sugarism.CmdPicture(Sugarism.CmdPicture.START_ID));

                case Sugarism.Command.Type.Filter:
                    return Create(new Sugarism.CmdFilter());

                case Sugarism.Command.Type.SE:
                    return Create(new Sugarism.CmdSE(Sugarism.CmdSE.START_ID));

                case Sugarism.Command.Type.Feeling:
                    return Create(new Sugarism.CmdFeeling(ScenarioEditor.Model.Character.START_ID));

                case Sugarism.Command.Type.Switch:
                    return Create(new Sugarism.CmdSwitch(ScenarioEditor.Model.Character.START_ID));

                // case Sugarism.Command.Type.Case: // prohibit user from adding it.

                case Sugarism.Command.Type.TargetAppear:
                    return Create(new Sugarism.CmdTargetAppear(ScenarioEditor.Model.Target.START_ID));

                case Sugarism.Command.Type.Disappear:
                    return Create(new Sugarism.CmdDisappear());

                default:
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

        private KeyBinding _keyBindEdit;
        private KeyBinding _keyBindAddNext;
        private KeyBinding _keyBindDelete;
        private KeyBinding _keyBindUp;
        private KeyBinding _keyBindDown;

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
