
public class CmdLinesEvent
{
    public delegate void Handler(int characterId, bool isAnonymous, 
                                string lines, Sugarism.ELinesEffect linesEffect);
    private event Handler _event;

    public CmdLinesEvent()
    {
        _event = new Handler(onCmdLines);
    }

    // default handler
    private void onCmdLines(int characterId, bool isAnonymous,
                                string lines, Sugarism.ELinesEffect linesEffect)
    {
        Log.Debug(string.Format(
            "onCmdLines; characterId({0}), isAnonymous({1}), linesEffect({2}), lines: \"{3}\"",
            characterId, isAnonymous, linesEffect, lines));
    }

    public void Invoke(int characterId, bool isAnonymous,
                        string lines, Sugarism.ELinesEffect linesEffect)
    {
        _event.Invoke(characterId, isAnonymous, lines, linesEffect);
    }

    // @warn : attach order
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


public class CmdAppearEvent
{
    public delegate void Handler(int characterId, Sugarism.EFace face
                            , Sugarism.ECostume costume, Sugarism.EPosition position);
    private event Handler _event;

    public CmdAppearEvent()
    {
        _event = new Handler(onCmdAppear);
    }

    // default handler
    private void onCmdAppear(int characterId, Sugarism.EFace face
                            , Sugarism.ECostume costume, Sugarism.EPosition position)
    {
        Log.Debug(string.Format("onCmdAppear; characterId({0}, face({1}), costume({2}), pos({3})",
                    characterId, face, costume, position));
    }

    public void Invoke(int characterId, Sugarism.EFace face
                            , Sugarism.ECostume costume, Sugarism.EPosition position)
    {
        _event.Invoke(characterId, face, costume, position);
    }

    // @warn : attach order
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


public class CmdSwitchEvent
{
    public delegate void Handler(CmdCase[] caseArray);
    private event Handler _event;

    public CmdSwitchEvent()
    {
        _event = new Handler(onCmdSwitch);
    }

    // default handler
    private void onCmdSwitch(CmdCase[] caseArray)
    {
        int numOfCase = -1;
        if (null == caseArray)
            Log.Error("onCmdSwitch; caseArray is null");
        else if (caseArray.Length <= 0)
            numOfCase = 0;
        else
            numOfCase = caseArray.Length;

        Log.Debug(string.Format("onCmdSwitch; num of case ({0})", numOfCase));
    }

    public void Invoke(CmdCase[] caseArray) { _event.Invoke(caseArray); }

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


public class CmdTextEvent
{
    public delegate void Handler(string text);
    private event Handler _event;

    public CmdTextEvent()
    {
        _event = new Handler(onCmdText);
    }

    // default handler
    private void onCmdText(string text)
    {
        Log.Debug(string.Format("onCmdText; {0}", text));
    }

    public void Invoke(string text) { _event.Invoke(text); }

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


public class CmdFilterEvent
{
    public delegate void Handler(Sugarism.EFilter filter);
    private event Handler _event;

    public CmdFilterEvent()
    {
        _event = new Handler(onCmdFilter);
    }

    // default handler
    private void onCmdFilter(Sugarism.EFilter filter)
    {
        Log.Debug(string.Format("onCmdFilter; {0}", filter));
    }

    public void Invoke(Sugarism.EFilter filter) { _event.Invoke(filter); }

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


public class CmdFeelingEvent
{
    public delegate void Handler(int characterId, Sugarism.EOperation op, int value);
    private event Handler _event;

    public CmdFeelingEvent()
    {
        _event = new Handler(onCmdFeeling);
    }

    // default handler
    private void onCmdFeeling(int characterId, Sugarism.EOperation op, int value)
    {
        Log.Debug(string.Format("onCmdFeeling; characterId({0}), op({1}), value({2})",
                characterId, op, value));
    }

    public void Invoke(int characterId, Sugarism.EOperation op, int value) { _event.Invoke(characterId, op, value); }

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


public class ScenarioStartEvent
{
    public delegate void Handler();
    private event Handler _event;

    public ScenarioStartEvent()
    {
        _event = new Handler(onScenarioStart);
    }

    // default handler
    private void onScenarioStart()
    {
        Log.Debug("onScenarioStart");
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


public class ScenarioEndEvent
{
    public delegate void Handler();
    private event Handler _event;

    public ScenarioEndEvent()
    {
        _event = new Handler(onScenarioEnd);
    }

    // default handler
    private void onScenarioEnd()
    {
        Log.Debug("onScenarioEnd");
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
        Log.Debug(string.Format("onScheduleFirst; npcId({0})", npcId));
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
        Log.Debug(string.Format("onScheduleDo; isSuccessed({0})", isSuccessed));
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
        Log.Debug(string.Format("onScheduleFinish;, achiveRatio({0}), npcId({1}), msg({2})",
                achievementRatio, npcId, msg));
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


public class MainCharacterStatEvent
{
    public delegate void Handler(EStat statType, int value);
    private event Handler _event;

    public MainCharacterStatEvent()
    {
        _event = new Handler(onMainCharacterStatChanged);
    }

    // default handler
    private void onMainCharacterStatChanged(EStat statType, int value)
    {
        Log.Debug(string.Format("onMainCharacterStatChanged; {0}: {1}", statType, value));
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
