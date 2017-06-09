
namespace ScenarioEditor.ViewModel
{
    public sealed class CmdText : Command
    {
        public CmdText(Sugarism.CmdText model) : base(model)
        {
            _model = model;
        }


        #region Field

        private Sugarism.CmdText _model;

        #endregion //Field


        #region Property

        public string Text
        {
            get
            {
                if (false == string.IsNullOrEmpty(_model.Text))
                    return _model.Text;
                else
                    return Properties.Resources.GuideText;
            }
            set
            {
                _model.Text = value;
                OnPropertyChanged();

                OnPropertyChanged("ToText");
            }
        }

        public override string ToText
        {
            get { return ToString(); }
        }

        #endregion //Property



        #region Public Method

        public override void Edit()
        {
            bool isEdited = Popup.EditText.Instance.Show(Text);
            if (false == isEdited)
                return;

            Text = Popup.EditText.Instance.Text;
        }

        public override string ToString()
        {
            string oneLine = convertToOneLine(Text);
            string content = string.Format("{0}", oneLine);
            return ToString(content);
        }

        #endregion //Public Method



        #region Private Method
        
        private string convertToOneLine(string text)
        {
            // @note : A string containing "\r\n" for non-Unix platforms, 
            //      or a string containing "\n" for Unix platforms
            return text.Replace(System.Environment.NewLine, "\\n");
        }

        #endregion //Private Method
    }
}
