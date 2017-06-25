using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScheduleToggleGroup : MonoBehaviour
{
    /********* Editor Interface *********/
    // prefabs
    public GameObject PrefScheduleToggle;

    //
    public int SPACE_X_BETWEEN_TOGGLE = 10;

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
        if (null == PrefScheduleToggle)
        {
            Log.Error("not found prefab schedule toggle");
            return;
        }

        RectTransform rectTransform = PrefScheduleToggle.GetComponent<RectTransform>();
        float childWidth = rectTransform.rect.width;

        int halfNumOfAction = Def.MAX_NUM_ACTION_IN_MONTH / 2;
        float space_posX = childWidth + SPACE_X_BETWEEN_TOGGLE;

        float posX = -1 * space_posX * halfNumOfAction;
	    for (int i = 0; i < Def.MAX_NUM_ACTION_IN_MONTH; ++i)
        {
            GameObject o = Instantiate(PrefScheduleToggle);
            o.transform.SetParent(transform, false);

            RectTransform rect = o.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(posX, 0.0f);
            posX += space_posX;

            ScheduleToggle toggle = o.GetComponent<ScheduleToggle>();
            toggle.ScheduleIndex = i;
            toggle.Set(_toggleGroup);
        }
	}
}
