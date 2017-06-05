﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Singleton 남발 방지용.
// @IMPORTANT : MonoBehaviour를 상속받은 이상 생성자 재정의 금지!!
public class Manager : MonoBehaviour
{
    private static Manager _instance = null;
    public static Manager Instance { get { return _instance; } }
    
    void Awake()
    {
        _instance = this;

        // custom event
        _cmdLinesEvent = new CmdLinesEvent();

        _moneyChangeEvent = new MoneyChangeEvent();
        _yearChangeEvent = new YearChangeEvent();
        _monthChangeEvent = new MonthChangeEvent();
        _dayChangeEvent = new DayChangeEvent();

        _scheduleChangeEvent = new ScheduleChangeEvent();
        _scheduleStartEvent = new ScheduleStartEvent();
        _scheduleBeginEvent = new ScheduleBeginEvent();
        _scheduleCancelEvent = new ScheduleCancelEvent();
        _scheduleFirstEvent = new ScheduleFirstEvent();
        _scheduleDoEvent = new ScheduleDoEvent();
        _scheduleFinishEvent = new ScheduleFinishEvent();
        _scheduleEndEvent = new ScheduleEndEvent();

        _mainCharacterStatEvent = new MainCharacterStatEvent();

        // manager
        _object = Instantiate(PrefObjectManager);
        _ui = Instantiate(PrefUIManager);
    }


    /********* Editor Interface *********/
    // Prefabs
    public ObjectManager PrefObjectManager;
    public UIManager PrefUIManager;

    // KYI : DataTable assets
    public ConstitutionObject AsstDTConstitution;
    public ZodiacObject AsstDTZodiac;
    public StatObject AsstDTStat;
    public ActionObject AsstDTAction;
    public ActionTypeObject AsstDTActionType;
    public VacationObject AsstDTVacation;
    public NPCObject AsstDTNPC;
    public TargetObject AsstDTTarget;


    /********* Game Interface *********/
    // Manager
    private ObjectManager _object = null;
    public ObjectManager Object { get { return _object; } }

    private UIManager _ui = null;
    public UIManager UI { get { return _ui; } }

    // CustomEvent
    private CmdLinesEvent _cmdLinesEvent = null;
    public CmdLinesEvent CmdLinesEvent { get { return _cmdLinesEvent; } }

    private MoneyChangeEvent _moneyChangeEvent = null;
    public MoneyChangeEvent MoneyChangeEvent { get { return _moneyChangeEvent; } }

    private YearChangeEvent _yearChangeEvent = null;
    public YearChangeEvent YearChangeEvent { get { return _yearChangeEvent; } }

    private MonthChangeEvent _monthChangeEvent = null;
    public MonthChangeEvent MonthChangeEvent { get { return _monthChangeEvent; } }

    private DayChangeEvent _dayChangeEvent = null;
    public DayChangeEvent DayChangeEvent { get { return _dayChangeEvent; } }

    private ScheduleChangeEvent _scheduleChangeEvent = null;
    public ScheduleChangeEvent ScheduleChangeEvent { get { return _scheduleChangeEvent; } }

    private ScheduleStartEvent _scheduleStartEvent = null;
    public ScheduleStartEvent ScheduleStartEvent { get { return _scheduleStartEvent; } }

    private ScheduleBeginEvent _scheduleBeginEvent = null;
    public ScheduleBeginEvent ScheduleBeginEvent { get { return _scheduleBeginEvent; } }

    private ScheduleCancelEvent _scheduleCancelEvent = null;
    public ScheduleCancelEvent ScheduleCancelEvent { get { return _scheduleCancelEvent; } }

    private ScheduleFirstEvent _scheduleFirstEvent = null;
    public ScheduleFirstEvent ScheduleFirstEvent { get { return _scheduleFirstEvent; } }

    private ScheduleDoEvent _scheduleDoEvent = null;
    public ScheduleDoEvent ScheduleDoEvent { get { return _scheduleDoEvent; } }

    private ScheduleFinishEvent _scheduleFinishEvent = null;
    public ScheduleFinishEvent ScheduleFinishEvent { get { return _scheduleFinishEvent; } }

    private ScheduleEndEvent _scheduleEndEvent = null;
    public ScheduleEndEvent ScheduleEndEvent { get { return _scheduleEndEvent; } }

    private MainCharacterStatEvent _mainCharacterStatEvent = null;
    public MainCharacterStatEvent MainCharacterStatEvent { get { return _mainCharacterStatEvent; } }

    // KYI : DataTable
    public ConstitutionObject DTConstitution { get { return AsstDTConstitution; } }
    public ZodiacObject DTZodiac { get { return AsstDTZodiac; } }
    public StatObject DTStat { get { return AsstDTStat; } }
    public ActionObject DTAction { get { return AsstDTAction; } }
    public ActionTypeObject DTActionType { get { return AsstDTActionType; } }
    public VacationObject DTVacation { get { return AsstDTVacation; } }
    public NPCObject DTNPC { get { return AsstDTNPC; } }
    public TargetObject DTTarget { get { return AsstDTTarget; } }
}
 
 