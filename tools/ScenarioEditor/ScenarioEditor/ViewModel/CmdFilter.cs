using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScenarioEditor.ViewModel
{
    public sealed class CmdFilter : Command
    {
        public CmdFilter(Sugarism.CmdFilter model) : base(model)
        {
            _model = model;
        }


        #region Field

        private Sugarism.CmdFilter _model;

        #endregion //Field


        #region Property

        public Sugarism.EFilter Filter
        {
            get { return _model.Filter; }
            set
            {
                _model.Filter = value;
                OnPropertyChanged();

                OnPropertyChanged("ToText");
            }
        }

        public override string ToText
        {
            get { return ToString(); }
        }

        #endregion


        #region Public Method

        public override void Edit()
        {
            bool isEdited = Popup.EditFilter.Instance.Show(Filter);
            if (false == isEdited)
                return;

            Filter = Popup.EditFilter.Instance.Filter;
        }

        public override string ToString()
        {
            string content = string.Format("{0}", Filter);
            return ToString(content);
        }

        #endregion //Public Method
    }
}
