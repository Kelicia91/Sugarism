using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RunScheduleAnimController : MonoBehaviour
{
    // @note: window 'Animator' - Parameters 'Trigers'
    public enum ETrigger
    {
        DoLesson = 0,
        DoPartTime,

        DoWalking,
        DoFreedom,
        DoIdle,

        LessonSuccess,
        LessonFail,
        PartTimeSuccess,
        PartTimeFail,

        Finish,

        MAX
    }

    //
    private Animator _animator;


    //
    void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
    public void SetTrigger(ETrigger triggerType)
    {
        if (ETrigger.MAX == triggerType)
        {
            Log.Error("invalid trigger type");
            return;
        }

        setTrigger(triggerType);
    }

    private string _prevTrigger = string.Empty;

    private void setTrigger(ETrigger triggerType)
    {
        // @note : id is HASH......
        //int id = (int)triggerType;
        //_animator.SetTrigger(id);

        _animator.ResetTrigger(_prevTrigger);

        string s = triggerType.ToString();
        _animator.SetTrigger(s);

        _prevTrigger = s;
    }
}
