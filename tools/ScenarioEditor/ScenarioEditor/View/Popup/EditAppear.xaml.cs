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

namespace ScenarioEditor.View.Popup
{
    /// <summary>
    /// Interaction logic for EditAppear.xaml
    /// </summary>
    public partial class EditAppear : Window
    {
        public EditAppear(ViewModel.Popup.EditAppear viewModel)
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

    // for ComboBox : convert Sugarism.EPosition -> formatted string.
    class EPositionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((null == value) || (false == (value is Sugarism.EPosition)))
                return string.Empty;

            Sugarism.EPosition pos = (Sugarism.EPosition)value;
            return pos.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
