
namespace ScenarioEditor.ViewModel
{
    /// <summary>
    /// Common ViewModel refered by some ViewModel.
    /// it manages to save Properties.Settings and raises (GameResource)ListChangeEvent.
    /// </summary>
    public sealed class Common : ModelBase
    {
        #region Singleton
        
        private static Common _instance = null;
        public static Common Instance
        {
            get
            {
                if (null == _instance)
                    _instance = new Common();

                return _instance;
            }
        }

        private Common()
        {
            // Character
            _characterlist = null;
            _characterListChangeEvent = new CharacterListChangeEvent();

            if (FileUtils.Exists(Properties.Settings.Default.CharacterFilePath))
                ImportCharacter(Properties.Settings.Default.CharacterFilePath);
            else
                Properties.Settings.Default.CharacterFilePath = Properties.Resources.GuideFindPath;
                
            // Background
            _backgroundList = null;
            _backgroundListChangeEvent = new BackgroundListChangeEvent();
            
            if (FileUtils.Exists(Properties.Settings.Default.BackgroundFilePath))
                ImportBackground(Properties.Settings.Default.BackgroundFilePath);
            else
                Properties.Settings.Default.BackgroundFilePath = Properties.Resources.GuideFindPath;

            // MiniPicture
            _miniPictureList = null;
            _miniPictureListChangeEvent = new MiniPictureListChangeEvent();

            if (FileUtils.Exists(Properties.Settings.Default.MiniPictureFilePath))
                ImportMiniPicture(Properties.Settings.Default.MiniPictureFilePath);
            else
                Properties.Settings.Default.MiniPictureFilePath = Properties.Resources.GuideFindPath;

            // Picture
            _pictureList = null;
            _pictureListChangeEvent = new PictureListChangeEvent();

            if (FileUtils.Exists(Properties.Settings.Default.PictureFilePath))
                ImportPicture(Properties.Settings.Default.PictureFilePath);
            else
                Properties.Settings.Default.PictureFilePath = Properties.Resources.GuideFindPath;

            // SE
            _SEList = null;
            _SEListChangeEvent = new SEListChangeEvent();

            if (FileUtils.Exists(Properties.Settings.Default.SEFilePath))
                ImportSE(Properties.Settings.Default.SEFilePath);
            else
                Properties.Settings.Default.SEFilePath = Properties.Resources.GuideFindPath;
        }

        ~Common()
        {
            Properties.Settings.Default.Save();
        }

        #endregion //Singleton



        #region Event

        private CharacterListChangeEvent _characterListChangeEvent;
        public CharacterListChangeEvent CharacterListChangeEvent
        {
            get { return _characterListChangeEvent; }
        }

        private BackgroundListChangeEvent _backgroundListChangeEvent;
        public BackgroundListChangeEvent BackgroundListChangeEvent
        {
            get { return _backgroundListChangeEvent; }
        }

        private MiniPictureListChangeEvent _miniPictureListChangeEvent;
        public MiniPictureListChangeEvent MiniPictureListChangeEvent
        {
            get { return _miniPictureListChangeEvent; }
        }

        private PictureListChangeEvent _pictureListChangeEvent;
        public PictureListChangeEvent PictureListChangeEvent
        {
            get { return _pictureListChangeEvent; }
        }

        private SEListChangeEvent _SEListChangeEvent;
        public SEListChangeEvent SEListChangeEvent
        {
            get { return _SEListChangeEvent; }
        }

        #endregion  //Event



        #region Property

        private Model.Character[] _characterlist;
        public Model.Character[] CharacterList
        {
            get { return _characterlist; }
            private set
            {
                _characterlist = value;

                for (int i = 0; i < _characterlist.Length; ++i)
                    _characterlist[i].Id = i;

                OnPropertyChanged();

                if (null != _characterListChangeEvent)
                    _characterListChangeEvent.Invoke();  /** Raise Event **/
            }
        }
        
        private Model.ArtsResource[] _backgroundList;
        public Model.ArtsResource[] BackgroundList
        {
            get { return _backgroundList; }
            private set
            {
                _backgroundList = value;

                for (int i = 0; i < _backgroundList.Length; ++i)
                    _backgroundList[i].Id = i;

                OnPropertyChanged();

                if (null != BackgroundListChangeEvent)
                    BackgroundListChangeEvent.Invoke();
            }
        }
        
        private Model.ArtsResource[] _miniPictureList;
        public Model.ArtsResource[] MiniPictureList
        {
            get { return _miniPictureList; }
            private set
            {
                _miniPictureList = value;

                for (int i = 0; i < _miniPictureList.Length; ++i)
                    _miniPictureList[i].Id = i;

                OnPropertyChanged();

                if (null != _miniPictureListChangeEvent)
                    _miniPictureListChangeEvent.Invoke();
            }
        }
        
        private Model.ArtsResource[] _pictureList;
        public Model.ArtsResource[] PictureList
        {
            get { return _pictureList; }
            private set
            {
                _pictureList = value;

                for (int i = 0; i < _pictureList.Length; ++i)
                    _pictureList[i].Id = i;

                OnPropertyChanged();

                if (null != _pictureListChangeEvent)
                    _pictureListChangeEvent.Invoke();
            }
        }

        private Model.ArtsResource[] _SEList;
        public Model.ArtsResource[] SEList
        {
            get { return _SEList; }
            private set
            {
                _SEList = value;

                for (int i = 0; i < _SEList.Length; ++i)
                    _SEList[i].Id = i;

                OnPropertyChanged();

                if (null != _SEListChangeEvent)
                    _SEListChangeEvent.Invoke();
            }
        }

        #endregion //Property



        #region Public Methods : Import

        public void ImportCharacter(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return;

            Model.Character[] chArray = null;

            bool isParsed = XmlUtils.Parse(filePath, out chArray);
            if (false == isParsed)
                return;

            //
            Properties.Settings.Default.CharacterFilePath = filePath;
            CharacterList = chArray;
        }


        public void ImportBackground(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return;

            Model.ArtsResource[] rscArray = null;
            
            bool isParsed = XmlUtils.Parse(filePath, out rscArray, 
                                        Model.ArtsResource.XML_BACKGROUND_NAME);
            if (false == isParsed)
                return;

            Properties.Settings.Default.BackgroundFilePath = filePath;
            BackgroundList = rscArray;
        }
        

        public void ImportMiniPicture(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return;

            Model.ArtsResource[] rscArray = null;

            bool isParsed = XmlUtils.Parse(filePath, out rscArray,
                                        Model.ArtsResource.XML_MINIPICTURE_NAME);
            if (false == isParsed)
                return;

            Properties.Settings.Default.MiniPictureFilePath = filePath;
            MiniPictureList = rscArray;
        }
        

        public void ImportPicture(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return;

            Model.ArtsResource[] rscArray = null;

            bool isParsed = XmlUtils.Parse(filePath, out rscArray,
                                        Model.ArtsResource.XML_PICTURE_NAME);
            if (false == isParsed)
                return;

            Properties.Settings.Default.PictureFilePath = filePath;
            PictureList = rscArray;
        }


        public void ImportSE(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return;

            Model.ArtsResource[] rscArray = null;

            bool isParsed = XmlUtils.Parse(filePath, out rscArray,
                                        Model.ArtsResource.XML_SE_NAME);
            if (false == isParsed)
                return;

            Properties.Settings.Default.SEFilePath = filePath;
            SEList = rscArray;
        }

        #endregion //Public Methods : Import



        #region Public Method : IsValid(Id)

        public bool IsValidCharacter(int id)
        {
            if (id < 0)
                return false;

            if (null == CharacterList)
                return false;

            // id(index) range : 0 ~ (CharacterList.Count - 1)
            if (id >= CharacterList.Length)
                return false;

            return true;
        }

        public bool IsValidBackground(int id)
        {
            if (id < 0)
                return false;

            if (null == BackgroundList)
                return false;

            // id(index) range : 0 ~ (BackgroundList.Count - 1)
            if (id >= BackgroundList.Length)
                return false;

            return true;
        }

        public bool IsValidMiniPicture(int id)
        {
            if (id < 0)
                return false;

            if (null == MiniPictureList)
                return false;

            // id(index) range : 0 ~ (MiniPictureList.Count - 1)
            if (id >= MiniPictureList.Length)
                return false;

            return true;
        }

        public bool IsValidPicture(int id)
        {
            if (id < 0)
                return false;

            if (null == PictureList)
                return false;

            // id(index) range : 0 ~ (PictureList.Count - 1)
            if (id >= PictureList.Length)
                return false;

            return true;
        }

        public bool IsValidSE(int id)
        {
            if (id < 0)
                return false;

            if (null == SEList)
                return false;

            // id(index) range : 0 ~ (SEList.Count - 1)
            if (id >= SEList.Length)
                return false;

            return true;
        }

        #endregion // Public Method : IsValid(Id)



        #region Private Methods
        #endregion //Private Methods
    }
}
