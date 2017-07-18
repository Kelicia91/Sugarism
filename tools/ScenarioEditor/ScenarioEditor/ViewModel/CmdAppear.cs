
namespace ScenarioEditor.ViewModel
{
    public sealed class CmdAppear : Command
    {
        public CmdAppear(Sugarism.CmdAppear model) : base(model)
        {
            _model = model;

            Common.Instance.CharacterListChangeEvent.Attach(onCharacterListChanged);
        }

        ~CmdAppear()
        {
            Common.Instance.CharacterListChangeEvent.Detach(onCharacterListChanged);
        }


        #region Field

        private Sugarism.CmdAppear _model = null;

        #endregion //Field


        #region Property

        public int CharacterId
        {
            get { return _model.CharacterId; }
            set
            {
                _model.CharacterId = value;
                OnPropertyChanged();

                OnPropertyChanged("RefCharacterName");
                OnPropertyChanged("ToText");
            }
        }

        public string RefCharacterName
        {
            get
            {
                if (Common.Instance.IsValidCharacter(CharacterId))
                    return Common.Instance.CharacterList[CharacterId].Name;
                else
                    return ScenarioEditor.Model.Character.STR_UNKNOWN;
            }
        }

        public Sugarism.EPosition Position
        {
            get { return _model.Position; }
            set
            {
                _model.Position = value;
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
            bool isEdited = Popup.EditAppear.Instance.Show(CharacterId, Position);
            if (false == isEdited)
                return;

            if (null != Popup.EditAppear.Instance.SelectedItem)
                CharacterId = Popup.EditAppear.Instance.SelectedItem.Id;
            else
                Log.Error(Properties.Resources.ErrNotFoundCharacter);
            
            Position = Popup.EditAppear.Instance.Position;
        }

        public override string ToString()
        {
            string content = string.Format("{0}:{1} - {2}",
                            CharacterId, RefCharacterName, Position);

            return ToString(content);
        }

        #endregion //Public Method



        #region Private Method

        // callback handler
        private void onCharacterListChanged()
        {
            OnPropertyChanged("ToText");
        }

        #endregion //Private Method
    }
}
