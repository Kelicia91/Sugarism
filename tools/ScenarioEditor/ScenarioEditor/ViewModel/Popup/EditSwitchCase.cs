using System.Windows.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace ScenarioEditor.ViewModel.Popup
{
    public class EditSwitchCase : ModelBase
    {
        #region Singleton

        private static EditSwitchCase _instance = null;
        public static EditSwitchCase Instance
        {
            get
            {
                if (null == _instance)
                {
                    _instance = new EditSwitchCase();
                }

                return _instance;
            }
        }

        private EditSwitchCase()
        {
            _caseList = new ObservableCollection<EditCase>();
        }

        #endregion //Singleton



        #region Property

        public List<Sugarism.Character> CharacterList
        {
            get { return Common.Instance.CharacterList; }
        }

        private Sugarism.Character _selectedItem;
        public Sugarism.Character SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged(); }
        }

        private ObservableCollection<EditCase> _caseList;
        public ObservableCollection<EditCase> CaseList
        {
            get { return _caseList; }
        }

        public string GuideNumOfCase
        {
            get { return string.Format(Properties.Resources.GuideNumOfCase, Sugarism.CmdSwitch.MIN_COUNT_CASE, Sugarism.CmdSwitch.MAX_COUNT_CASE); }
        }

        public string GuideHowToInputCaseDescription
        {
            get { return string.Format(Properties.Resources.GuideHowToInputDescription, Sugarism.CmdCase.MAX_LENGTH_DESCRIPTION); }
        }

        #endregion //Property


        #region ICommand

        private ICommand _cmdAddChild;
        public ICommand CmdAddChild
        {
            get
            {
                if (null == _cmdAddChild)
                {
                    _cmdAddChild = new RelayCommand(param => AddCase());
                }

                return _cmdAddChild;
            }
        }

        #endregion //ICommand



        #region Public Method

        /// <summary>
        /// Show View.Popup.EditSwitchCase.
        /// </summary>
        /// <param name="caseList">CaseList before editing.</param>
        /// <returns>Whether edit or not.</returns>
        public bool Show(int characterId, IList<Sugarism.CmdCase> caseList)
        {
            reset(characterId, caseList);

            View.Popup.EditSwitchCase view = new View.Popup.EditSwitchCase(this);

            bool? result = view.ShowDialog();
            switch(result)
            {
                case true:
                    return true;

                default:
                    return false;
            }
        }

        public void AddCase()
        {
            int key = CaseList.Count;
            
            EditCase cmdCase = new EditCase(key, string.Empty);

            int index = key;
            Insert(index, cmdCase);
        }

        public void Insert(int index, EditCase editCase)
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

            if (null == editCase)
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

            CaseList.Insert(index, editCase);
            editCase.Owner = this;
        }

        public void Delete(EditCase editCase)
        {
            if (CaseList.Count <= Sugarism.CmdSwitch.MIN_COUNT_CASE)
            {
                string msg = string.Format(Properties.Resources.ErrDeleteCaseUnderMin, Sugarism.CmdSwitch.MIN_COUNT_CASE);
                Log.Error(msg);
                return;
            }

            if (null != editCase)
                editCase.Owner = null;

            CaseList.Remove(editCase);    // List의 모든 element가 같은 object를 참조하는 경우는 없다고 가정.
        }

        #endregion //Public Method



        #region Private Method

        private void reset(int characterId, IList<Sugarism.CmdCase> caseList)
        {
            // characterId
            if (Common.Instance.IsValid(characterId))
            {
                SelectedItem = CharacterList[characterId];
            }
            else
            {
                if (CharacterList.Count > 0)
                    SelectedItem = CharacterList[0];
                else
                    SelectedItem = null;
            }

            // caseList
            CaseList.Clear();
            for(int i = 0; i < caseList.Count; ++i)
            {
                EditCase editCase = new EditCase(caseList[i].Key, caseList[i].Description);

                Insert(CaseList.Count, editCase);
            }
        }

        #endregion //Private Method
    }
}
