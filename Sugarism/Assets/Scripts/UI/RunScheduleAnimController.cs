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

    //
    void OnEnable()
    {
        //SetTrigger(ETrigger.Finish);  // 제대로 안먹힌다..
        Log.Debug("RunScheduleAnimController.OnEnable");
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
    //private const string ANIM_PARAM_NAME = "stateNo";
    private void setTrigger(ETrigger triggerType)
    {
        _animator.ResetTrigger(_prevTrigger);

        string s = triggerType.ToString();
        _animator.SetTrigger(s);

        _prevTrigger = s;

        // int 변수 하나로 제어해도 disable 이 finish 보다 먼저 일어나는지..
        // 마지막 행동에 대해서만 finish 가 제대로 안 먹혀서
        // 다음에 다시 애니메이션 시작하려고 하면 마지막 행동 상태에서 여전히 남아있음..
        // 해결해야 하는 문제.. 고로 anim state 관리를 trigger 하든 int 하든 해결방법이 못됨.
        //int value = (int)triggerType;
        //_animator.SetInteger(ANIM_PARAM_NAME, value);
    }
}
