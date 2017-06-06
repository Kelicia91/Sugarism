using System;
using System.IO;
using System.Text;
using System.Windows.Input; // for ICommand
using System.Collections;
using System.Collections.ObjectModel;

using Microsoft.Win32;  // for FileDialog

using Sugarism; // Model


namespace CharacterEditor
{
    class MainViewModel : ModelBase
    {
        private readonly Encoding _ENCODING;

        // default constructor
        public MainViewModel()
        {
            _ENCODING = Encoding.UTF8;

            _charViewModel = new CharacterViewModel();
            _characterList = new ObservableCollection<Character>();

            Init();
        }


        #region Properties

        private int _selectedTabItemIndex = 0;  // for tabControl.selectedIndex
        public int SelectedTabItemIndex
        {
            get { return _selectedTabItemIndex; }
            set { _selectedTabItemIndex = value; }
        }

        private string _importPath; // for textblock
        public string ImportPath
        {
            get { return _importPath; }
            private set { _importPath = value; OnPropertyChanged(); }
        }

        private string _exportPath; // for textblock
        public string ExportPath
        {
            get { return _exportPath; }
            private set { _exportPath = value; OnPropertyChanged(); }
        }

        private string _nameToFind; // for textbox
        public string NameToFind
        {
            get { return _nameToFind; }
            set { _nameToFind = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Character> _characterList; // for listview
        public ObservableCollection<Character> CharacterList
        {
            get { return _characterList; }
            private set { _characterList = value; OnPropertyChanged(); }
        }

        private Character _selectedCharacter;   // for listview.selectedItem
        public Character SelectedCharacter
        {
            get { return _selectedCharacter; }
            set { _selectedCharacter = value; OnPropertyChanged(); }
        }

        private CharacterViewModel _charViewModel;  // for character input format
        public CharacterViewModel CharViewModel
        {
            get { return _charViewModel; }
        }

        #endregion //Properties



        #region Commands

        private ICommand _cmdNew;
        public ICommand CmdNew
        {
            get
            {
                if (null == _cmdNew)
                {
                    _cmdNew = new RelayCommand(param => Init());
                }
                return _cmdNew;
            }
        }

        private ICommand _cmdImport;
        public ICommand CmdImport
        {
            get
            {
                if (null == _cmdImport)
                {
                    _cmdImport = new RelayCommand(param => Import());
                }
                return _cmdImport;
            }
        }

        private ICommand _cmdExport;
        public ICommand CmdExport
        {
            get
            {
                if (null == _cmdExport)
                {
                    _cmdExport = new RelayCommand(param => Export());
                }
                return _cmdExport;
            }
        }

        private ICommand _cmdFind;
        public ICommand CmdFind
        {
            get
            {
                if (null == _cmdFind)
                {
                    _cmdFind = new RelayCommand(param => Find());
                }
                return _cmdFind;
            }
        }

        private ICommand _cmdAdd;
        public ICommand CmdAdd
        {
            get
            {
                if (null == _cmdAdd)
                {
                    // @warning
                    // If a Button is bound to a Command, 
                    // the CanExecute() determines if the Button is enabled or not.
                    _cmdAdd = new RelayCommand(param => Add());
                }
                return _cmdAdd;
            }
        }

        private ICommand _cmdDelete;
        public ICommand CmdDelete
        {
            get
            {
                if (null == _cmdDelete)
                {
                    _cmdDelete = new RelayCommand(param => Delete());
                }
                return _cmdDelete;
            }
        }

        private ICommand _cmdSave;
        public ICommand CmdSave
        {
            get
            {
                if (null == _cmdSave)
                {
                    _cmdSave = new RelayCommand(param => Save(), param => isEditorTab());
                }
                return _cmdSave;
            }
        }

        #endregion //Commands


        
        #region Public Logics

        public void Init()
        {
            ImportPath = string.Empty;
            ExportPath = string.Empty;

            NameToFind = string.Empty;

            CharacterList.Clear();

            SelectedCharacter = null;
            
            CharViewModel.Init();
        }


        public void Import()
        {
            // get file path
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = JsonUtils.FILE_EXTENSION;
            dlg.Filter = JsonUtils.FILE_FILTER;

            var isGotFilePath = dlg.ShowDialog();
            if (false == isGotFilePath)
                return;

            if((dlg.FileName).Equals(ImportPath))
            {
                Log.Error(Properties.Resources.Warning, Properties.Resources.ErrAlreadyImportedFile);
                return;
            }

            // read the file as text
            string contents = string.Empty;
            try
            {
                contents = File.ReadAllText(dlg.FileName, _ENCODING);
            }
            catch (Exception e)
            {
                Log.Error(Properties.Resources.ErrMsgBoxTitle, e.Message);
                return;
            }

            // deserialize the text to object
            object result;
            bool isDeserialized = JsonUtils.Deserialize<ObservableCollection<Character>>(contents, out result);
            if (false == isDeserialized)
                return;

            Init();

            ImportPath = dlg.FileName;
            CharacterList = result as ObservableCollection<Character>;
        }


        public void Export()
        {
            // get file path
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.DefaultExt = JsonUtils.FILE_EXTENSION;
            dlg.Filter = JsonUtils.FILE_FILTER;

            if (0 < ImportPath.Length)
            {
                dlg.InitialDirectory = ImportPath;
            }
            else
            {
                Environment.SpecialFolder folderPath = Environment.SpecialFolder.DesktopDirectory;
                dlg.InitialDirectory = Environment.GetFolderPath(folderPath);
            }

            var isGotFilePath = dlg.ShowDialog();
            if (false == isGotFilePath)
            {
                return;
            }

            ExportPath = dlg.FileName;

            // set character's id
            for (int i = Character.START_ID; i < CharacterList.Count; ++i)
            {
                CharacterList[i].Id = i;
            }

            // serialize object to text
            string contents;
            bool isSerialized = JsonUtils.Serialize(CharacterList, out contents);
            if (false == isSerialized)
            {
                return;
            }

            // write the file as text
            try
            {
                File.WriteAllText(ExportPath, contents, _ENCODING);
            }
            catch (Exception e)
            {
                Log.Error(Properties.Resources.ErrMsgBoxTitle, e.Message);
                return;
            }
        }


        public void Find()
        {
            int offset = 0;
            if (null != SelectedCharacter)
            {
                offset = CharacterList.IndexOf(SelectedCharacter);
                ++offset;
            }

            // return the first character found by comparing name (index = {offset ~ max})
            for (int i = offset; i < CharacterList.Count; ++i)
            {
                if (NameToFind.Equals(CharacterList[i].Name))
                {
                    SelectedCharacter = CharacterList[i];
                    return; // found
                }
            }

            // not found
            string msg = string.Format(Properties.Resources.ErrCharacterNotFound, NameToFind);
            Log.Error(Properties.Resources.ErrMsgBoxTitle, msg);
        }


        public void Add()
        {
            CharViewModel.Init();

            SelectedCharacter = null;
        }


        public void Delete()
        {
            if (null == SelectedCharacter)
            {
                Log.Error(Properties.Resources.ErrMsgBoxTitle, Properties.Resources.ErrCharacterToDeleteIsNull);
                return;
            }

            CharacterList.Remove(SelectedCharacter);

            SelectedCharacter = null;
            
            CharViewModel.Init();
        }

        public void Save()
        {
            if (false == CharViewModel.IsValid())
                return;

            if (null == SelectedCharacter)
            {
                // add
                if (false == canAdd())
                    return;

                Character c = new Character(CharViewModel.Name);

                CharacterList.Add(c);

                CharViewModel.Init();
            }
            else
            {
                // edit
                // @note : Character Input Format 변경될 때마다 반영되어야 한다. (save)
                SelectedCharacter.Name = CharViewModel.Name;
            }
        }


        /** called from View.Callback **/
        public void Load(IList selectedItemList)
        {
            if (0 >= selectedItemList.Count)
                return;

            // @note : Character Input Format 변경될 때마다 반영되어야 한다. (load)
            CharViewModel.Name = SelectedCharacter.Name;
        }

        #endregion //Public Logics



        #region Private Logics
        
        private bool isEditorTab()
        {
            // ! ASSUME : Editor Tab Index = 0
            if (0 == _selectedTabItemIndex)
                return true;
            else
                return false;
        }


        private bool canAdd()
        {
            if (Const.MAX_NUM_OF_CHARACTER <= _characterList.Count)
            {
                string msg = string.Format(Properties.Resources.ErrOverMaxNumCharacter, Const.MAX_NUM_OF_CHARACTER);
                Log.Error(Properties.Resources.ErrMsgBoxTitle, msg);
                return false;
            }

            return true;
        }

        #endregion //Private Logics
    }
}
