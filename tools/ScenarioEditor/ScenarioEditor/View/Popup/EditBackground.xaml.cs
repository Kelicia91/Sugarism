using System;
using System.Windows;
using System.Windows.Data;

namespace ScenarioEditor.View.Popup
{
    /// <summary>
    /// Interaction logic for EditBackground.xaml
    /// </summary>
    public partial class EditBackground : Window
    {
        public EditBackground(ViewModel.Popup.EditBackground viewModel)
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

    // for ComboBox : convert Model.ArtsResource -> formatted string.
    public class ArtsResourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((null == value) || (false == (value is Model.ArtsResource)))
                return string.Empty;

            Model.ArtsResource rsrc = value as Model.ArtsResource;

            string s = string.Format("{0}:{1}", rsrc.Id, rsrc.Description);
            return s;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
