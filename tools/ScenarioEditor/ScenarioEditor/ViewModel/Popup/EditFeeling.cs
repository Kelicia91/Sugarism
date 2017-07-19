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
        /// <param name="targetId">Target Id before editing.</param>
        /// <param name="op">Operation before editing.</param>
        /// <param name="value">Value before editing.</param>
        /// <returns>Whether edit or not.</returns>
        public bool Show(int targetId, Sugarism.EOperation op, int value)
        {
            reset(targetId, op, value);

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

        private void reset(int targetId, Sugarism.EOperation op, int value)
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

            Op = op;
            Value = value;
        }

        #endregion //Private Method
    }
}
