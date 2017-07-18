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
    /// Interaction logic for EditTargetAppear.xaml
    /// </summary>
    public partial class EditTargetAppear : Window
    {
        public EditTargetAppear(ViewModel.Popup.EditTargetAppear viewModel)
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

    // for ComboBox : convert Sugarism.EFace -> formatted string.
    class EFaceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((null == value) || (false == (value is Sugarism.EFace)))
                return string.Empty;

            Sugarism.EFace face = (Sugarism.EFace)value;
            return face.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    // for ComboBox : convert Sugarism.ECostume -> formatted string.
    class ECostumeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((null == value) || (false == (value is Sugarism.ECostume)))
                return string.Empty;

            Sugarism.ECostume cos = (Sugarism.ECostume)value;
            return cos.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
