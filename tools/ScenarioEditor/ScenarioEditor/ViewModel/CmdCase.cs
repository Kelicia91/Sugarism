using System.Windows.Input;
using System.Collections.ObjectModel;


namespace ScenarioEditor.ViewModel
{
    public sealed class CmdCase : Command, IOwner<Command>
    {
        public CmdCase(Sugarism.CmdCase model) : base(model)
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

            _parent = null;

            InputBindings.Clear();
            InputBindings.Add(new KeyBinding(CmdExpand, System.Windows.Input.Key.Enter, ModifierKeys.None));
            InputBindings.Add(new KeyBinding(CmdAddChild, System.Windows.Input.Key.A, ModifierKeys.Control));
        }


        #region Field

        private Sugarism.CmdCase _model;

        private ObservableCollection<Command> _cmdList;
        private IOwner<CmdCase> _parent;
        
        private ICommand _cmdAddChild;

        #endregion //Field


        #region Property

        public int Key
        {
            get { return _model.Key; }
            private set
            {
                _model.Key = value;
                OnPropertyChanged();

                OnPropertyChanged("CanDelete");
                OnPropertyChanged("ToText");
            }
        }

        public string Description
        {
            get
            {
                if (false == string.IsNullOrEmpty(_model.Description))
                    return _model.Description;
                else
                    return Properties.Resources.GuideCase; 
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

        public override string ToText
        {
            get { return ToString(); }
        }
        
        public override IOwner<Command> Owner { get { return null; } }
        
        public IOwner<CmdCase> Parent
        {
            get { return _parent; }
            set { _parent = value; OnPropertyChanged(); }
        }

        #endregion //Property


        #region ICommand

        public ICommand CmdAddChild
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

        public override void Edit()
        {
            return;
        }

        public override string ToString()
        {
            string content = string.Format("[{0}] {1}", Key, Description);

            return ToString(content);
        }

        public void AddFirstCmd()
        {
            Command cmd = Popup.AddCmd.Instance.Show();
            if (null == cmd)
                return;

            Insert(0, cmd);
        }

        public override void Delete()
        {
            if (false == canGetParent())
                return;

            Parent.Delete(this);
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

        private bool canGetParent()
        {
            if (null != Parent)
            {
                return true;
            }
            else
            {
                Log.Error(Properties.Resources.ErrNotFoundCmdOwner);
                return false;
            }
        }

        #endregion //Private Method
    }
}
