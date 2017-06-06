using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SelectTargetButton : MonoBehaviour
{
    /********* Editor Interface *********/
    // prefabs
    public Text NameText;
    public Text ValueText;
    public Slider Slider;
    public Image Image;
    
    //
    private int _targetId = -1;


	// Use this for initialization
	void Start ()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(onClick);

        if (null != Slider)
        {
            Slider.wholeNumbers = true;
            Slider.minValue = 0;
            Slider.maxValue = 0;
            Slider.value = Slider.minValue;
        }
	}

    public void Set(int targetId)
    {
        if (false == TargetCharacter.isValid(targetId))
            return;

        _targetId = targetId;
        
        Target t = Manager.Instance.DTTarget[_targetId];

        Character c = Manager.Instance.DTCharacter[t.characterId];
        set(c.name);

        TargetCharacter tc = Manager.Instance.Object.TargetCharacterArray[t.characterId];
        set(tc.Feeling);

        set(t.image);
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

    private void set(int value)
    {
        if (null == ValueText)
            Log.Error("not found value text");
        else
            ValueText.text = value.ToString();

        if (null == Slider)
            Log.Error("not found slider");
        else
            Slider.value = System.Convert.ToInt32(value);
    }

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
        Log.Debug("SelectTargetButton.onClick");
    }
}
