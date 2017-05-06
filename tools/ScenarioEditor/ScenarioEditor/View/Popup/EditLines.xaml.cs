using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Threading;


namespace ScenarioEditor.View.Popup
{
    /// <summary>
    /// Interaction logic for EditLines.xaml
    /// </summary>
    public partial class EditLines : Window
    {
        public EditLines(ViewModel.Popup.EditLines viewModel)
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

            Sugarism.CmdLines.ValidationResult result = Sugarism.CmdLines.IsValid(textBox.Text);
            if (Sugarism.CmdLines.ValidationResult.Success != result)
            {
                Dispatcher.BeginInvoke(DispatcherPriority.Input, (Action)(() => textBox.Undo()));
            }
        }
    }

    // for ComboBox : convert Sugarism.Character -> formatted string.
    class CharacterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((null == value) || (false == (value is Sugarism.Character)))
                return string.Empty;

            Sugarism.Character c = value as Sugarism.Character;

            string s = string.Format("{0}:{1}", c.Id, c.Name);
            return s;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
