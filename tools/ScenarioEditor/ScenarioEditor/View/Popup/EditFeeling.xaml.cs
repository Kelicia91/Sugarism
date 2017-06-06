using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace ScenarioEditor.View.Popup
{
    /// <summary>
    /// Interaction logic for EditFeeling.xaml
    /// </summary>
    public partial class EditFeeling : Window
    {
        public const string DIGIT_PATTERN = "[^0-9]+";

        public EditFeeling(ViewModel.Popup.EditFeeling viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            e.Handled = Regex.IsMatch(e.Text, DIGIT_PATTERN);
        }
    }

    // for ComboBox : convert Sugarism.EOperation -> formatted string.
    public class EOperationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((null == value) || (false == (value is Sugarism.EOperation)))
                return string.Empty;

            Sugarism.EOperation c = (Sugarism.EOperation)value;
            
            return c.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
