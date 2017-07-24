using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RunButton : MonoBehaviour
{
    /********* Editor Interface *********/
    // prefabs
    public Text Text;

    //
    private Button _btn = null;

    //
	void Awake()
    {
        _btn = GetComponent<Button>();
        if (null != _btn)
        {
            _btn.onClick.AddListener(onClick);
        }
        else
        {
            Log.Error("not found run button");
        }

        setText(Def.CMD_RUN_SCHEDULE_NAME);
        Manager.Instance.Object.NurtureMode.Schedule.InsertEvent.Attach(onScheduleInserted);
	}

    // call before strat()
    void OnEnable()
    {
        setInteractable();
    }

    private void setText(string s)
    {
        if (null == Text)
        {
            Log.Error("not found text");
            return;
        }

        Text.text = s;
    }

    private void onClick()
    {
        // @todo : 팝업(이대로 진행할까요? y/n)
        // common - 팝업 hide
        // n - 
        // y - 스케줄패널 hide, 런스케줄패널 show

        //
        Manager.Instance.Object.NurtureMode.Schedule.Start();
    }

    private void onScheduleInserted(int scheduleIndex, int actionId)
    {
        setInteractable();
    }

    private void setInteractable()
    {
        if (Manager.Instance.Object.NurtureMode.Schedule.IsFull())
            _btn.interactable = true;
        else
            _btn.interactable = false;
    }
}
