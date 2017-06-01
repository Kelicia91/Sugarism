
// @todo : 이 클래스는 삭제할 것.
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
    
    public delegate void CmdLinesEventHandler(int characterId, string lines);
    private event CmdLinesEventHandler _cmdLinesEvent;

    // default handler
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


public class MoneyChangeEvent
{
    public delegate void Handler(int money);
    private event Handler _event;

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


public class YearChangeEvent
{
    public delegate void Handler(int year);
    private event Handler _event;

    public YearChangeEvent()
    {
        _event = new Handler(onYearChanged);
    }

    // default handler
    private void onYearChanged(int year)
    {
        Log.Debug(string.Format("onYearChanged; {0}", year));
    }

    public void Invoke(int year) { _event.Invoke(year); }

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

public class MonthChangeEvent
{
    public delegate void Handler(int month);
    private event Handler _event;

    public MonthChangeEvent()
    {
        _event = new Handler(onMonthChanged);
    }

    // default handler
    private void onMonthChanged(int month)
    {
        Log.Debug(string.Format("onMonthChanged; {0}", month));
    }

    public void Invoke(int month) { _event.Invoke(month); }

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

public class DayChangeEvent
{
    public delegate void Handler(int day);
    private event Handler _event;

    public DayChangeEvent()
    {
        _event = new Handler(onDayChanged);
    }

    // default handler
    private void onDayChanged(int day)
    {
        Log.Debug(string.Format("onDayChanged; {0}", day));
    }

    public void Invoke(int day) { _event.Invoke(day); }

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


public class ScheduleChangeEvent
{
    public delegate void Handler(int scheduleIndex, int actionId);
    private event Handler _event;

    public ScheduleChangeEvent()
    {
        _event = new Handler(onScheduleChanged);
    }

    // default handler
    private void onScheduleChanged(int scheduleIndex, int actionId)
    {
        Log.Debug(string.Format("onScheduleChanged; scheduleIndex = {0}, actionId = {1}",
            scheduleIndex, actionId));
    }

    public void Invoke(int scheduleIndex, int actionId) { _event.Invoke(scheduleIndex, actionId); }

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


public class ScheduleStartEvent
{
    public delegate void Handler();
    private event Handler _event;

    public ScheduleStartEvent()
    {
        _event = new Handler(onScheduleStart);
    }

    // default handler
    private void onScheduleStart()
    {
        Log.Debug(string.Format("onScheduleStart;"));
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

public class ScheduleBeginEvent
{
    public delegate void Handler(int actionId);
    private event Handler _event;

    public ScheduleBeginEvent()
    {
        _event = new Handler(onScheduleBegin);
    }

    // default handler
    private void onScheduleBegin(int actionId)
    {
        Log.Debug(string.Format("onScheduleBegin; actionId({0})", actionId));
    }

    public void Invoke(int actionId) { _event.Invoke(actionId); }

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

public class ScheduleCancelEvent
{
    public delegate void Handler();
    private event Handler _event;

    public ScheduleCancelEvent()
    {
        _event = new Handler(onScheduleCancel);
    }

    // default handler
    private void onScheduleCancel()
    {
        Log.Debug(string.Format("onScheduleCancel;"));
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

public class ScheduleFirstEvent
{
    public delegate void Handler(int npcId);
    private event Handler _event;

    public ScheduleFirstEvent()
    {
        _event = new Handler(onScheduleFirst);
    }

    // default handler
    private void onScheduleFirst(int npcId)
    {
        Log.Debug(string.Format("onScheduleFirst; npcId = {0}", npcId));
    }

    public void Invoke(int npcId) { _event.Invoke(npcId); }

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

public class ScheduleDoEvent
{
    public delegate void Handler(bool isSuccessed);
    private event Handler _event;

    public ScheduleDoEvent()
    {
        _event = new Handler(onScheduleDo);
    }

    // default handler
    private void onScheduleDo(bool isSuccessed)
    {
        Log.Debug(string.Format("onScheduleDo;"));
    }

    public void Invoke(bool isSuccessed) { _event.Invoke(isSuccessed); }

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

public class ScheduleFinishEvent
{
    public delegate void Handler(int achievementRatio, int npcId, string msg);
    private event Handler _event;

    public ScheduleFinishEvent()
    {
        _event = new Handler(onScheduleFinish);
    }

    // default handler
    private void onScheduleFinish(int achievementRatio, int npcId, string msg)
    {
        Log.Debug(string.Format("onScheduleFinish;"));
    }

    public void Invoke(int achievementRatio, int npcId, string msg) { _event.Invoke(achievementRatio, npcId, msg); }

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

public class ScheduleEndEvent
{
    public delegate void Handler();
    private event Handler _event;

    public ScheduleEndEvent()
    {
        _event = new Handler(onScheduleEnd);
    }

    // default handler
    private void onScheduleEnd()
    {
        Log.Debug(string.Format("onScheduleEnd;"));
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


public class HeroineStatEvent
{
    public delegate void Handler(EStat statType, int value);
    private event Handler _event;

    public HeroineStatEvent()
    {
        _event = new Handler(onHeroineStatChanged);
    }

    // default handler
    private void onHeroineStatChanged(EStat statType, int value)
    {
        //Log.Debug(string.Format("onHeroineStatChanged; {0}", statType));
    }

    public void Invoke(EStat statType, int value) { _event.Invoke(statType, value); }

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
