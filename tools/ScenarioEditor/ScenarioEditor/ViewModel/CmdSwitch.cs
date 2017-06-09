using System.Windows.Input;
using System.Collections.ObjectModel;


namespace ScenarioEditor.ViewModel
{
    public sealed class CmdSwitch : Command, IOwner<CmdCase>
    {
        public CmdSwitch(Sugarism.CmdSwitch model) : base(model)
        {
            _model = model;

            _caseList = new ObservableCollection<CmdCase>();
            foreach(Sugarism.CmdCase cmdCase in _model.CaseList)
            {
                CmdCase cmdCaseVm = new CmdCase(cmdCase);
                CaseList.Add(cmdCaseVm);
                cmdCaseVm.Parent = this;
            }

            IsExpanded = true;

            InputBindings.Add(new KeyBinding(CmdExpand, Key.Enter, ModifierKeys.None));

            Common.Instance.CharacterListChangeEvent.Attach(onCharacterListChanged);
        }

        ~CmdSwitch()
        {
            Common.Instance.CharacterListChangeEvent.Detach(onCharacterListChanged);
        }


        #region Field

        private Sugarism.CmdSwitch _model;
        
        private ObservableCollection<CmdCase> _caseList;

        #endregion Field


        #region Property

        public int CharacterId
        {
            get { return _model.CharacterId; }
            set
            {
                _model.CharacterId = value;
                OnPropertyChanged();

                OnPropertyChanged("RefCharacterName");
                OnPropertyChanged("ToText");
            }
        }
        
        public string RefCharacterName
        {
            get
            {
                if (Common.Instance.IsValidCharacter(CharacterId))
                    return Common.Instance.CharacterList[CharacterId].Name;
                else
                    return ScenarioEditor.Model.Character.STR_UNKNOWN;
            }
        }
        
        public override string ToText
        {
            get { return ToString(); }
        }

        public ObservableCollection<CmdCase> CaseList
        {
            get { return _caseList; }
        }

        #endregion //Property


        #region ICommand

        public ICommand CmdAddChild // IOwner
        {
            get { return null; }
        }

        #endregion //ICommand



        #region Public Method

        public override void Edit()
        {
            bool isEdited = Popup.EditSwitchCase.Instance.Show(CharacterId, _model.CaseList);
            if (false == isEdited)
                return;

            if (null != Popup.EditSwitchCase.Instance.SelectedItem)
                CharacterId = Popup.EditSwitchCase.Instance.SelectedItem.Id;
            else
                Log.Error(Properties.Resources.ErrNotFoundCharacter);

            // Update : CaseList.Count, CaseList[i].Description
            if (CaseList.Count <= Popup.EditSwitchCase.Instance.CaseList.Count)
            {
                // add case
                for (int i = 0; i < Popup.EditSwitchCase.Instance.CaseList.Count; ++i)
                {
                    if (i >= CaseList.Count)
                        AddCmdCase();

                    CaseList[i].Description = Popup.EditSwitchCase.Instance.CaseList[i].Description;
                }
            }
            else
            {
                // delete case
                for (int i = 0; i < CaseList.Count; ++i)
                {
                    if (i >= Popup.EditSwitchCase.Instance.CaseList.Count)
                    {
                        Delete(CaseList[i]);
                        continue;
                    }

                    CaseList[i].Description = Popup.EditSwitchCase.Instance.CaseList[i].Description;
                }
            }
        }

        public void AddCmdCase()
        {
            int key = CaseList.Count;

            Sugarism.CmdCase model = new Sugarism.CmdCase(key);

            CmdCase cmdCase = new CmdCase(model);

            int index = key;
            Insert(index, cmdCase);
        }

        public override string ToString()
        {
            string numOfCase = string.Format("{0} cases", CaseList.Count);
            string content = string.Format("{0}:{1} => {2}", CharacterId, RefCharacterName, numOfCase);

            return ToString(content);
        }

        #endregion //Public Method

        #region Public Method : IOwner

        public void Insert(int index, CmdCase cmdCase)
        {
            if (index < 0)
            {
                string errMsg = string.Format(Properties.Resources.ErrInsertCmdUnderMin, index, 0);
                Log.Error(errMsg);
                return;
            }

            if (index > CaseList.Count)
            {
                string errMsg = string.Format(Properties.Resources.ErrInsertCmdOverMax, index, CaseList.Count);
                Log.Error(errMsg);
                return;
            }

            if (null == cmdCase)
            {
                Log.Error(Properties.Resources.ErrInsertCmdNullCmd);
                return;
            }
            
            if (CaseList.Count >= Sugarism.CmdSwitch.MAX_COUNT_CASE)
            {
                string msg = string.Format(Properties.Resources.ErrInsertCaseOverMax, Sugarism.CmdSwitch.MAX_COUNT_CASE);
                Log.Error(msg);
                return;
            }

            CaseList.Insert(index, cmdCase);
            cmdCase.Parent = this;

            insert(index, cmdCase.Model as Sugarism.CmdCase);

            OnPropertyChanged("ToText");
        }

        public void Delete(CmdCase cmdCase)
        {
            if (CaseList.Count <= Sugarism.CmdSwitch.MIN_COUNT_CASE)
            {
                string msg = string.Format(Properties.Resources.ErrDeleteCaseUnderMin, Sugarism.CmdSwitch.MIN_COUNT_CASE);
                Log.Error(msg);
                return;
            }

            if (null != cmdCase)
                cmdCase.Parent = null;

            CaseList.Remove(cmdCase);    // List의 모든 element가 같은 object를 참조하는 경우는 없다고 가정.

            delete(cmdCase.Model as Sugarism.CmdCase);

            OnPropertyChanged("ToText");
        }

        public void Up(CmdCase cmdCase) { return; }
        public void Down(CmdCase cmdCase) { return; }
        public bool CanUp(CmdCase cmdCase) { return false; }
        public bool CanDown(CmdCase cmdCase) { return false; }
        public int GetIndexOf(CmdCase cmdCase) { return -1; }

        #endregion //Public Method : IOwner



        #region Private Method

        private void insert(int index, Sugarism.CmdCase cmdCase)
        {
            _model.CaseList.Insert(index, cmdCase);
        }

        private void delete(Sugarism.CmdCase cmdCase)
        {
            _model.CaseList.Remove(cmdCase);
        }

        // callback handler
        private void onCharacterListChanged()
        {
            OnPropertyChanged("ToText");
        }

        #endregion //Private Method
    }
}
