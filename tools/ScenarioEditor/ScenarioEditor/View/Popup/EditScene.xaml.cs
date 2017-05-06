using System.Windows;


namespace ScenarioEditor.View.Popup
{
    /// <summary>
    /// Interaction logic for PopupEditScene.xaml
    /// </summary>
    public partial class EditScene : Window
    {
        ViewModel.Popup.EditScene _vm;

        public EditScene(ViewModel.Popup.EditScene viewModel)
        {
            InitializeComponent();

            _vm = viewModel;
            DataContext = _vm;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (null == _vm)
                return;

            if(string.Empty == _vm.Description)
            {
                _vm.Description = Properties.Resources.GuideSceneDescription;
            }

            DialogResult = true;
            Close();
        }
    }
}
