using System.Windows;

namespace ScenarioEditor.View.Popup
{
    /// <summary>
    /// Interaction logic for EditPicture.xaml
    /// </summary>
    public partial class EditPicture : Window
    {
        public EditPicture(ViewModel.Popup.EditPicture viewModel)
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
