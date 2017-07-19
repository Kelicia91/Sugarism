
namespace ScenarioEditor.ViewModel
{
    public sealed class CmdFeeling : Command
    {
        public CmdFeeling(Sugarism.CmdFeeling model) : base(model)
        {
            _model = model;

            Common.Instance.CharacterListChangeEvent.Attach(onCharacterListChanged);
            Common.Instance.TargetListChangeEvent.Attach(onTargetListChanged);
        }

        ~CmdFeeling()
        {
            Common.Instance.CharacterListChangeEvent.Detach(onCharacterListChanged);
            Common.Instance.TargetListChangeEvent.Detach(onTargetListChanged);
        }


        #region Field

        private Sugarism.CmdFeeling _model;

        #endregion //Field


        #region Property

        public int TargetId
        {
            get { return _model.TargetId; }
            set
            {
                _model.TargetId = value;
                OnPropertyChanged();

                OnPropertyChanged("RefCharacterName");
                OnPropertyChanged("ToText");
            }
        }

        public string RefCharacterName
        {
            get
            {
                if (false == Common.Instance.IsValidTarget(TargetId))
                    return ScenarioEditor.Model.Character.STR_UNKNOWN;

                int characterId = Common.Instance.TargetList[TargetId].CharacterId;

                if (Common.Instance.IsValidCharacter(characterId))
                    return Common.Instance.CharacterList[characterId].Name;
                else
                    return ScenarioEditor.Model.Character.STR_UNKNOWN;
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
            Popup.EditFeeling popup = Popup.EditFeeling.Instance;

            bool isEdited = popup.Show(TargetId, Op, Value);
            if (false == isEdited)
                return;

            if (null != popup.SelectedItem)
                TargetId = popup.SelectedItem.Id;
            else
                Log.Error(Properties.Resources.ErrNotFoundCharacter);

            Op = popup.Op;
            Value = popup.Value;
        }

        public override string ToString()
        {
            string content = string.Format("{0}:{1} {2} {3}",
                TargetId, RefCharacterName, Op, Value);
            return ToString(content);
        }

        #endregion //Public Method


        #region Private Method

        // callback handler
        private void onCharacterListChanged()
        {
            OnPropertyChanged("ToText");
        }

        private void onTargetListChanged()
        {
            OnPropertyChanged("ToText");
        }

        #endregion
    }
}
