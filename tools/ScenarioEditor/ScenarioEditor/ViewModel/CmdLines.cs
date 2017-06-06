
namespace ScenarioEditor.ViewModel
{
    public sealed class CmdLines : Command
    {
        public CmdLines(Sugarism.CmdLines model) : base(model)
        {
            _model = model;

            Common.Instance.Attach(onCharacterListChanged);
        }

        ~CmdLines()
        {
            Common.Instance.Detach(onCharacterListChanged);
        }


        #region Field

        private Sugarism.CmdLines _model;

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

        public string Lines
        {
            get
            {
                if (false == string.IsNullOrEmpty(_model.Lines))
                    return _model.Lines;
                else
                    return Properties.Resources.GuideLines;
            }
            set
            {
                _model.Lines = value;
                OnPropertyChanged();

                OnPropertyChanged("ToText");
            }
        }

        public bool IsAnonymous
        {
            get { return _model.IsAnonymous; }
            set
            {
                _model.IsAnonymous = value;
                OnPropertyChanged();

                OnPropertyChanged("ToText");
            }
        }

        public Sugarism.ELinesEffect LinesEffect
        {
            get { return _model.LinesEffect; }
            set
            {
                _model.LinesEffect = value;
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
            bool isEdited = Popup.EditLines.Instance.Show(CharacterId, Lines, IsAnonymous, LinesEffect, Face, Costume, Position);
            if (false == isEdited)
                return;

            if (null != Popup.EditLines.Instance.SelectedItem)
                CharacterId = Popup.EditLines.Instance.SelectedItem.Id;
            else
                Log.Error(Properties.Resources.ErrNotFoundCharacter);
            
            Lines = Popup.EditLines.Instance.Lines;

            IsAnonymous = Popup.EditLines.Instance.IsAnonymous;
            Face = Popup.EditLines.Instance.Face;
            Costume = Popup.EditLines.Instance.Costume;
            Position = Popup.EditLines.Instance.Position;
            LinesEffect = Popup.EditLines.Instance.LinesEffect;
        }

        public override string ToString()
        {
            string oneLine = convertToOneLine(Lines);
            string content = string.Format("{0}:{1}({2}), {3}/{4}/{5}, {6}:\"{7}\"", 
                CharacterId, RefCharacterName, IsAnonymous, 
                Face, Costume, Position, 
                LinesEffect, oneLine);

            return ToString(content);
        }

        #endregion //Public Method



        #region Private Method

        // callback handler
        private void onCharacterListChanged()
        {
            OnPropertyChanged("ToText");
        }

        private string convertToOneLine(string lines)
        {
            // @note : A string containing "\r\n" for non-Unix platforms, 
            //      or a string containing "\n" for Unix platforms
            return lines.Replace(System.Environment.NewLine, "\\n");
        }

        #endregion //Private Method
    }
}
