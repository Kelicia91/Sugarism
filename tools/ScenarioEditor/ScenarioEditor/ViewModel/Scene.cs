using System.Windows.Input; // for ICommand
using System.Collections.ObjectModel;


namespace ScenarioEditor.ViewModel
{
    public class Scene : ModelBase, IOwner<Command>, ITreeViewItem
    {
        public Scene(Sugarism.Scene model)
        {
            _model = model;

            _cmdList = new ObservableCollection<Command>();
            foreach (Sugarism.Command cmdModel in _model.CmdList)
            {
                Command cmdViewModel = Command.Create(cmdModel);
                if (null != cmdViewModel)
                {
                    CmdList.Add(cmdViewModel);
                    cmdViewModel.Owner = this;
                }
                else
                {
                    Log.Error(Properties.Resources.ErrInvalidCmdType);
                }
            }

            _owner = null;

            _isExpanded = false;
            _isSelected = false;

            _inputBindings = new InputBindingCollection();

            _inputBindings.Add(new KeyBinding(CmdExpand,    Key.Enter,  ModifierKeys.None));
            _inputBindings.Add(new KeyBinding(CmdEdit,      Key.E,      ModifierKeys.Control));
            _inputBindings.Add(new KeyBinding(CmdAddNext,   Key.Q,      ModifierKeys.Control));
            // not delete key
            _inputBindings.Add(new KeyBinding(CmdUp,        Key.Up,     ModifierKeys.Control));
            _inputBindings.Add(new KeyBinding(CmdDown,      Key.Down,   ModifierKeys.Control));
            _inputBindings.Add(new KeyBinding(CmdAddChild,  Key.A,      ModifierKeys.Control));

            if (_model.CmdList.Count <= 0)
                addSampleCmd();
        }


        #region Field

        private Sugarism.Scene _model;
        
        private ObservableCollection<Command> _cmdList;
        private IOwner<Scene> _owner;

        private bool _isExpanded;
        private bool _isSelected;
        private InputBindingCollection _inputBindings;

        private ICommand _cmdExpand;
        private ICommand _cmdEdit;
        private ICommand _cmdAddNext;
        private ICommand _cmdUp;
        private ICommand _cmdDown;
        private ICommand _cmdAddChild;

        #endregion Field


        #region Property
        
        public Sugarism.Scene Model { get { return _model; } }

        public string Description
        {
            get
            {
                if (false == string.IsNullOrEmpty(_model.Description))
                    return _model.Description;
                else
                    return Properties.Resources.GuideSceneDescription;
            }
            set
            {
                _model.Description = value;
                OnPropertyChanged();

                OnPropertyChanged("ToText");
            }
        }
        
        public ObservableCollection<Command> CmdList
        {
            get { return _cmdList; }
        }

        public IOwner<Scene> Owner
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

        public string ToText
        {
            get { return string.Format("{0}: {1}", Properties.Resources.Scene, Description); }
        }

        #endregion //Property


        #region ICommand

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
        
        public ICommand CmdAddChild // IOwner
        {
            get
            {
                if (null == _cmdAddChild)
                {
                    _cmdAddChild = new RelayCommand(param => AddFirstCmd(), param => IsSelected);
                }
                return _cmdAddChild;
            }
        }

        #endregion //ICommand



        #region Public Method

        public void Edit()
        {
            string result = Popup.EditScene.Instance.Show(Description);

            if (string.Empty == result)
                return;

            Description = result;
        }

        public void AddNext()
        {
            if (false == canGetOwner())
                return;

            int nextIndex = Owner.GetIndexOf(this) + 1;

            Sugarism.Scene model = new Sugarism.Scene(string.Empty);

            Scene newScene = new Scene(model);

            Owner.Insert(nextIndex, newScene);
        }

        public void Delete()
        {
            if (false == canGetOwner())
                return;

            Owner.Delete(this);
        }

        public void Up()
        {
            if (false == canGetOwner())
                return;

            Owner.Up(this);
        }

        public void Down()
        {
            if (false == canGetOwner())
                return;

            Owner.Down(this);
        }

        public void AddFirstCmd()
        {
            Command child = Popup.AddCmd.Instance.Show();
            if (null == child)
                return;

            Insert(0, child);
        }
        
        #endregion //Public Method

        #region Public Method : IOwner

        public void Insert(int index, Command cmd)
        {
            if (index < 0)
            {
                string errMsg = string.Format(Properties.Resources.ErrInsertCmdUnderMin, index, 0);
                Log.Error(errMsg);
                return;
            }

            if (index > CmdList.Count)
            {
                string errMsg = string.Format(Properties.Resources.ErrInsertCmdOverMax, index, CmdList.Count);
                Log.Error(errMsg);
                return;
            }

            if (null == cmd)
            {
                Log.Error(Properties.Resources.ErrInsertCmdNullCmd);
                return;
            }

            CmdList.Insert(index, cmd);
            cmd.Owner = this;

            insert(index, cmd.Model);
        }

        public void Delete(Command cmd)
        {
            if (null != cmd)
                cmd.Owner = null;

            CmdList.Remove(cmd);    // List의 모든 element가 같은 object를 참조하는 경우는 없다고 가정.

            delete(cmd.Model);
        }

        public void Up(Command cmd)
        {
            int cmdIndex;
            if (false == canUp(cmd, out cmdIndex))
                return;

            int prevIndex = cmdIndex - 1;

            CmdList.Move(cmdIndex, prevIndex);

            up(cmdIndex, prevIndex);
        }

        public void Down(Command cmd)
        {
            int cmdIndex;
            if (false == canDown(cmd, out cmdIndex))
                return;

            int nextIndex = cmdIndex + 1;

            CmdList.Move(cmdIndex, nextIndex);

            down(cmdIndex, nextIndex);
        }

        public bool CanUp(Command cmd)
        {
            int index;
            return canUp(cmd, out index);
        }

        public bool CanDown(Command cmd)
        {
            int index;
            return canDown(cmd, out index);
        }

        public int GetIndexOf(Command cmd)
        {
            return CmdList.IndexOf(cmd);    // if not found, return -1.
        }

        #endregion //Public Method : IOwner



        #region Private Method

        private void addSampleCmd()
        {
            Sugarism.CmdBackground bgModel = new Sugarism.CmdBackground(Sugarism.CmdBackground.START_ID);
            CmdBackground cmdBackground = new CmdBackground(bgModel);
            Insert(0, cmdBackground);

            Sugarism.CmdSwitch switchModel = new Sugarism.CmdSwitch(ScenarioEditor.Model.Character.START_ID);
            CmdSwitch cmdSwitch = new CmdSwitch(switchModel);
            Insert(1, cmdSwitch);

            Sugarism.CmdLines linesModel = new Sugarism.CmdLines(ScenarioEditor.Model.Character.START_ID);
            CmdLines cmdlines = new CmdLines(linesModel);
            Insert(2, cmdlines);
        }

        private void insert(int index, Sugarism.Command cmd)
        {
            _model.CmdList.Insert(index, cmd);
        }

        private void delete(Sugarism.Command cmd)
        {
            _model.CmdList.Remove(cmd);
        }

        private void up(int oldIndex, int newIndex)
        {
            Sugarism.Command cmd = _model.CmdList[oldIndex];
            insert(newIndex, cmd);
            _model.CmdList.RemoveAt(oldIndex + 1);
        }

        private void down(int oldIndex, int newIndex)
        {
            Sugarism.Command cmd = _model.CmdList[oldIndex];
            _model.CmdList.RemoveAt(oldIndex);
            insert(newIndex, cmd);
        }

        private bool canUp(Command cmd, out int cmdIndex)
        {
            cmdIndex = GetIndexOf(cmd);
            if (cmdIndex <= 0)
                return false;
            else
                return true;
        }

        private bool canDown(Command cmd, out int cmdIndex)
        {
            cmdIndex = GetIndexOf(cmd);
            if (cmdIndex < 0)
                return false;

            int lastIndex = CmdList.Count - 1;
            if (cmdIndex.Equals(lastIndex))
                return false;

            return true;
        }

        private bool canGetOwner()
        {
            if (null != Owner)
            {
                return true;
            }
            else
            {
                Log.Error(Properties.Resources.ErrNotFoundScenario);
                return false;
            }
        }

        #endregion //Private Method
    }
}
