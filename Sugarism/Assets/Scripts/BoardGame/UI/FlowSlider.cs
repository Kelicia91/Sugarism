using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FlowSlider : MonoBehaviour
{
    //
    private Slider _slider = null;


    void Awake()
    {
        _slider = GetComponent<Slider>();

        _slider.interactable = false;
        _slider.wholeNumbers = true;
        _slider.minValue = BoardGame.BoardGameMode.MIN_FLOW;
        _slider.maxValue = BoardGame.BoardGameMode.MAX_FLOW;
        _slider.value = BoardGame.BoardGameMode.INIT_FLOW;

        Manager.Instance.Object.BoardGameMode.FlowChangeEvent.Attach(onFlowChanged);
    }


    private void onFlowChanged(int flow)
    {
        //setValue(flow);

        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(move(flow));
        }
        else
        {
            setValue(flow);
        }
    }

    private void setValue(int value)
    {
        if (null == _slider)
        {
            Log.Error("not found slider");
            return;
        }

        _slider.value = value;
    }

    private const float WAIT_SECONDS = 0.1f;
    private WaitForSeconds _waitForSeconds = new WaitForSeconds(WAIT_SECONDS);
    IEnumerator move(int flow)
    {
        float startValue = _slider.value;
        float t = 0.0f;
        while (t < 1.0f)
        {
            _slider.value = Mathf.SmoothStep(startValue, flow, t);
            t += WAIT_SECONDS;
            yield return _waitForSeconds;
        }

        setValue(flow);
        Manager.Instance.Object.BoardGameMode.JudgeIter();
    }
}
