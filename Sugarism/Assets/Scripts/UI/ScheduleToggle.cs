using System.Collections;
using System.Collections.Generic;
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
            onEnable();
            setOrderText(_scheduleIndex);
        }
    }
    
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
        Manager.Instance.Object.NurtureMode.Schedule.InsertEvent.Attach(onScheduleInserted);
    }

    // This function is called when the object becomes enabled and active.
    void OnEnable()
    {
        onEnable();

        // action panel
        int actionId = Manager.Instance.Object.NurtureMode.Schedule.GetActionId(ScheduleIndex);
        set(actionId);   
    }

    // called after OnEnable()
    public void Set(ToggleGroup toggleGroup)
    {
        _toggle.group = toggleGroup;
    }

    private void onEnable()
    {
        if (0 == ScheduleIndex)
            _toggle.isOn = true;
        else
            _toggle.isOn = false;
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
        if (ScheduleIndex == scheduleIndex)
            set(actionId);

        int nextIndex = getNextScheduleIndex(scheduleIndex);
        if (ScheduleIndex == nextIndex)
            _toggle.isOn = true;
        else
            _toggle.isOn = false;
    }

    private int getNextScheduleIndex(int selectedScheduleIndex)
    {
        int max = Def.MAX_NUM_ACTION_IN_MONTH - 1;

        if (selectedScheduleIndex < max)
            return (selectedScheduleIndex + 1);
        else
            return max;
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
