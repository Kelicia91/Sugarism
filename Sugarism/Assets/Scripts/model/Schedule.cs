using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public partial class Schedule : MonoBehaviour
{
    private int NUM_OF_ACTION;
    
    private Calendar _calendar = null;
    private MainCharacter _mainCharacter = null;
    private int[] _actionArray = null;

    private IdleAction _idleAction = null;


    // Must call once.
    public void Constrution(int numOfAction, Calendar calendar, MainCharacter mainCharacter)
    {
        NUM_OF_ACTION = numOfAction;

        _calendar = calendar;
        _mainCharacter = mainCharacter;
        _actionArray = new int[NUM_OF_ACTION];

        _idleAction = new IdleAction(Def.ACTION_IDLE_ID, _mainCharacter);

        Initialize();
    }

    public void Initialize()
    {
        for (int i = 0; i < NUM_OF_ACTION; ++i)
        {
            _actionArray[i] = -1;
        }
    }

    public int GetActionId(int index)
    {
        if (false == isValid(index))
            return -1;
        else
            return _actionArray[index];
    }

    public bool IsFull()
    {
        for (int i = 0; i < NUM_OF_ACTION; ++i)
        {
            if (false == ExtAction.isValid(_actionArray[i]))
                return false;
        }

        return true;
    }

    public void Insert(int index, int actionId)
    {
        if (false == isValid(index))
            return;

        if (false == ExtAction.isValid(actionId))
            return;

        insert(index, actionId);
    }

    private void insert(int index, int actionId)
    {
        _actionArray[index] = actionId;

        Manager.Instance.ScheduleChangeEvent.Invoke(index, _actionArray[index]);
    }

    private bool isValid(int index)
    {
        if (index < 0)
            return false;
        else if (index >= NUM_OF_ACTION)
            return false;
        else
            return true;
    }    
}


public partial class Schedule
{
    /* SCHEDULEING
     * : Act Unit = Schedule = 1 Month
     */
     
    int _month = -1;
    int _day = -1;

    int[] _beginDayArray = null;
    int[] _endDayArray = null;
    ExtAction[] _extActionArray = null;

    int _actionOrder = -1;
    

    private void initScheduling()
    {
        _month = _calendar.Month;
        _day = _calendar.Day;

        _beginDayArray = getBeginDayOfActon(_month, NUM_OF_ACTION);
        _endDayArray = getEndDayOfAction(_month, NUM_OF_ACTION);
        _extActionArray = generate();

        _actionOrder = 0;
    }

    public void NextDay()
    {
        ++_day;
        _calendar.Day = _day;

        scheduling();
    }

    public void Run()
    {
        Log.Debug(Def.START_SCHEDULE_DESC);

        initScheduling();

        scheduling();
        //StartCoroutine(scheduling(Configuration.DAY_RUNNING_TIME_SECONDS));
    }

    public void Begin()
    {
        begin();
    }

    private void begin()
    {
        ExtAction extAction = _extActionArray[_actionOrder];
        extAction.Begin();
    }

    private void first()
    {
        ExtAction extAction = _extActionArray[_actionOrder];

        if (_mainCharacter.GetActCount(extAction.Id) <= 0)
            extAction.First();
        else
            Do();
    }

    public void Do()
    {
        StartCoroutine(doing(Configuration.DAY_RUNNING_TIME_SECONDS));
    }

    private IEnumerator doing(float waitSeconds)
    {        
        while (true)
        {
            ExtAction extAction = _extActionArray[_actionOrder];
            extAction.Do();

            yield return new WaitForSeconds(waitSeconds);
            break;
        }

        if (_day == _endDayArray[_actionOrder])
            finish();
        else
            NextDay();
    }

    private void finish()
    {
        int doneActionIndex = _actionOrder;
        ++_actionOrder;

        ExtAction extAction = _extActionArray[doneActionIndex];
        extAction.Finish();
    }


    private void scheduling()
    {
        if (_day > Calendar.LastDay[_month])
        {
            Initialize();
            Manager.Instance.ScheduleEndEvent.Invoke();
            return;
        }
        
        if (_day == _beginDayArray[_actionOrder])
        {
            begin();
            return;
        }

        CanBeCanceled();
    }

    public void CanBeCanceled()
    {
        ExtAction extAction = _extActionArray[_actionOrder];

        if (isLackMoney(extAction.Id))
        {
            _extActionArray[_actionOrder] = _idleAction;
            Manager.Instance.ScheduleCancelEvent.Invoke();
        }
        else
        {
            first();
        }        
    }

    //private IEnumerator scheduling(float waitSeconds)
    //{
    //    int MONTH = _calendar.Month;

    //    int[] BEGINDAY_ACT = getBeginDayOfActon(MONTH, NUM_OF_ACTION);
    //    int[] ENDDAY_ACT = getEndDayOfAction(MONTH, NUM_OF_ACTION);

    //    ExtAction[] extActionArray = generate();

    //    int actionOrder = 0;
    //    int day = _calendar.Day;

    //    bool isAlarmLackMoney = false;

    //    while (day <= Calendar.LastDay[MONTH])
    //    {
    //        ExtAction extAction = extActionArray[actionOrder];

    //        if (isLackMoney(extAction.Id))
    //        {
    //            extAction = _idleAction;
    //            if (false == isAlarmLackMoney)
    //            {
    //                isAlarmLackMoney = true;
    //                Log.Debug(Def.ALARM_LACK_MONEY_DESC);
    //            }
    //        }

    //        if (day == BEGINDAY_ACT[actionOrder])
    //            extAction.Begin();

    //        if (_mainCharacter.GetActCount(extAction.Id) <= 0)
    //            extAction.First();

    //        extAction.Do();

    //        if (day == ENDDAY_ACT[actionOrder])
    //        {
    //            extAction.Done();
    //            ++actionOrder;
    //            isAlarmLackMoney = false;
    //        }

    //        ++day;
    //        _calendar.Day = day;

    //        yield return new WaitForSeconds(waitSeconds);
    //    }

    //    Manager.Instance.ScheduleDoneEvent.Invoke();
    //    Initialize();
    //}

    private bool isLackMoney(int actionId)
    {
        Action action = Manager.Instance.DTAction[actionId];

        // @note : action.money can be < 0.
        int sum = _mainCharacter.Money + action.money;
        if (sum < 0)
            return true;
        else
            return false;
    }

    private ExtAction[] generate()
    {
        ExtAction[] extActionArray = new ExtAction[NUM_OF_ACTION];

        for (int i = 0; i < NUM_OF_ACTION; ++i)
        {
            int actionId = _actionArray[i];
            if (Def.ACTION_VACATION_ID == actionId)
            {
                extActionArray[i] = new VacationAction(actionId, _mainCharacter);
                continue;
            }

            Action action = Manager.Instance.DTAction[actionId];
            switch (action.type)
            {
                case EActionType.PARTTIME:
                    extActionArray[i] = new PartTimeAction(actionId, _mainCharacter);
                    break;

                case EActionType.LESSON:
                    extActionArray[i] = new LessonAction(actionId, _mainCharacter);
                    break;

                case EActionType.RELAX:
                    extActionArray[i] = new RelaxAction(actionId, _mainCharacter);
                    break;

                default:
                    extActionArray[i] = null;
                    break;
            }
        }

        return extActionArray;
    }

    private int[] getBeginDayOfActon(int month, int numOfAction)
    {
        int actionPeriod = Calendar.LastDay[month] / numOfAction;

        int[] beginDayOfAction = new int[numOfAction];

        int numBeginDayOfAction = beginDayOfAction.Length;
        for (int i = 0; i < numBeginDayOfAction; ++i)
        {
            beginDayOfAction[i] = actionPeriod * i + 1;
        }

        return beginDayOfAction;
    }

    private int[] getEndDayOfAction(int month, int numOfAction)
    {
        int actionPeriod = Calendar.LastDay[month] / numOfAction;

        int[] endDayOfAction = new int[numOfAction];

        int numEndDayOfAction = endDayOfAction.Length;
        for (int i = 0; i < numEndDayOfAction; ++i)
        {
            endDayOfAction[i] = actionPeriod * (i + 1);
        }

        endDayOfAction[(numOfAction - 1)] = Calendar.LastDay[month];

        return endDayOfAction;
    }
}