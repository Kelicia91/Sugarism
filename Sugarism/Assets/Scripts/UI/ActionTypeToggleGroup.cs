using System;
using UnityEngine;
using UnityEngine.UI;


public class ActionTypeToggleGroup : Panel
{
    /********* Editor Interface *********/
    // prefabs
    public GameObject PrefActionTypeToggle;

    //
    private ToggleGroup _toggleGroup;

    //
    void Awake()
    {
        _toggleGroup = GetComponent<ToggleGroup>();
    }

    // Use this for initialization
    void Start ()
    {
	    if (null == PrefActionTypeToggle)
        {
            Log.Error("not found prefab action type toggle");
            return;
        }

        Array actionTypeArray = Enum.GetValues(typeof(EActionType));
        int NUM_BUTTON = (int)EActionType.MAX;
        
        for (int i = 0; i < NUM_BUTTON; ++i)
        {
            GameObject o = Instantiate(PrefActionTypeToggle);
            o.transform.SetParent(transform, false);

            // @ anchor(left,top), pivot(1,1)
            RectTransform rectTransform = o.GetComponent<RectTransform>();
            float height = rectTransform.rect.height;
            float posY = -1 * i * height;
            rectTransform.anchoredPosition = new Vector2(0.0f, posY);

            ActionTypeToggle toggle = o.GetComponent<ActionTypeToggle>();
            EActionType val = (EActionType)actionTypeArray.GetValue(i);
            toggle.SetActionType(val);
            toggle.Set(_toggleGroup);
        }
    }
}
