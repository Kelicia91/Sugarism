
public class MoneyChangeEvent
{
    public delegate void Handler(int money);
    private event Handler _event = null;

    public MoneyChangeEvent()
    {
        _event = new Handler(onMoneyChanged);
    }

    // default handler
    private void onMoneyChanged(int money)
    {
        Log.Debug(string.Format("onMoneyChanged; {0}", money));
    }

    public void Invoke(int money) { _event.Invoke(money); }

    public void Attach(Handler handler)
    {
        if (null == handler)
            return;

        _event += handler;
    }

    public void Detach(Handler handler)
    {
        if (null == handler)
            return;

        _event -= handler;
    }
}
