
namespace ScenarioEditor.ViewModel
{
    public sealed class CmdFeeling : Command
    {
        public CmdFeeling(Sugarism.CmdFeeling model) : base(model)
        {
            _model = model;

            Common.Instance.Attach(onCharacterListChanged);
        }

        ~CmdFeeling()
        {
            Common.Instance.Detach(onCharacterListChanged);
        }


        #region Field

        private Sugarism.CmdFeeling _model;

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
                if (Common.Instance.IsValid(CharacterId))
                    return Common.Instance.CharacterList[CharacterId].Name;
                else
                    return Sugarism.Character.STR_UNKNOWN;
            }
        }

        public Sugarism.EOperation Op
        {
            get { return _model.Op; }
            set
            {
                _model.Op = value;
                OnPropertyChanged();

                OnPropertyChanged("ToText");
            }
        }

        public int Value
        {
            get { return _model.Value; }
            set
            {
                _model.Value = value;
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
            bool isEdited = Popup.EditFeeling.Instance.Show(CharacterId, Op, Value);
            if (false == isEdited)
                return;

            if (null != Popup.EditFeeling.Instance.Character)
                CharacterId = Popup.EditFeeling.Instance.Character.Id;
            else
                Log.Error(Properties.Resources.ErrNotFoundCharacter);

            Op = Popup.EditFeeling.Instance.Op;
            Value = Popup.EditFeeling.Instance.Value;
        }

        public override string ToString()
        {
            string content = string.Format("{0}:{1} {2} {3}",
                CharacterId, RefCharacterName, Op, Value);
            return ToString(content);
        }

        #endregion //Public Method


        #region Private Method

        // callback handler
        private void onCharacterListChanged()
        {
            OnPropertyChanged("ToText");
        }

        #endregion
    }
}
