using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CalendarPanel : Panel
{
    /********* Editor Interface *********/
    // prefabs
    public Text YearText;
    public Text YearUnitText;
    public Text MonthText;
    public Text MonthUnitText;
    public Text DayText;
    public Text DayUnitText;


    // Use this for initialization
    void Start ()
    {
        Nurture.Calendar _calendar = Manager.Instance.Object.Calendar;
        if (null != _calendar)
        {
            setYearText(_calendar.Year.ToString());
            setMonthText(_calendar.Month.ToString());
            setDayText(_calendar.Day.ToString());
        }
        else
        {
            Log.Error("not found calendar");
        }

        setYearUnitText(Def.YEAR_UNIT);
        setMonthUnitText(Def.MONTH_UNIT);
        setDayUnitText(Def.DAY_UNIT);

        Manager.Instance.YearChangeEvent.Attach(onYearChanged);
        Manager.Instance.MonthChangeEvent.Attach(onMonthChanged);
        Manager.Instance.DayChangeEvent.Attach(onDayChanged);
    }

    private void setYearText(string s)
    {
        if (null == YearText)
        {
            Log.Error("not found year text");
            return;
        }

        YearText.text = s;
    }

    private void setMonthText(string s)
    {
        if (null == MonthText)
        {
            Log.Error("not found month text");
            return;
        }

        MonthText.text = s;
    }

    private void setDayText(string s)
    {
        if (null == DayText)
        {
            Log.Error("not found day text");
            return;
        }

        DayText.text = s;
    }

    private void setYearUnitText(string s)
    {
        if (null == YearUnitText)
        {
            Log.Error("not found year unit text");
            return;
        }

        YearUnitText.text = s;
    }

    private void setMonthUnitText(string s)
    {
        if (null == MonthUnitText)
        {
            Log.Error("not found month unit text");
            return;
        }

        MonthUnitText.text = s;
    }

    private void setDayUnitText(string s)
    {
        if (null == DayUnitText)
        {
            Log.Error("not found day unit text");
            return;
        }

        DayUnitText.text = s;
    }

    private void onYearChanged(int year)
    {
        setYearText(year.ToString());
    }

    private void onMonthChanged(int month)
    {
        setMonthText(month.ToString());
    }

    private void onDayChanged(int day)
    {
        setDayText(day.ToString());
    }
}
