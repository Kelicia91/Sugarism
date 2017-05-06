using System;
using System.Windows;
using System.Windows.Data;


namespace ScenarioEditor.View.Popup
{
    /// <summary>
    /// Interaction logic for EditSwitchCase.xaml
    /// </summary>
    public partial class EditSwitchCase : Window
    {
        public EditSwitchCase(ViewModel.Popup.EditSwitchCase viewModel)
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
    
    class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((null == value) || (false == (value is bool)))
                return Visibility.Collapsed;
            
            bool boolValue = (bool)value;

            if (boolValue)
                return Visibility.Visible;
            else
                return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
