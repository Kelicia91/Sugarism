using System;
using System.Collections.Generic;


namespace ScenarioEditor.ViewModel.Popup
{
    public class EditFeeling : ModelBase
    {
        #region Singleton

        private static EditFeeling _instance = null;
        public static EditFeeling Instance
        {
            get
            {
                if (null == _instance)
                    _instance = new EditFeeling();

                return _instance;
            }
        }

        private EditFeeling()
        {
            _opList = new List<Sugarism.EOperation>();
            Array arr = Enum.GetValues(typeof(Sugarism.EOperation));
            foreach (var v in arr)
            {
                Sugarism.EOperation item = (Sugarism.EOperation)v;
                _opList.Add(item);
            }
        }

        #endregion


        #region Property

        public Model.Character[] CharacterList
        {
            get { return Common.Instance.CharacterList; }
        }

        private Model.Character _character;
        public Model.Character Character
        {
            get { return _character; }
            set { _character = value; OnPropertyChanged(); }
        }

        private List<Sugarism.EOperation> _opList;
        public List<Sugarism.EOperation> OpList
        {
            get { return _opList; }
        }

        private Sugarism.EOperation _op;
        public Sugarism.EOperation Op
        {
            get { return _op; }
            set { _op = value; OnPropertyChanged(); }
        }

        private int _value;
        public int Value
        {
            get { return _value; }
            set { _value = value; OnPropertyChanged(); }
        }

        #endregion


        #region Public Method

        /// <summary>
        /// Show View.Popup.EditFeeling.
        /// </summary>
        /// <param name="characterId">Character Id before editing.</param>
        /// <param name="op">Operation before editing.</param>
        /// <param name="value">Value before editing.</param>
        /// <returns>Whether edit or not.</returns>
        public bool Show(int characterId, Sugarism.EOperation op, int value)
        {
            reset(characterId, op, value);

            View.Popup.EditFeeling view = new View.Popup.EditFeeling(this);

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

        private void reset(int characterId, Sugarism.EOperation op, int value)
        {
            if (Common.Instance.IsValidCharacter(characterId))
            {
                Character = CharacterList[characterId];
            }
            else
            {
                if (null == CharacterList)
                    Character = null;
                else if (CharacterList.Length > 0)
                    Character = CharacterList[0];
                else
                    Character = null;
            }

            Op = op;
            Value = value;
        }

        #endregion //Private Method
    }
}
