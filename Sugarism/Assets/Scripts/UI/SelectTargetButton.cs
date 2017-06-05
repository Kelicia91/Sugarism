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

    // @todo: hero id 정보만 갖고 시나리오 가져오기
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
        if (false == isValid(targetId))
            return;

        _targetId = targetId;

        // @todo: update ui
        Target t = Manager.Instance.DTTarget[_targetId];
        set(t.name);
        //set(/*친밀도값*/);
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

    private bool isValid(int targetId)
    {
        if (targetId < 0)
        {
            string errMsg = string.Format("invalid target id({0}) < 0", targetId);
            Log.Error(errMsg);
            return false;
        }
        else if (targetId >= Manager.Instance.DTTarget.Count)
        {
            string errMsg = string.Format("invalid target id({0}) >= max({1})", targetId, Manager.Instance.DTTarget.Count);
            Log.Error(errMsg);
            return false;
        }
        else
        {
            return true;
        }
    }
}
