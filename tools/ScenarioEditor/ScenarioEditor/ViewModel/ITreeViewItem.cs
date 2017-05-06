using System.Windows.Input;

namespace ScenarioEditor.ViewModel
{
    public interface ITreeViewItem
    {
        bool IsSelected { get; set; }
        bool IsExpanded { get; set; }

        ICommand CmdExpand { get; }

        InputBindingCollection InputBindings { get; }
    }
}
