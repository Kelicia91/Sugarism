﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RunSchedulePanel : Panel
{
    /********* Editor Interface *********/
    // prefabs
    public Image ActionIconImage;
    public Text ActionNameText;
    public Text ProgressDescriptionText;

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
        _schedule.ActionEndEvent.Attach(onActionEnd);
        _schedule.ActionEndEvent.Attach(onActionEndAchievement);
    }

    void OnEnable()
    {
        setBackgroundImage(null);

        ActionIconImage.enabled = false;
        setActionNameText(string.Empty);
        setProgressDescriptionText(string.Empty);

        int statPanelListCount = _statPanelList.Count;
        for (int i = 0; i < statPanelListCount; i++)
            _statPanelList[i].Hide();

        DialoguePanel.Hide();
        MessagePanel.Hide();
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

    private void setActionNameText(string s)
    {
        if (null == ActionNameText)
        {
            Log.Error("not found action name text");
            return;
        }

        ActionNameText.text = s;
    }

    private void setProgressDescriptionText(string s)
    {
        if (null == ProgressDescriptionText)
        {
            Log.Error("not found progress description text");
            return;
        }

        ProgressDescriptionText.text = s;
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

        int midAge = (Def.INIT_AGE + (Def.INIT_AGE + Def.PERIOD_YEAR)) / 2;
        if (Manager.Instance.Object.MainCharacter.Age >= midAge)
            return vacation.adultHood;
        else
            return vacation.childHood;
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

        ActionIconImage.sprite = action.icon;
        ActionIconImage.enabled = true;
        setActionNameText(action.name);
        setProgressDescriptionText(string.Empty);

        initStat(action);

        _animController.ResetTrigger(RunScheduleAnimController.ETrigger.END);
    }

    /*** ACTION START ***/
    private void onActionStart(int actionId)
    {
        start(actionId);

        Action action = Manager.Instance.DTAction[_actionId];
        
        string msg = string.Format(Def.ACTION_BEGIN_DESC_FORMAT, action.beginDesc);
        MessagePanel.Show(msg, onClickActionStartConfirm);
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
        NPC npc = Manager.Instance.DTNPC[npcId];
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
        setProgressDescriptionText(description);

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

    /*** ACTION END ***/
    private void onActionEnd()
    {
        endAnim();

        Invoke(RESUME_METHOD_NAME, DEFAULT_DELAY_SECONDS);
    }

    private void onActionEndAchievement(int achievementRatio, int npcId, string msg)
    {
        endAnim();

        NPC npc = Manager.Instance.DTNPC[npcId];

        string lines = null;
        if (achievementRatio <= 0)
        {
            lines = string.Format("{0}\n({1})", npc.angryDesc, msg);
        }
        else if (achievementRatio < 100)
        {
            lines = npc.doneDesc;
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
}