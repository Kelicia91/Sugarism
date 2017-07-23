using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SelectTargetButton : MonoBehaviour
{
    /********* Editor Interface *********/
    // prefabs
    public Text NameText;
    //public Text ValueText;
    //public Slider Slider;
    public Image Image;
    
    //
    private int _targetId = -1;

    
    void Awake()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(onClick);

        //if (null != Slider)
        //{
        //    Slider.wholeNumbers = true;
        //    Slider.minValue = Def.MIN_FEELING;
        //    Slider.maxValue = Def.MAX_FEELING;
        //    Slider.value = Slider.minValue;
        //}
    }

    void OnEnable()
    {
        if (false == ExtTarget.isValid(_targetId))
            return;
        
        refresh();
    }
    
    public void Set(int targetId)
    {
        if (false == ExtTarget.isValid(targetId))
            return;

        _targetId = targetId;

        refresh();  // need, because called after OnEnable().
    }

    private void refresh()
    {
        Target t = Manager.Instance.DTTarget[_targetId];

        Character c = Manager.Instance.DTCharacter[t.characterId];
        set(c.image);
        set(c.name);

        //Story.TargetCharacter tc = Manager.Instance.Object.StoryMode.TargetCharacterArray[_targetId];
        //set(tc.Feeling);
    }

    private void set(string name)
    {
        if (null == NameText)
        {
            Log.Error("not found name text");
            return;
        }

        NameText.text = name;
    }

    //private void set(int value)
    //{
    //    if (null == ValueText)
    //        Log.Error("not found value text");
    //    else
    //        ValueText.text = value.ToString();

    //    if (null == Slider)
    //        Log.Error("not found slider");
    //    else
    //        Slider.value = (float)value;
    //}

    private void set(Sprite sprite)
    {
        if (null == Image)
        {
            Log.Error("not found image");
            return;
        }

        Image.sprite = sprite;
    }

    private void onClick()
    {
        //@todo : enable input blocking panel -> disable

        Story.Mode storyMode = Manager.Instance.Object.StoryMode;
    
        bool isLoaded = storyMode.LoadScenario(_targetId);
        if (isLoaded)
        {
            Manager.Instance.UI.SelectTargetPanel.Hide();
        }
        else
        {
            Log.Error("failed to load scenario");
            return; // @todo: 어떻게 처리할까...
        }
    }
}
