using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;


namespace ScenarioEditor.View.Popup
{
    /// <summary>
    /// Interaction logic for EditText.xaml
    /// </summary>
    public partial class EditText : Window
    {
        public EditText(ViewModel.Popup.EditText viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (null == textBox)
                return;

            Sugarism.CmdText.ValidationResult result = Sugarism.CmdText.IsValid(textBox.Text);
            if (Sugarism.CmdText.ValidationResult.Success != result)
            {
                Dispatcher.BeginInvoke(DispatcherPriority.Input, (Action)(() => textBox.Undo()));
            }
        }
    }
}
