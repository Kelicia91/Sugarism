using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RunSchedulePanel : Panel
{
    /********* Editor Interface *********/
    // prefabs
    public Image ActionImage;
    public Text ActionNameText;
    public Text ProgressDescriptionText;

    public Image AnimImage;

    public GameObject StatsPanel;
    public GameObject PrefStatPanel;

    public NPCNotifyPanel NPCPanel;
    public MessagePanel MessagePanel;


    //
    private Image _backgroundImage;
    private RunScheduleAnimController _animController;

    private const int DEFAULT_NUM_STAT_PANEL = 2;
    private List<StatPanel> _statPanelList = null;
    private int _actionId = -1;


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

        Manager.Instance.ScheduleBeginEvent.Attach(onScheduleBegin);
        Manager.Instance.ScheduleCancelEvent.Attach(onScheduleCancel);
        Manager.Instance.ScheduleFirstEvent.Attach(onScheduleFirst);
        Manager.Instance.ScheduleDoEvent.Attach(onScheduleDo);
        Manager.Instance.ScheduleFinishEvent.Attach(onScheduleFinish);
    }

    void OnEnable()
    {
        setBackgroundImage(null);

        ActionImage.enabled = false;
        setActionNameText(string.Empty);
        setProgressDescriptionText(string.Empty);

        AnimImage.gameObject.SetActive(false);

        int statPanelListCount = _statPanelList.Count;
        for (int i = 0; i < statPanelListCount; i++)
            _statPanelList[i].Hide();

        NPCPanel.Hide();
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
    
    private void begin(int actionId)
    {
        _actionId = actionId;
        Action action = Manager.Instance.DTAction[_actionId];

        Sprite sprite = null;
        if (Def.ACTION_VACATION_ID == _actionId)
            sprite = getVactionImage();
        else
            sprite = action.background;

        setBackgroundImage(sprite);

        ActionImage.sprite = action.icon;
        ActionImage.enabled = true;
        setActionNameText(action.name);
        setProgressDescriptionText(string.Empty);
        
        initStat(action);
    }


    private void onScheduleBegin(int actionId)
    {
        begin(actionId);

        Action action = Manager.Instance.DTAction[actionId];
        // @todo :일정 시간후 자동 hide 또는 입력받으면 hide
        string msg = string.Format(Def.ACTION_BEGIN_DESC_FORMAT, action.beginDesc);
        MessagePanel.Show(msg, onDisableScheduleBeginConfirm);
    }

    private void onDisableScheduleBeginConfirm()
    {
        Manager.Instance.Object.Schedule.CanBeCanceled();
    }


    private void onScheduleCancel()
    {
        AnimImage.gameObject.SetActive(false);

        MessagePanel.Show(Def.ALARM_LACK_MONEY_DESC, onClickScheduleCancelConfirm);
    }

    private void onClickScheduleCancelConfirm()
    {
        Manager.Instance.Object.Schedule.Begin();
    }

    
    private void onScheduleFirst(int npcId)
    {
        NPC npc = Manager.Instance.DTNPC[npcId];
        string lines = npc.firstMeetDesc;

        NPCPanel.Show(npcId, lines, onClickNpcFirstMeet);
    }

    private void onClickNpcFirstMeet()
    {
        Manager.Instance.Object.Schedule.Do();
    }


    
    private void onScheduleDo(bool isSuccessed)
    {
        if (Def.ACTION_VACATION_ID == _actionId)
        {
            doVacation();
            return;
        }

        // Awake 에서 초기화 해주기 위함
        _animController.gameObject.SetActive(true);
        
        Action action = Manager.Instance.DTAction[_actionId];
        switch (action.type)
        {
            case EActionType.PARTTIME:
                doPartTime(action, isSuccessed);
                break;

            case EActionType.LESSON:
                doLesson(action, isSuccessed);
                break;

            case EActionType.RELAX:
                doRelax(action);
                break;

            case EActionType.IDLE:
                doIdle(action);
                break;

            default:
                break;
        }
    }

    private void doPartTime(Action action, bool isSuccessed)
    {
        string description;
        RunScheduleAnimController.ETrigger resultAnimTrigger;
        if (isSuccessed)
        {
            description = Def.ACTION_PARTTIME_DOING_SUCCESS_DESC;
            resultAnimTrigger = action.successAnim;
        }
        else
        {
            description = Def.ACTION_PARTTIME_DOING_FAIL_DESC;
            resultAnimTrigger = action.failAnim;
        }

        setProgressDescriptionText(description);
        StartCoroutine(animate(isSuccessed, action.normalAnim, resultAnimTrigger));
    }

    private void doLesson(Action action, bool isSuccessed)
    {
        string description;
        RunScheduleAnimController.ETrigger resultAnimTrigger;
        if (isSuccessed)
        {
            description = Def.ACTION_LESSON_DOING_SUCCESS_DESC;
            resultAnimTrigger = action.successAnim;
        }
        else
        {
            description = Def.ACTION_PARTTIME_DOING_FAIL_DESC;
            resultAnimTrigger = action.failAnim;
        }

        setProgressDescriptionText(description);
        StartCoroutine(animate(isSuccessed, action.normalAnim, resultAnimTrigger));
    }

    private IEnumerator animate(bool isSuccessed
        , RunScheduleAnimController.ETrigger normal
        , RunScheduleAnimController.ETrigger result)
    {
        float waitSeconds = Configuration.DAY_RUNNING_TIME_SECONDS * 0.4f;

        while (true)
        {
            _animController.SetTrigger(normal);
            yield return new WaitForSeconds(waitSeconds);
            break;
        }

        _animController.SetTrigger(result);
    }

    private void doRelax(Action action)
    {
        _animController.SetTrigger(action.normalAnim);
    }

    private void doIdle(Action action)
    {
        _animController.SetTrigger(action.normalAnim);
    }

    private void doVacation()
    {
        // do nothing..
    }

    private Sprite getVactionImage()
    {
        ESeason season = Manager.Instance.Object.Calendar.Get();
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



    private void onScheduleFinish(int achievementRatio, int npcId, string msg)
    {
        _animController.SetTrigger(RunScheduleAnimController.ETrigger.Finish);
        //AnimImage.gameObject.SetActive(false);

        if (false == ExtNPC.IsValid(npcId))
        {
            finish();
            return;
        }

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

        NPCPanel.Show(npcId, lines, onClickNpcFinish);
    }

    private void onClickNpcFinish()
    {
        finish();
    }

    private void finish()
    {
        Manager.Instance.Object.Schedule.NextDay();
    }
}
