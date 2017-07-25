
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


public class BuyCostumeEvent
{
    public delegate void Handler(int costumeId);
    private event Handler _event = null;

    public BuyCostumeEvent()
    {
        _event = new Handler(onBuyCostume);
    }

    // default handler
    private void onBuyCostume(int costumeId)
    {
        Log.Debug(string.Format("onBuyCostume; {0}", costumeId));
    }

    public void Invoke(int costumeId) { _event.Invoke(costumeId); }

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


public class WearCostumeEvent
{
    public delegate void Handler(int costumeId);
    private event Handler _event = null;

    public WearCostumeEvent()
    {
        _event = new Handler(onWearCostume);
    }

    // default handler
    private void onWearCostume(int costumeId)
    {
        Log.Debug(string.Format("onWearCostume; {0}", costumeId));
    }

    public void Invoke(int costumeId) { _event.Invoke(costumeId); }

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


public class EndNurtureEvent
{
    public delegate void Handler();
    private event Handler _event = null;

    public EndNurtureEvent()
    {
        _event = new Handler(onEndNurture);
    }

    // default handler
    private void onEndNurture()
    {
        Log.Debug("onEndNurture");
    }

    public void Invoke() { _event.Invoke(); }

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
