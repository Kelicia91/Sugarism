using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RunSchedulePanel : Panel
{
    /********* Editor Interface *********/
    // prefabs
    public GameObject PrefCalendarPanel;
    public ScheduleProgressPanel ProgressPanel;

    public Image AnimImage;

    public GameObject StatsPanel;
    public GameObject PrefStatPanel;

    public ActionDialoguePanel DialoguePanel;
    public MessagePanel MessagePanel;


    //
    private Image _backgroundImage = null;
    private RunScheduleAnimController _animController = null;

    private const int DEFAULT_NUM_STAT_PANEL = 2;
    private List<StatPanel> _statPanelList = null;
    private int _actionId = -1;

    //
    Nurture.Schedule _schedule = null;

    void Awake()
    {
        if (null != PrefCalendarPanel)
        {
            GameObject o = Instantiate(PrefCalendarPanel);
            o.transform.SetParent(transform, false);
            o.transform.SetAsFirstSibling();
        }
        else
        {
            Log.Error("not found prefab calendar panel");
        }

        _statPanelList = new List<StatPanel>();
        for (int i = 0; i < DEFAULT_NUM_STAT_PANEL; ++i)
        {
            GameObject o = Instantiate(PrefStatPanel);
            o.transform.SetParent(StatsPanel.transform, false);

            StatPanel p = o.GetComponent<StatPanel>();
            _statPanelList.Add(p);
        }

        _backgroundImage = GetComponent<Image>();
        _animController = AnimImage.GetComponent<RunScheduleAnimController>();

        _schedule = Manager.Instance.Object.NurtureMode.Schedule;
        _schedule.ActionCancelEvent.Attach(onActionCancel);
        _schedule.ActionStartEvent.Attach(onActionStart);
        _schedule.ActionFirstEvent.Attach(onActionFirst);
        _schedule.ActionFirstEvent.Attach(onActionFirstNPC);
        _schedule.ActionDoEvent.Attach(onActionDo);
        _schedule.ActionDoEvent.Attach(onActionDoResult);
        _schedule.ActionBeforeEndEvent.Attach(onActionBeforeEnd);
        _schedule.ActionBeforeEndEvent.Attach(onActionBeforeEndExam);
        _schedule.ActionEndEvent.Attach(onActionEnd);
        _schedule.ActionEndEvent.Attach(onActionEndAchievement);

        Nurture.Mode nurtureMode = Manager.Instance.Object.NurtureMode;
        nurtureMode.Schedule.StartEvent.Attach(onScheduleStart);
        nurtureMode.Schedule.EndEvent.Attach(onScheduleEnd);
    }

    void OnEnable()
    {
        setBackgroundImage(null);

        int statPanelListCount = _statPanelList.Count;
        for (int i = 0; i < statPanelListCount; i++)
            _statPanelList[i].Hide();

        DialoguePanel.Hide();
        MessagePanel.Hide();
    }

    public override void Show()
    {
        CurrencyPanel c = Manager.Instance.UI.CurrencyPanel;
        c.ActPowePanel.Hide();
        c.GoldPanel.Hide();
        c.MoneyPanel.SetCharge(false);
        c.MoneyPanel.Show();

        base.Show();
    }

    private void setBackgroundImage(Sprite s)
    {
        if (null == _backgroundImage)
        {
            Log.Error("not found background image");
            return;
        }

        _backgroundImage.sprite = s;
    }
    
    private Sprite getVactionImage()
    {
        ESeason season = Manager.Instance.Object.NurtureMode.Calendar.Get();
        if (ESeason.MAX == season)
        {
            Log.Error("invalid season");
            return null;
        }

        int seasonId = (int)season;
        Vacation vacation = Manager.Instance.DTVacation[seasonId];
        
        if (Manager.Instance.Object.MainCharacter.IsChildHood())
            return vacation.childHood;
        else
            return vacation.adultHood;
    }

    private void initStat(Action action)
    {
        if (null == StatsPanel)
        {
            Log.Error("not found stats panel");
            return;
        }

        List<EStat> statList = new List<EStat>();

        if (action.stress != 0)
            statList.Add(EStat.STRESS);

        if (action.stamina != 0)
            statList.Add(EStat.STAMINA);
        if (action.intellect != 0)
            statList.Add(EStat.INTELLECT);
        if (action.grace != 0)
            statList.Add(EStat.GRACE);
        if (action.charm != 0)
            statList.Add(EStat.CHARM);

        if (action.attack != 0)
            statList.Add(EStat.ATTACK);
        if (action.defense != 0)
            statList.Add(EStat.DEFENSE);

        if (action.leadership != 0)
            statList.Add(EStat.LEADERSHIP);
        if (action.tactic != 0)
            statList.Add(EStat.TACTIC);

        if (action.morality != 0)
            statList.Add(EStat.MORALITY);
        if (action.goodness != 0)
            statList.Add(EStat.GOODNESS);

        if (action.sensibility != 0)
            statList.Add(EStat.SENSIBILITY);
        if (action.arts != 0)
            statList.Add(EStat.ARTS);

        int numMoreNeedStatPanel = statList.Count - _statPanelList.Count;
        if (numMoreNeedStatPanel > 0)
        {
            for (int i = 0; i < numMoreNeedStatPanel; ++i)
            {
                GameObject o = Instantiate(PrefStatPanel);
                o.transform.SetParent(StatsPanel.transform, false);

                StatPanel p = o.GetComponent<StatPanel>();
                _statPanelList.Add(p);
            }
        }

        int statListCount = statList.Count;
        int statPanelListCount = _statPanelList.Count;
        for (int i = 0; i < statListCount; ++i)
        {
            _statPanelList[i].Set(statList[i]);
            _statPanelList[i].Show();
        }
        for (int i = statList.Count; i < statPanelListCount; i++)
        {
            _statPanelList[i].Hide();   // hide unused panel
        }
    }


    private const float DEFAULT_DELAY_SECONDS = 0.1f;
    private const string RESUME_METHOD_NAME = "resume"; // for Invoke()
    private void resume()
    {
        _schedule.Iterate();
    }
    
    
    /*** ACTION START ***/
    private void onActionStart(int actionId)
    {
        start(actionId);

        Action action = Manager.Instance.DTAction[_actionId];
        
        string msg = string.Format(Def.ACTION_BEGIN_DESC_FORMAT, action.beginDesc);
        MessagePanel.Show(msg, onClickActionStartConfirm);
    }

    private void start(int actionId)
    {
        _actionId = actionId;
        Action action = Manager.Instance.DTAction[_actionId];

        Sprite sprite = null;
        if (Def.ACTION_VACATION_ID == _actionId)
            sprite = getVactionImage();
        else
            sprite = action.background;

        setBackgroundImage(sprite);

        ProgressPanel.OnActionStart(action.icon, action.name);

        initStat(action);

        _animController.ResetTrigger(RunScheduleAnimController.ETrigger.END);
    }

    private void onClickActionStartConfirm()
    {
        resume();
    }


    /*** ACTION CANCEL ***/
    private void onActionCancel()
    {
        endAnim();

        MessagePanel.Show(Def.ALARM_LACK_MONEY_DESC, onClickScheduleCancelConfirm);
    }

    private void onClickScheduleCancelConfirm()
    {
        resume();
    }
    

    /*** ACTION FIRST ***/
    private void onActionFirst()
    {
        Invoke(RESUME_METHOD_NAME, DEFAULT_DELAY_SECONDS);
    }

    private void onActionFirstNPC(int npcId)
    {
        ActionNPC npc = Manager.Instance.DTActionNPC[npcId];
        string lines = npc.firstMeetDesc;

        DialoguePanel.Show(npcId, lines, onClickNpcFirstMeet);
    }

    private void onClickNpcFirstMeet()
    {
        resume();
    }


    /*** ACTION DO ***/
    private void onActionDo()
    {
        Action action = Manager.Instance.DTAction[_actionId];

        if (RunScheduleAnimController.ETrigger.MAX == action.animTrigger)
        {
            // case. vacation
            Invoke(RESUME_METHOD_NAME, DEFAULT_DELAY_SECONDS);
        }
        else
        {
            _animController.SetTrigger(action.animTrigger);

            float animLength = _animController.GetCurrentStateLength(RunScheduleAnimController.DEFAULT_LAYER_INDEX);
            Invoke(RESUME_METHOD_NAME, animLength);
        }
    }

    private void onActionDoResult(bool isSuccessed)
    {
        Action action = Manager.Instance.DTAction[_actionId];

        //
        string description = getActionDoResultDescription(action.type, isSuccessed);
        ProgressPanel.SetProgressDescriptionText(description);

        //
        _animController.SetTrigger(action.animTrigger);

        string methodName = getAcionResultMethodName(isSuccessed);
        float animLength = _animController.GetCurrentStateLength(RunScheduleAnimController.DEFAULT_LAYER_INDEX);
        Invoke(methodName, animLength);
    }

    private const string ACTION_SUCCESS_METHOD_NAME = "actionSuccess";   // for Invoke()
    private void actionSuccess()
    {
        _animController.SetTrigger(RunScheduleAnimController.ETrigger.SUCCESS);

        float animLength = _animController.GetCurrentStateLength(RunScheduleAnimController.DEFAULT_LAYER_INDEX);
        Invoke(RESUME_METHOD_NAME, animLength);
    }

    private const string ACTION_FAIL_METHOD_NAME = "actionFail";  // for Invoke()
    private void actionFail()
    {
        _animController.SetTrigger(RunScheduleAnimController.ETrigger.FAIL);

        float animLength = _animController.GetCurrentStateLength(RunScheduleAnimController.DEFAULT_LAYER_INDEX);
        Invoke(RESUME_METHOD_NAME, animLength);
    }

    private string getAcionResultMethodName(bool isSuccessed)
    {
        if (isSuccessed)
            return ACTION_SUCCESS_METHOD_NAME;
        else
            return ACTION_FAIL_METHOD_NAME;
    }

    private string getActionDoResultDescription(EActionType actionType, bool isSuccessed)
    {
        switch (actionType)
        {
            case EActionType.PARTTIME:
                if (isSuccessed)
                    return Def.ACTION_PARTTIME_DOING_SUCCESS_DESC;
                else
                    return Def.ACTION_PARTTIME_DOING_FAIL_DESC;

            case EActionType.LESSON:
                if (isSuccessed)
                    return Def.ACTION_LESSON_DOING_SUCCESS_DESC;
                else
                    return Def.ACTION_LESSON_DOING_FAIL_DESC;

            default:
                return null;
        }
    }


    /*** ACTION BEFORE END ***/
    private void onActionBeforeEnd()
    {
        endAnim();
        ProgressPanel.HideActionProgressPanel();

        Invoke(RESUME_METHOD_NAME, DEFAULT_DELAY_SECONDS);
    }

    private Exam.Exam _exam = null;
    private void onActionBeforeEndExam(Exam.Exam exam)
    {
        endAnim();
        ProgressPanel.HideActionProgressPanel();

        _exam = exam;
        _exam.StartEvent.Attach(onExamStart);
        _exam.EndEvent.Attach(onExamEnd);
        _exam.DialogueEvent.Attach(onExamDialogue);
        _exam.DialogueEvent.Attach(onExamDialogueNPC);
        _exam.DialogueEvent.Attach(onExamDialogueRival);

        _exam.Start();
    }

    private void resumeExam()
    {
        if (null == _exam)
        {
            Log.Error("not found exam");
            return;
        }

        _exam.Iterate();
    }


    /*** ACTION.LESSON.EXAM ***/
    private void onExamStart()
    {
        // do something for ui
    }

    private void onExamEnd()
    {
        // do something for ui

        _exam.StartEvent.Detach(onExamStart);
        _exam.EndEvent.Detach(onExamEnd);
        _exam.DialogueEvent.Detach(onExamDialogue);
        _exam.DialogueEvent.Detach(onExamDialogueNPC);
        _exam.DialogueEvent.Detach(onExamDialogueRival);
        _exam = null;

        Invoke(RESUME_METHOD_NAME, DEFAULT_DELAY_SECONDS);
    }

    private void onExamDialogue(string lines)
    {
        DialoguePanel.Show(lines, resumeExam);
    }

    private void onExamDialogueNPC(int npcId, string lines)
    {
        DialoguePanel.Show(npcId, lines, resumeExam);
    }

    private void onExamDialogueRival(Rival rival, string lines)
    {
        DialoguePanel.Show(rival, lines, resumeExam);
    }


    /*** ACTION END ***/
    private void onActionEnd()
    {
        Invoke(RESUME_METHOD_NAME, DEFAULT_DELAY_SECONDS);
    }

    private void onActionEndAchievement(int achievementRatio, int npcId, string msg)
    {
        ActionNPC npc = Manager.Instance.DTActionNPC[npcId];

        string lines = null;
        if (achievementRatio <= 0)
        {
            lines = string.Format("{0}\n({1})", npc.angryDesc, msg);
        }
        else if (achievementRatio < 100)
        {
            lines = string.Format("{0}\n({1})", npc.doneDesc, msg);
        }
        else
        {
            lines = string.Format("{0}\n({1})", npc.bonusDesc, msg);
        }

        DialoguePanel.Show(npcId, lines, onClickNpcEnd);
    }

    private void onClickNpcEnd()
    {
        resume();
    }


    //
    private void endAnim()
    {
        Action action = Manager.Instance.DTAction[_actionId];
        _animController.ResetTrigger(action.animTrigger);

        _animController.SetTrigger(RunScheduleAnimController.ETrigger.END);
    }


    //
    private void onScheduleStart()
    {
        Show();
    }

    private void onScheduleEnd()
    {
        Hide();
        Manager.Instance.UI.MainPanel.Show();   // for recovering currency panel
    }
}
