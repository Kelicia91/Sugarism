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
            _posList = new List<Sugarism.EPosition>();
            Array arr = Enum.GetValues(typeof(Sugarism.EPosition));
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
        /// <param name="pos">Position before editing.</param>
        /// <returns>Whether edit or not.</returns>
        public bool Show(int characterId, Sugarism.EPosition pos)
        {
            reset(characterId, pos);

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

        private void reset(int characterId, Sugarism.EPosition pos)
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
            
            Position = pos;
        }

        #endregion //Private Method
    }
}
