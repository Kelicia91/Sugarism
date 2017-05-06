using System.Collections.Generic;


namespace ScenarioEditor.ViewModel.Popup
{
    public class EditLines : ModelBase
    {
        #region Singleton

        private static EditLines _instance = null;
        public static EditLines Instance
        {
            get
            {
                if (null == _instance)
                    _instance = new EditLines();

                return _instance;
            }
        }

        private EditLines()
        {
            _lines = string.Empty;
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

        private string _lines;
        public string Lines
        {
            get { return _lines; }
            set { _lines = value; OnPropertyChanged(); }
        }

        public string GuideHowToInputLines
        {
            get { return string.Format(Properties.Resources.GuideHowToInputLines, Sugarism.CmdLines.MAX_LENGTH_LINE, Sugarism.CmdLines.MAX_COUNT_LINE_END); }
        }

        #endregion //Property



        #region Public Method

        /// <summary>
        /// Show View.Popup.EditLines.
        /// </summary>
        /// <param name="characterId">Character Id before editing.</param>
        /// <param name="lines">Lines before editing.</param>
        /// <returns>Whether edit or not.</returns>
        public bool Show(int characterId, string lines)
        {
            reset(characterId, lines);

            View.Popup.EditLines view = new View.Popup.EditLines(this);

            bool? result = view.ShowDialog();
            switch (result)
            {
                case true:
                    return true;

                default:
                    return false;
            }
        }

        #endregion //Public Method



        #region Private Method

        private void reset(int characterId, string lines)
        {
            if (Common.Instance.IsValid(characterId))
            {
                SelectedItem = CharacterList[characterId-1];
            }
            else
            {
                if (CharacterList.Count > 0)
                    SelectedItem = CharacterList[0];
                else
                    SelectedItem = null;
            }
            
            Lines = lines;
        }

        #endregion //Private Method
    }
}
