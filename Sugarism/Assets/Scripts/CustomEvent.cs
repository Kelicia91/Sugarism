
public class CustomEvent
{
    private static CustomEvent _instance;
    public static CustomEvent Instance
    {
        get
        {
            if (null == _instance)
            {
                _instance = new CustomEvent();
            }
            return _instance;
        }
    }

    private CustomEvent()
    {
        _cmdLinesEvent = new CmdLinesEventHandler(onCmdLines);
    }


    //
    public delegate void CmdLinesEventHandler(int characterId, string lines);
    private event CmdLinesEventHandler _cmdLinesEvent;

    private void onCmdLines(int characterId, string lines) { /*Log.Debug("CustomEvent.onCmdLines");*/ }

    public void Invoke(int characterId, string lines)
    {
        _cmdLinesEvent.Invoke(characterId, lines);
    }

    // @warn : attach order
    public void Attach(CmdLinesEventHandler handler)
    {
        if (null == handler)
            return;

        _cmdLinesEvent += handler;
    }

    public void Detach(CmdLinesEventHandler handler)
    {
        if (null == handler)
            return;

        _cmdLinesEvent -= handler;
    }
}
