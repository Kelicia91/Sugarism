using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RunScheduleAnimController : MonoBehaviour
{
    // @note: window 'Animator' - Parameters 'Triggers'
    public enum ETrigger
    {
        END = 0,

        SUCCESS = 1,
        FAIL = 2,

        IDLE,
        WALKING,
        FREEDOM,

        FARM,
        MARKET,
        RESIDENCE,
        PUB,

        ARENA,
        TACTIC,
        POLITICS,
        ARTS,

        // HERE add a new trigger.

        MAX
    }

    //
    public const int DEFAULT_LAYER_INDEX = 0;

    //
    private Animator _animator = null;

    //
    void Awake()
    {
        _animator = GetComponent<Animator>();
    }


    public void ResetTrigger(ETrigger triggerType)
    {
        Log.Debug(string.Format("RunScheduleAnimController.ResetTrigger; {0}", triggerType));

        if (ETrigger.MAX == triggerType)
            return;

        resetTrigger(triggerType);
    }

    private void resetTrigger(ETrigger triggerType)
    {
        string s = triggerType.ToString();
        _animator.ResetTrigger(s);
    }

    public void SetTrigger(ETrigger triggerType)
    {
        Log.Debug(string.Format("RunScheduleAnimController.SetTrigger; {0}", triggerType));

        if (ETrigger.MAX == triggerType)
            return;

        setTrigger(triggerType);
    }

    private void setTrigger(ETrigger triggerType)
    {
        string s = triggerType.ToString();
        _animator.SetTrigger(s);
    }


    public float GetCurrentStateLength(int layerIndex)
    {
        AnimatorStateInfo info = _animator.GetCurrentAnimatorStateInfo(layerIndex);

        float length = info.length;
        Log.Debug(string.Format("animator state info. length: {0}", length));

        return length;
    }
}
