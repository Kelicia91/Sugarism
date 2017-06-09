using System;
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

            _linesEffectList = new List<Sugarism.ELinesEffect>();
            Array arr = Enum.GetValues(typeof(Sugarism.ELinesEffect));
            foreach(var v in arr)
            {
                Sugarism.ELinesEffect item = (Sugarism.ELinesEffect)v;
                _linesEffectList.Add(item);
            }
        }

        #endregion //Singleton



        #region Property

        public Model.Character[] CharacterList
        {
            get { return Common.Instance.CharacterList; }
        }

        private Model.Character _selectedItem;
        public Model.Character SelectedItem
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

        private bool _isAnonymous;
        public bool IsAnonymous
        {
            get { return _isAnonymous; }
            set { _isAnonymous = value; OnPropertyChanged(); }
        }

        private List<Sugarism.ELinesEffect> _linesEffectList;
        public List<Sugarism.ELinesEffect> LinesEffectList
        {
            get { return _linesEffectList; }
        }

        private Sugarism.ELinesEffect _linesEffect;
        public Sugarism.ELinesEffect LinesEffect
        {
            get { return _linesEffect; }
            set { _linesEffect = value; OnPropertyChanged(); }
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
        public bool Show(int characterId, bool isAnonymous, 
                        string lines, Sugarism.ELinesEffect linesEffect)
        {
            reset(characterId, isAnonymous, lines, linesEffect);

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

        private void reset(int characterId, bool isAnonymous, 
                        string lines, Sugarism.ELinesEffect linesEffect)
        {
            if (Common.Instance.IsValidCharacter(characterId))
            {
                SelectedItem = CharacterList[characterId];
            }
            else
            {
                if (null == CharacterList)
                    SelectedItem = null;
                else if (CharacterList.Length > 0)
                    SelectedItem = CharacterList[0];
                else
                    SelectedItem = null;
            }

            IsAnonymous = isAnonymous;
            Lines = lines;
            LinesEffect = linesEffect;
        }

        #endregion //Private Method
    }
}
