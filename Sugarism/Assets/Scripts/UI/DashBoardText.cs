using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DashBoardText : MonoBehaviour
{
    private Text _text;


    // Use this for initialization
    void Awake()
    {
        _text = GetComponent<Text>();
        if (null == _text)
            Log.Error("not found dashboard text");
    }

    // Use this for initialization
    void OnEnable()
    {
        Calendar calendar = Manager.Instance.Object.Calendar;

        string s = string.Format("{0} {1}  {2} {3}", 
                                calendar.Year, Def.YEAR_UNIT,
                                calendar.Month, Def.MONTH_UNIT);

        _text.text = s;
	}
}
