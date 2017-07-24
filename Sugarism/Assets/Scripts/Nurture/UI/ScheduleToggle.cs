using UnityEngine;
using UnityEngine.UI;


public class ScheduleToggle : MonoBehaviour
{
    /********* Editor Interface *********/
    // prefabs
    public Text OrderText;
    public ActionPanel ActionPanel;

    //
    private Toggle _toggle;
    
    //
    private int _scheduleIndex = -1;
    public int ScheduleIndex
    {
        get { return _scheduleIndex; }
        set
        {
            _scheduleIndex = value;
            setOrderText(_scheduleIndex);
        }
    }

    //
    private Nurture.Schedule _schedule = null;


    //
    void Awake()
    {
        _toggle = GetComponent<Toggle>();
        if (null == _toggle)
        {
            Log.Error("not found toggle");
            return;
        }

        _toggle.onValueChanged.AddListener(onValueChanged);
        
        _schedule = Manager.Instance.Object.NurtureMode.Schedule;
        _schedule.InsertEvent.Attach(onScheduleInserted);
    }

    void Start()
    {
        // action panel
        int actionId = _schedule.GetActionId(ScheduleIndex);
        set(actionId);

        // toggle
        if (ScheduleIndex == Manager.Instance.UI.SchedulePanel.SelectedScheduleIndex)
            _toggle.isOn = true;
        else
            _toggle.isOn = false;
    }

    // called after OnEnable(), before Start()
    public void Set(ToggleGroup toggleGroup)
    {
        _toggle.group = toggleGroup;
    }

	private void setOrderText(int index)
    {
        if (null == OrderText)
        {
            Log.Error("not found order text");
            return;
        }

        int order = index + 1;
        OrderText.text = order.ToString();
    }
    
    private void onValueChanged(bool isOn)
    {
        if (false == isOn)
            return;

        Manager.Instance.UI.SchedulePanel.SelectedScheduleIndex = ScheduleIndex;
    }
    
    private void onScheduleInserted(int scheduleIndex, int actionId)
    {
        if (scheduleIndex == ScheduleIndex)
            set(actionId);

        int nextIndex = getNextScheduleIndex(scheduleIndex);
        if (nextIndex == ScheduleIndex)
            _toggle.isOn = true;
        else
            _toggle.isOn = false;
    }

    private int getNextScheduleIndex(int selectedScheduleIndex)
    {
        int min = 0;
        int max = Def.MAX_NUM_ACTION_IN_MONTH - 1;

        // circular
        if (selectedScheduleIndex < max)
            return (selectedScheduleIndex + 1);
        else
            return min;
    }

    private void set(int actionId)
    {
        if (false == ExtAction.isValid(actionId))
        {
            ActionPanel.Hide();
            return;
        }
        
        ActionPanel.SetActionId(actionId);
        ActionPanel.Show();
    }
}
