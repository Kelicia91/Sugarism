using System;
using System.Collections.Generic;


namespace ScenarioEditor.ViewModel.Popup
{
    public class EditAppear : ModelBase
    {
        #region Singleton

        private static EditAppear _instance = null;
        public static EditAppear Instance
        {
            get
            {
                if (null == _instance)
                    _instance = new EditAppear();

                return _instance;
            }
        }

        private EditAppear()
        {
            _faceList = new List<Sugarism.EFace>();
            Array arr = Enum.GetValues(typeof(Sugarism.EFace));
            foreach (var v in arr)
            {
                Sugarism.EFace item = (Sugarism.EFace)v;
                _faceList.Add(item);
            }

            _costumeList = new List<Sugarism.ECostume>();
            arr = Enum.GetValues(typeof(Sugarism.ECostume));
            foreach (var v in arr)
            {
                Sugarism.ECostume item = (Sugarism.ECostume)v;
                _costumeList.Add(item);
            }

            _posList = new List<Sugarism.EPosition>();
            arr = Enum.GetValues(typeof(Sugarism.EPosition));
            foreach (var v in arr)
            {
                Sugarism.EPosition item = (Sugarism.EPosition)v;
                _posList.Add(item);
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

        #endregion //Property



        #region Public Method

        /// <summary>
        /// Show View.Popup.EditAppear.
        /// </summary>
        /// <param name="characterId">Character Id before editing.</param>
        /// <param name="face">Face before editing.</param>
        /// <param name="costume">Costume before editing.</param>
        /// <param name="pos">Position before editing.</param>
        /// <returns>Whether edit or not.</returns>
        public bool Show(int characterId, Sugarism.EFace face, Sugarism.ECostume costume, Sugarism.EPosition pos)
        {
            reset(characterId, face, costume, pos);

            View.Popup.EditAppear view = new View.Popup.EditAppear(this);

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

        private void reset(int characterId, Sugarism.EFace face, Sugarism.ECostume costume, Sugarism.EPosition pos)
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
            
            Face = face;
            Costume = costume;
            Position = pos;
        }

        #endregion //Private Method
    }
}
