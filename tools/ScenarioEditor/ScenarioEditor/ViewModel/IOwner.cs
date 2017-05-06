using System.Windows.Input;


namespace ScenarioEditor.ViewModel
{
    // for ContextMenu
    public interface IOwner<T>
    {
        ICommand CmdAddChild { get; }

        void Insert(int index, T child);
        void Delete(T child);
        void Up(T child);
        void Down(T child);
        bool CanUp(T child);
        bool CanDown(T child);

        int GetIndexOf(T child);
    }
}
