using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StatPanel : Panel
{
    /********* Editor Interface *********/
    // prefabs
    public Text NameText;
    public Slider Slider;
    public Text ValueText;

    //
    private EStat _statType = EStat.MAX;

    //
    void Awake()
    {
        if (null == Slider)
            Log.Error("not found slider");

        Slider.wholeNumbers = true;
        Slider.minValue = Def.MIN_STAT;
        Slider.maxValue = Def.MAX_STAT;
        Slider.value = Slider.minValue;
    }

    void Start()
    {
        Manager.Instance.MainCharacterStatEvent.Attach(onMainCharacterStatChanged);
    }

    public void Set(EStat statType)
    {
        if (EStat.MAX == statType)
        {
            Log.Error("invalid stat type");
            return;
        }

        _statType = statType;

        int statId = (int)statType;
        Stat stat = Manager.Instance.DTStat[statId];
        setNameText(stat.name);

        MainCharacter mainCharacter = Manager.Instance.Object.MainCharacter;
        int value = mainCharacter.Get(_statType);
        setValue(value);
    }

    private void setValue(int value)
    {
        Slider.value = value;
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

    private void setValueText(int value)
    {
        if (null == ValueText)
        {
            Log.Error("not found value text");
            return;
        }

        ValueText.text = value.ToString();
    }
    

    private void onMainCharacterStatChanged(EStat statType, int value)
    {
        if (_statType != statType)
            return;

        setValue(value);
    }
}
