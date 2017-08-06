using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Microsoft.Win32;  // OpenFileDialog, SaveFileDialog

/*  MUST-DO
 *  - All ViewModel do NOT refer to View.
 *  - All ViewModel do NOT use 'using System.Windows' for MessageBox.
 *      if you need MessageBox, access it in View
 *      but you allow ViewModel to refer View in the case of the following exceptions.
 *      -> Test for Calling Method binded to ICommand
 *      -> ViewModel is for Popup
 */

namespace ScenarioEditor.ViewModel
{
    public class MainViewModel : ModelBase
    {
        private readonly Newtonsoft.Json.JsonSerializerSettings _jsonSerializerSettings;

        public MainViewModel()
        {
            _jsonSerializerSettings = new Newtonsoft.Json.JsonSerializerSettings();
            _jsonSerializerSettings.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto;

            // @note: for removing assembly-name because of unity.iOS issue
            KnownTypesBinder knownTypesBinder = new KnownTypesBinder
            {
                KnownTypes = new List<Type>
                {
                    typeof(Sugarism.Scenario), typeof(Sugarism.Scene),
                    typeof(Sugarism.CmdAppear), typeof(Sugarism.CmdBackground),
                    typeof(Sugarism.CmdCase), typeof(Sugarism.CmdDisappear),
                    typeof(Sugarism.CmdFeeling), typeof(Sugarism.CmdFilter),
                    typeof(Sugarism.CmdLines), typeof(Sugarism.CmdMiniPicture),
                    typeof(Sugarism.CmdPicture), typeof(Sugarism.CmdSE),
                    typeof(Sugarism.CmdSwitch), typeof(Sugarism.CmdTargetAppear),
                    typeof(Sugarism.CmdText)
                }
            };
            _jsonSerializerSettings.SerializationBinder = knownTypesBinder;

            _selectedScenario = null;
            _scenarioList = new ObservableCollection<Scenario>();

            AddSampleScenario();
        }


        #region Properties

        private Scenario _selectedScenario;
        public Scenario SelectedScenario
        {
            get { return _selectedScenario; }
            set { _selectedScenario = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Scenario> _scenarioList;
        public ObservableCollection<Scenario> ScenarioList
        {
            get { return _scenarioList; }
        }

        #endregion //Properties


        #region Commands

        private ICommand _cmdAddScenario;
        public ICommand CmdAddScenario
        {
            get
            {
                if (null == _cmdAddScenario)
                {
                    _cmdAddScenario = new RelayCommand(param => AddSampleScenario());
                }
                return _cmdAddScenario;
            }
        }

        private ICommand _cmdSetCharacterPath;
        public ICommand CmdSetCharacterPath
        {
            get
            {
                if (null == _cmdSetCharacterPath)
                {
                    _cmdSetCharacterPath = new RelayCommand(param => setCharacterPath());
                }
                return _cmdSetCharacterPath;
            }
        }

        private ICommand _cmdSetTargetPath;
        public ICommand CmdSetTargetPath
        {
            get
            {
                if (null == _cmdSetTargetPath)
                {
                    _cmdSetTargetPath = new RelayCommand(param => setTargetPath());
                }
                return _cmdSetTargetPath;
            }
        }

        private ICommand _cmdSetBackgroundPath;
        public ICommand CmdSetBackgroundPath
        {
            get
            {
                if (null == _cmdSetBackgroundPath)
                {
                    _cmdSetBackgroundPath = new RelayCommand(param => setBackgroundPath());
                }
                return _cmdSetBackgroundPath;
            }
        }

        private ICommand _cmdSetMiniPicturePath;
        public ICommand CmdSetMiniPicturePath
        {
            get
            {
                if (null == _cmdSetMiniPicturePath)
                {
                    _cmdSetMiniPicturePath = new RelayCommand(param => setMiniPicturePath());
                }
                return _cmdSetMiniPicturePath;
            }
        }

        private ICommand _cmdSetPictruePath;
        public ICommand CmdSetPicturePath
        {
            get
            {
                if (null == _cmdSetPictruePath)
                {
                    _cmdSetPictruePath = new RelayCommand(param => setPicturePath());
                }
                return _cmdSetPictruePath;
            }
        }

        private ICommand _cmdSetSEPath;
        public ICommand CmdSetSEPath
        {
            get
            {
                if (null == _cmdSetSEPath)
                {
                    _cmdSetSEPath = new RelayCommand(param => setSEPath());
                }
                return _cmdSetSEPath;
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

        #endregion //Commands



        #region Public Methods

        public void AddSampleScenario()
        {
            Sugarism.Scenario model = new Sugarism.Scenario(null);

            Scenario snr = new Scenario(model);

            Add(snr);
        }

        public void Add(Scenario scenario)
        {
            if (null == scenario)
                return;

            ScenarioList.Add(scenario);

            scenario.Owner = this;
            SelectedScenario = scenario;
        }

        public void Delete(Scenario scenario)
        {
            ScenarioList.Remove(scenario);
        }

        public void Import()
        {
            // get file path to import
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = JsonUtils.FILE_EXTENSION;
            dlg.Filter = JsonUtils.FILE_FILTER;

            var isGotFilePath = dlg.ShowDialog();
            if (false == isGotFilePath)
                return;

            string filePath = dlg.FileName;
            if (string.IsNullOrEmpty(filePath))
                return;

            string text;

            // read the file as text
            bool isRead = FileUtils.ReadAllTextAsUTF8(filePath, out text);
            if (false == isRead)
                return;

            // deserialize the text to object
            object result = null;
            bool isDeserialized = JsonUtils.Deserialize<Sugarism.Scenario>(text, out result, _jsonSerializerSettings);
            if (false == isDeserialized)
                return;

            //
            Sugarism.Scenario model = result as Sugarism.Scenario;
            Scenario scenario = new Scenario(filePath, model);
            Add(scenario);
        }

        public void Export()
        {
            // get file path to export
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.DefaultExt = JsonUtils.FILE_EXTENSION;
            dlg.Filter = JsonUtils.FILE_FILTER;

            if (FileUtils.Exists(SelectedScenario.FileFullPath))
            {
                dlg.InitialDirectory = SelectedScenario.FileFullPath;
            }
            else
            {
                Environment.SpecialFolder folderPath = Environment.SpecialFolder.DesktopDirectory;
                dlg.InitialDirectory = Environment.GetFolderPath(folderPath);
            }

            var isGotFilePath = dlg.ShowDialog();
            if (false == isGotFilePath)
                return;

            string filePath = dlg.FileName;
            if (string.IsNullOrEmpty(filePath))
                return;

            string text;

            // serialize object to text
            bool isSerialized = JsonUtils.Serialize(SelectedScenario.Model, out text, _jsonSerializerSettings);
            if (false == isSerialized)
                return;

            // write the file as text
            bool isWritten = FileUtils.WriteAllTextAsUTF8(filePath, text);
            if (false == isWritten)
                return;

            //
            SelectedScenario.FileFullPath = filePath;
        }

        #endregion //Public Methods



        #region Private Methods

        private void setCharacterPath()
        {
            string label = Properties.Resources.LabelCharacterFilePath;
            string path = Properties.Settings.Default.CharacterFilePath;

            string result = Popup.Settings.Instance.Show(label, path);

            Common.Instance.ImportCharacter(result);
        }

        private void setTargetPath()
        {
            string label = Properties.Resources.LabelTargetFilePath;
            string path = Properties.Settings.Default.TargetFilePath;

            string result = Popup.Settings.Instance.Show(label, path);

            Common.Instance.ImportTarget(result);
        }

        private void setBackgroundPath()
        {
            string label = Properties.Resources.LabelBackgroundFilePath;
            string path = Properties.Settings.Default.BackgroundFilePath;

            string result = Popup.Settings.Instance.Show(label, path);

            Common.Instance.ImportBackground(result);
        }

        private void setMiniPicturePath()
        {
            string label = Properties.Resources.LabelMiniPictureFilePath;
            string path = Properties.Settings.Default.MiniPictureFilePath;

            string result = Popup.Settings.Instance.Show(label, path);

            Common.Instance.ImportMiniPicture(result);
        }

        private void setPicturePath()
        {
            string label = Properties.Resources.LabelPictureFilePath;
            string path = Properties.Settings.Default.PictureFilePath;

            string result = Popup.Settings.Instance.Show(label, path);
            
            Common.Instance.ImportPicture(result);
        }

        private void setSEPath()
        {
            string label = Properties.Resources.LabelSEFilePath;
            string path = Properties.Settings.Default.SEFilePath;

            string result = Popup.Settings.Instance.Show(label, path);
            
            Common.Instance.ImportSE(result);
        }

        #endregion //Private Methods

    }
}
