﻿
namespace ScenarioEditor.ViewModel
{
    public sealed class CmdLines : Command
    {
        public CmdLines(Sugarism.CmdLines model) : base(model)
        {
            _model = model;

            Common.Instance.CharacterListChangeEvent.Attach(onCharacterListChanged);
        }

        ~CmdLines()
        {
            Common.Instance.CharacterListChangeEvent.Detach(onCharacterListChanged);
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
                if (Common.Instance.IsValidCharacter(CharacterId))
                    return Common.Instance.CharacterList[CharacterId].Name;
                else
                    return ScenarioEditor.Model.Character.STR_UNKNOWN;
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
        
        public override string ToText
        {
            get { return ToString(); }
        }

        #endregion //Property



        #region Public Method

        public override void Edit()
        {
            bool isEdited = Popup.EditLines.Instance.Show(CharacterId, IsAnonymous, Lines, LinesEffect);
            if (false == isEdited)
                return;

            if (null != Popup.EditLines.Instance.SelectedItem)
                CharacterId = Popup.EditLines.Instance.SelectedItem.Id;
            else
                Log.Error(Properties.Resources.ErrNotFoundCharacter);
            
            Lines = Popup.EditLines.Instance.Lines;

            IsAnonymous = Popup.EditLines.Instance.IsAnonymous;
            LinesEffect = Popup.EditLines.Instance.LinesEffect;
        }

        public override string ToString()
        {
            string oneLine = convertToOneLine(Lines);
            string content = string.Format("{0}:{1}({2}) - {3}:\"{4}\"", 
                CharacterId, RefCharacterName, IsAnonymous, 
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
