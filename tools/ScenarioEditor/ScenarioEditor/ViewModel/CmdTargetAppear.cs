
namespace ScenarioEditor.ViewModel
{
    public sealed class CmdTargetAppear : Command
    {
        public CmdTargetAppear(Sugarism.CmdTargetAppear model) : base(model)
        {
            _model = model;

            Common.Instance.CharacterListChangeEvent.Attach(onCharacterListChanged);
            Common.Instance.TargetListChangeEvent.Attach(onTargetListChanged);
        }

        ~CmdTargetAppear()
        {
            Common.Instance.CharacterListChangeEvent.Detach(onCharacterListChanged);
            Common.Instance.TargetListChangeEvent.Detach(onTargetListChanged);
        }


        #region Field

        private Sugarism.CmdTargetAppear _model = null;

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

        public bool IsBlush
        {
            get { return _model.IsBlush; }
            set
            {
                _model.IsBlush = value;
                OnPropertyChanged();

                OnPropertyChanged("ToText");
            }
        }

        public Sugarism.EFace Face
        {
            get { return _model.Face; }
            set
            {
                _model.Face = value;
                OnPropertyChanged();

                OnPropertyChanged("ToText");
            }
        }

        public Sugarism.ECostume Costume
        {
            get { return _model.Costume; }
            set
            {
                _model.Costume = value;
                OnPropertyChanged();

                OnPropertyChanged("ToText");
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
            Popup.EditTargetAppear popup = Popup.EditTargetAppear.Instance;

            bool isEdited = popup.Show(TargetId, IsBlush, Face, Costume, Position);
            if (false == isEdited)
                return;

            if (null != popup.SelectedItem)
                TargetId = popup.SelectedItem.Id;
            else
                Log.Error(Properties.Resources.ErrNotFoundTarget);

            IsBlush = popup.IsBlush;
            Face = popup.Face;
            Costume = popup.Costume;
            Position = popup.Position;
        }

        public override string ToString()
        {
            string content = string.Format("{0}:{1} - {2}({3}) / {4} / {5}",
                            TargetId, RefCharacterName,
                            Face, IsBlush, Costume, Position);

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

        #endregion //Private Method
    }
}
