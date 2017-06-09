
namespace ScenarioEditor.ViewModel.Popup
{
    public class EditText : ModelBase
    {
        #region Singleton

        private static EditText _instance = null;
        public static EditText Instance
        {
            get
            {
                if (null == _instance)
                    _instance = new EditText();

                return _instance;
            }
        }

        private EditText()
        {
            _text = string.Empty;
        }

        #endregion //Singleton



        #region Property

        private string _text;
        public string Text
        {
            get { return _text; }
            set { _text = value; OnPropertyChanged(); }
        }

        public string GuideHowToInputLines
        {
            get { return string.Format(Properties.Resources.GuideHowToInputLines, Sugarism.CmdText.MAX_LENGTH_LINE, Sugarism.CmdText.MAX_COUNT_LINE_END); }
        }

        #endregion //Property



        #region Public Method

        /// <summary>
        /// Show View.Popup.EditText.
        /// </summary>
        /// <param name="text">Text before editing.</param>
        /// <returns>Whether edit or not.</returns>
        public bool Show(string text)
        {
            reset(text);

            View.Popup.EditText view = new View.Popup.EditText(this);

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

        private void reset(string text)
        {
            Text = text;
        }

        #endregion //Private Method
    }
}
