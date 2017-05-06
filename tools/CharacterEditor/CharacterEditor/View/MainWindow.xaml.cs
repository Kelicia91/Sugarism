using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;


namespace CharacterEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel _viewModel = null;
        
        public MainWindow()
        {
            InitializeComponent();

            _viewModel = DataContext as MainViewModel;
        }


        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (null == _viewModel)
            {
                Log.Error(Properties.Resources.ErrMsgBoxTitle, Properties.Resources.ErrSysError);
                return;
            }
            
            _viewModel.Load(e.AddedItems);
        }


        private void FindTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Return: // Enter
                    find();
                    break;

                default:
                    break;
            }
        }


        private void find()
        {
            if (null == _viewModel)
            {
                Log.Error(Properties.Resources.ErrMsgBoxTitle, Properties.Resources.ErrSysError);
                return;
            }

            _viewModel.CmdFind.Execute(null);
        }
    }
}
