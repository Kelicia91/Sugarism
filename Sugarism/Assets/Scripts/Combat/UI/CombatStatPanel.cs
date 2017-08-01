using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CombatStatPanel : Panel
{
    /********* Editor Interface *********/
    // prefabs
    public Text NameText;
    public Slider Slider;
    public Text ValueText;

    //
    void Awake()
    {
        if (null == Slider)
        {
            Log.Error("not found slider");
        }
        else
        {
            Slider.interactable = false;
            Slider.wholeNumbers = true;
            Slider.minValue = Def.MIN_STAT;
            Slider.maxValue = Def.MAX_STAT;
            Slider.value = Slider.minValue;
        }
    }

    public void Set(string name, int value)
    {
        setNameText(name);
        set(value);
    }

    public void Set(int value)
    {
        set(value);
    }

    public void SetRoutine(int toValue)
    {
        StartCoroutine(move(toValue));
    }

    private const float WAIT_SECONDS = .1f;
    private WaitForSeconds _waitForSeconds = new WaitForSeconds(WAIT_SECONDS);
    private const float DELAY_SECONDS = .6f;
    private WaitForSeconds _delayForSeconds = new WaitForSeconds(DELAY_SECONDS);
    IEnumerator move(int toValue)
    {
        float fromValue = Slider.value;
        float t = 0.0f;
        while (t < 1.0f)
        {
            int tmp = (int) Mathf.SmoothStep(fromValue, toValue, t);
            set(tmp);

            t += WAIT_SECONDS;
            yield return _waitForSeconds;
        }

        set(toValue);
        yield return _delayForSeconds;

        Manager.Instance.Object.CombatMode.BattleIterate();
    }

    private void set(int value)
    {
        setSliderValue(value);
        setValueText(value);
    }

    private void setNameText(string s)
    {
        if (null == NameText)
        {
            Log.Error("not found name text");
            return;
        }

        NameText.text = s;
    }

    private void setSliderValue(int value)
    {
        if (null == Slider)
        {
            Log.Error("not found slider");
            return;
        }

        Slider.value = value;
    }

    private void setValueText(int value)
    {
        if (null == ValueText)
        {
            Log.Error("not found value text");
            return;
        }

        ValueText.text = value.ToString();
    }
}
