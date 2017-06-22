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

    IEnumerator move(int toValue)
    {
        const float waitSeconds = 0.1f;

        float fromValue = Slider.value;
        float t = 0.0f;
        while (t < 1.0f)
        {
            int tmp = (int) Mathf.SmoothStep(fromValue, toValue, t);
            set(tmp);

            t += waitSeconds;
            yield return new WaitForSeconds(waitSeconds);
        }

        set(toValue);
        yield return new WaitForSeconds(0.6f);

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
