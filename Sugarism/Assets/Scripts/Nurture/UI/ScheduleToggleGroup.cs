using UnityEngine;
using UnityEngine.UI;


public class ScheduleToggleGroup : MonoBehaviour
{
    /********* Editor Interface *********/
    // prefabs
    public GameObject PrefScheduleToggle;

    //
    private ToggleGroup _toggleGroup = null;

    //
    void Awake()
    {
        _toggleGroup = GetComponent<ToggleGroup>();
    }

    // Use this for initialization
    void Start()
    {
        if (null == PrefScheduleToggle)
        {
            Log.Error("not found prefab schedule toggle");
            return;
        }

	    for (int i = 0; i < Def.MAX_NUM_ACTION_IN_MONTH; ++i)
        {
            GameObject o = Instantiate(PrefScheduleToggle);
            o.transform.SetParent(transform, false);

            ScheduleToggle toggle = o.GetComponent<ScheduleToggle>();
            toggle.ScheduleIndex = i;
            toggle.Set(_toggleGroup);
        }
	}
}
