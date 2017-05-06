using System;
using System.Windows;
using System.Windows.Data;


namespace ScenarioEditor.View.Popup
{
    /// <summary>
    /// Interaction logic for PopupAddCmd.xaml
    /// </summary>
    public partial class AddCmd : Window
    {
        public AddCmd(ViewModel.Popup.AddCmd viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }

    // for ComboBox : convert Sugarism.Command.Type -> formatted string.
    class CommandTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((null == value) || (false == (value is Sugarism.Command.Type)))
                return string.Empty;

            Sugarism.Command.Type cmdType = (Sugarism.Command.Type)value;

            string strCmdType = cmdType.ToString();
            int intCmdType = System.Convert.ToInt32(cmdType);

            string s = string.Format("{0}:{1}", intCmdType, strCmdType);
            return s;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
