using System;
using System.Windows.Input;
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

        private ICommand _cmdSettings;
        public ICommand CmdSettings
        {
            get
            {
                if (null == _cmdSettings)
                {
                    _cmdSettings = new RelayCommand(param => Settings());
                }
                return _cmdSettings;
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

        public void Settings()
        {
            string result = Popup.Settings.Instance.Show(Properties.Settings.Default.CharacterFilePath);

            Common.Instance.Import(result);
        }

        #endregion //Public Methods



        #region Private Methods
        #endregion //Private Methods

    }
}
