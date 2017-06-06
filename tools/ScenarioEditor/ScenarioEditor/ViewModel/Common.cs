using System.Collections.Generic;


namespace ScenarioEditor.ViewModel
{
    /// <summary>
    /// Common ViewModel refered by some ViewModel.
    /// it manages to save Properties.Settings and raises CharacterListChangeEvent.
    /// </summary>
    public sealed class Common : ModelBase
    {
        #region Singleton
        
        private static readonly Common _instance = new Common();

        private Common()
        {
            _characterlist = new List<Sugarism.Character>();
            _characterListChangeEvent = new CharacterListChangeEventHandler(onCharacterListChanged);

            if (FileUtils.Exists(Properties.Settings.Default.CharacterFilePath))
                Import(Properties.Settings.Default.CharacterFilePath);
            else
                Properties.Settings.Default.CharacterFilePath = Properties.Resources.GuideFindPath;
        }

        ~Common()
        {
            Properties.Settings.Default.Save();
        }

        public static Common Instance
        {
            get { return _instance; }
        }

        #endregion //Singleton



        #region Event

        public delegate void CharacterListChangeEventHandler();
        private event CharacterListChangeEventHandler _characterListChangeEvent;

        #endregion  //Event



        #region Property

        private List<Sugarism.Character> _characterlist;
        public List<Sugarism.Character> CharacterList
        {
            get { return _characterlist; }
            private set
            {
                _characterlist = value;
                OnPropertyChanged();

                if (null != _characterListChangeEvent)
                    _characterListChangeEvent.Invoke();  /** Raise Event **/
            }
        }

        #endregion //Property



        #region Public Methods

        public void Import(string characterFilePath)
        {
            if (string.IsNullOrEmpty(characterFilePath))
                return;

            string text = string.Empty;

            // read the file as text
            bool isRead = FileUtils.ReadAllTextAsUTF8(characterFilePath, out text);
            if (false == isRead)
                return;

            // deserialize the text to object
            object result = null;
            bool isDeserialized = JsonUtils.Deserialize<List<Sugarism.Character>>(text, out result, null);
            if (false == isDeserialized)
                return;

            //
            Properties.Settings.Default.CharacterFilePath = characterFilePath;
            CharacterList.Clear();
            CharacterList = result as List<Sugarism.Character>;
        }

        public bool IsValid(int characterId)
        {
            if (characterId < 0)
                return false;

            // id(index) range : 0 ~ (CharacterList.Count - 1)
            if (characterId >= CharacterList.Count)
                return false;

            return true;
        }

        public void Attach(CharacterListChangeEventHandler handler)
        {
            if (null == handler)
                return;

            _characterListChangeEvent += handler;
        }

        public void Detach(CharacterListChangeEventHandler handler)
        {
            if (null == handler)
                return;

            _characterListChangeEvent -= handler;
        }

        #endregion //Public Methods



        #region Private Methods

        // callback handler
        private void onCharacterListChanged()
        {
            // do nothing
        }

        #endregion //Private Methods
    }
}
