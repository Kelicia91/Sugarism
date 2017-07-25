using UnityEngine;
using UnityEngine.UI;


public class SliderPanel : MonoBehaviour
{
    /********* Editor Interface *********/
    // prefabs
    public Text NameText;
    public Slider Slider;
    public Text ValueText;


    //
    void Awake()
    {
        if (null != Slider)
        {
            Slider.interactable = false;
            Slider.wholeNumbers = true;
            Slider.minValue = Def.MIN_STAT;
            Slider.maxValue = Def.MAX_STAT;
            Slider.value = Slider.minValue;   
        }
    }
    
    public void SetNameText(string s)
    {
        if (null == NameText)
        {
            Log.Error("not found name text");
            return;
        }

        NameText.text = s;
    }

    public void SetValue(int value, string format = "{0}")
    {
        if (null == Slider)
        {
            Log.Error("not found slider");
            return;
        }

        Slider.value = value;

        string s = string.Format(format, value);
        setValueText(s);
    }

    private void setValueText(string s)
    {
        if (null == ValueText)
        {
            Log.Error("not found value text");
            return;
        }

        ValueText.text = s;
    }
}
