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

            _faceList = new List<Sugarism.EFace>();
            arr = Enum.GetValues(typeof(Sugarism.EFace));
            foreach(var v in arr)
            {
                Sugarism.EFace item = (Sugarism.EFace)v;
                _faceList.Add(item);
            }

            _costumeList = new List<Sugarism.ECostume>();
            arr = Enum.GetValues(typeof(Sugarism.ECostume));
            foreach(var v in arr)
            {
                Sugarism.ECostume item = (Sugarism.ECostume)v;
                _costumeList.Add(item);
            }

            _posList = new List<Sugarism.EPosition>();
            arr = Enum.GetValues(typeof(Sugarism.EPosition));
            foreach(var v in arr)
            {
                Sugarism.EPosition item = (Sugarism.EPosition)v;
                _posList.Add(item);
            }
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

        private List<Sugarism.EFace> _faceList;
        public List<Sugarism.EFace> FaceList
        {
            get { return _faceList; }
        }

        private Sugarism.EFace _face;
        public Sugarism.EFace Face
        {
            get { return _face; }
            set { _face = value; OnPropertyChanged(); }
        }

        private List<Sugarism.ECostume> _costumeList;
        public List<Sugarism.ECostume> CostumeList
        {
            get { return _costumeList; }
        }

        private Sugarism.ECostume _costume;
        public Sugarism.ECostume Costume
        {
            get { return _costume; }
            set { _costume = value; OnPropertyChanged(); }
        }

        private List<Sugarism.EPosition> _posList;
        public List<Sugarism.EPosition> PosList
        {
            get { return _posList; }
        }

        private Sugarism.EPosition _position;
        public Sugarism.EPosition Position
        {
            get { return _position; }
            set { _position = value; OnPropertyChanged(); }
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
        public bool Show(int characterId, string lines, 
            bool isAnonymous, Sugarism.ELinesEffect linesEffect, 
            Sugarism.EFace face, Sugarism.ECostume costume, Sugarism.EPosition pos)
        {
            reset(characterId, lines, isAnonymous, linesEffect, face, costume, pos);

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

        private void reset(int characterId, string lines,
            bool isAnonymous, Sugarism.ELinesEffect linesEffect,
            Sugarism.EFace face, Sugarism.ECostume costume, Sugarism.EPosition pos)
        {
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
            
            Lines = lines;

            IsAnonymous = isAnonymous;
            Face = face;
            LinesEffect = linesEffect;
            Costume = costume;
            Position = pos;
        }

        #endregion //Private Method
    }
}
