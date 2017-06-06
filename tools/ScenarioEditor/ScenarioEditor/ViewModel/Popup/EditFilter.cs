using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScenarioEditor.ViewModel.Popup
{
    public class EditFilter : ModelBase
    {
        #region Singleton

        private static EditFilter _instance = null;
        public static EditFilter Instance
        {
            get
            {
                if (null == _instance)
                    _instance = new EditFilter();

                return _instance;
            }
        }

        public EditFilter()
        {
            _filterList = new List<Sugarism.EFilter>();
            Array arr = Enum.GetValues(typeof(Sugarism.EFilter));
            foreach (var v in arr)
            {
                Sugarism.EFilter item = (Sugarism.EFilter)v;
                _filterList.Add(item);
            }
        }

        #endregion //Singleton


        #region Property

        private List<Sugarism.EFilter> _filterList;
        public List<Sugarism.EFilter> FilterList
        {
            get { return _filterList; }
        }

        private Sugarism.EFilter _filter;
        public Sugarism.EFilter Filter
        {
            get { return _filter; }
            set { _filter = value; OnPropertyChanged(); }
        }

        #endregion


        #region Public Method

        /// <summary>
        /// Show View.Popup.EditFilter.
        /// </summary>
        /// <param name="filter">Filter before editing.</param>
        /// <returns>Whether edit or not.</returns>
        public bool Show(Sugarism.EFilter filter)
        {
            reset(filter);

            View.Popup.EditFilter view = new View.Popup.EditFilter(this);

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

        private void reset(Sugarism.EFilter filter)
        {
            Filter = filter;
        }

        #endregion //Private Method
    }
}
