using System.Windows;

namespace ScenarioEditor.View.Popup
{
    /// <summary>
    /// Interaction logic for EditMiniPicture.xaml
    /// </summary>
    public partial class EditMiniPicture : Window
    {
        public EditMiniPicture(ViewModel.Popup.EditMiniPicture viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
