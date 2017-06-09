using System.Windows;

namespace ScenarioEditor.View.Popup
{
    /// <summary>
    /// Interaction logic for EditSE.xaml
    /// </summary>
    public partial class EditSE : Window
    {
        public EditSE(ViewModel.Popup.EditSE viewModel)
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
