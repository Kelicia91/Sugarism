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
        setValue(flow);
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
}
