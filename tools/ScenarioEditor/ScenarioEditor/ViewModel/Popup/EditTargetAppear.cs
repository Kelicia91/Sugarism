using System;
using System.Collections.Generic;


namespace ScenarioEditor.ViewModel.Popup
{
    public class EditTargetAppear : ModelBase
    {
        #region Singleton

        private static EditTargetAppear _instance = null;
        public static EditTargetAppear Instance
        {
            get
            {
                if (null == _instance)
                    _instance = new EditTargetAppear();

                return _instance;
            }
        }

        private EditTargetAppear()
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

        #endregion  //Singleton


        #region Property

        public Model.Target[] TargetList
        {
            get { return Common.Instance.TargetList; }
        }

        private Model.Target _selectedItem;
        public Model.Target SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged(); }
        }

        private bool _isBlush;
        public bool IsBlush
        {
            get { return _isBlush; }
            set { _isBlush = value; OnPropertyChanged(); }
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
        /// Show View.Popup.EditTargetAppear.
        /// </summary>
        /// <param name="targetId">Target Id before editing.</param>
        /// <param name="isBlush">IsBlush before editing</param>
        /// <param name="face">Face before editing.</param>
        /// <param name="costume">Costume before editing.</param>
        /// <param name="pos">Position before editing.</param>
        /// <returns>Whether edit or not.</returns>
        public bool Show(int targetId, bool isBlush, Sugarism.EFace face, Sugarism.ECostume costume, Sugarism.EPosition pos)
        {
            reset(targetId, isBlush, face, costume, pos);

            View.Popup.EditTargetAppear view = new View.Popup.EditTargetAppear(this);

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

        private void reset(int targetId, bool isBlush, Sugarism.EFace face, Sugarism.ECostume costume, Sugarism.EPosition pos)
        {
            if (Common.Instance.IsValidTarget(targetId))
            {
                SelectedItem = TargetList[targetId];
            }
            else
            {
                if (null == TargetList)
                    SelectedItem = null;
                else if (TargetList.Length > 0)
                    SelectedItem = TargetList[0];
                else
                    SelectedItem = null;
            }

            IsBlush = isBlush;
            Face = face;
            Costume = costume;
            Position = pos;
        }

        #endregion //Private Method

    }   // class
}
