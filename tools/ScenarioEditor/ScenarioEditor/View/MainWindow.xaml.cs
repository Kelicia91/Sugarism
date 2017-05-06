using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


namespace ScenarioEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModel.MainViewModel _vm;

        public MainWindow()
        {
            InitializeComponent();

            if (null != DataContext)
                _vm = DataContext as ViewModel.MainViewModel;
        }        
        

        private void Scene_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            TextBlock item = sender as TextBlock;
            if (null == item)
                return;
            
            ContextMenu cMenu = item.ContextMenu;
            if (null == cMenu)
                return;

            ViewModel.Scene scene = item.DataContext as ViewModel.Scene;
            if (null == scene)
                return;
            
            if (null == scene.Owner)
                return;
            
            /* WARN : MenuItem's index is hard coded... */
            const int upItemIndex = 4;
            MenuItem upItem = cMenu.Items.GetItemAt(upItemIndex) as MenuItem;
            if (null != upItem)
                upItem.IsEnabled = scene.Owner.CanUp(scene);

            const int downItemIndex = 5;
            MenuItem downItem = cMenu.Items.GetItemAt(downItemIndex) as MenuItem;
            if (null != downItem)
                downItem.IsEnabled = scene.Owner.CanDown(scene);
        }

        private void Cmd_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            TextBlock item = sender as TextBlock;
            if (null == item)
                return;

            ContextMenu cMenu = item.ContextMenu;
            if (null == cMenu)
                return;

            ViewModel.Command cmd = item.DataContext as ViewModel.Command;
            if (null == cmd)
                return;

            if (null == cmd.Owner)
                return;

            /* WARN : MenuItem's index is hard coded... */
            const int upItemIndex = 4;
            MenuItem upItem = cMenu.Items.GetItemAt(upItemIndex) as MenuItem;
            if (null != upItem)
                upItem.IsEnabled = cmd.Owner.CanUp(cmd);

            const int downItemIndex = 5;
            MenuItem downItem = cMenu.Items.GetItemAt(downItemIndex) as MenuItem;
            if (null != downItem)
                downItem.IsEnabled = cmd.Owner.CanDown(cmd);
        }



        private void DeleteScenario_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (null == btn)
                return;

            ViewModel.Scenario scenario = btn.DataContext as ViewModel.Scenario;
            if (null == scenario)
                return;

            string msg = string.Format(Properties.Resources.WarnDeleteScenario, scenario.FileName);
            string title = Properties.Resources.Warning;
            MessageBoxButton msgBtn = MessageBoxButton.OKCancel;

            MessageBoxResult result = MessageBox.Show(msg, title, msgBtn);
            switch (result)
            {
                case MessageBoxResult.OK:
                    break;

                default:
                    return;
            }

            scenario.Delete();
        }

        private void DeleteScene_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mItem = sender as MenuItem;
            if (null == mItem)
                return;

            ViewModel.Scene scene = mItem.DataContext as ViewModel.Scene;
            if (null == scene)
                return;

            string msg = string.Format(Properties.Resources.WarnDeleteScene, scene.Description);
            string title = Properties.Resources.Warning;
            MessageBoxButton btn = MessageBoxButton.OKCancel;

            MessageBoxResult result = MessageBox.Show(msg, title, btn);
            switch (result)
            {
                case MessageBoxResult.OK:
                    break;

                default:
                    return;
            }

            scene.Delete();
        }



        private void TreeViewItem_OnMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem treeViewItem = VisualUpwardSearch(e.OriginalSource as DependencyObject);
            if (null != treeViewItem)
            {
                treeViewItem.Focus();
                e.Handled = true;
            }
        }

        private TreeViewItem VisualUpwardSearch(DependencyObject source)
        {
            while (null != source && !(source is TreeViewItem))
                source = VisualTreeHelper.GetParent(source);

            return source as TreeViewItem;
        }



        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
