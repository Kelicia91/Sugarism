using System.Windows;

using Microsoft.Win32;  // OpenFileDialog


namespace ScenarioEditor.View.Popup
{
    /// <summary>
    /// Interaction logic for PopupSettings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        private ViewModel.Popup.Settings _vm;
        
        public Settings(ViewModel.Popup.Settings viewModel)
        {
            InitializeComponent();

            _vm = viewModel;
            DataContext = _vm;
        }

        private void FindPath_Click(object sender, RoutedEventArgs e)
        {
            if (null == _vm)
            {
                Log.Error(Properties.Resources.ErrSysError);
                return;
            }

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = JsonUtils.FILE_EXTENSION;
            dlg.Filter = JsonUtils.FILE_FILTER;

            var isGotFilePath = dlg.ShowDialog();
            if (false == isGotFilePath)
                return;

            _vm.CharacterFilePath = dlg.FileName;
        }

        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            if (null == _vm)
            {
                Log.Error(Properties.Resources.ErrSysError);
                return;
            }

            // if the path is imported already, warn whether or not to import again.
            if ((_vm.CharacterFilePath).Equals(Properties.Settings.Default.CharacterFilePath))
            {
                string msg = Properties.Resources.WarnAlreadyImportedFile;
                string title = Properties.Resources.Warning;
                MessageBoxButton btn = MessageBoxButton.OKCancel;

                MessageBoxResult result = MessageBox.Show(msg, title, btn);
                switch (result)
                {
                    case MessageBoxResult.OK:
                        DialogResult = true;
                        break;

                    default:
                        DialogResult = false;
                        break;
                }
            }
            else
            {
                DialogResult = true;
            }
            
            Close();
        }
    }
}
