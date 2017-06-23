﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Calendar
{
    public const int MIN_MONTH = 1;
    public const int MAX_MONTH = 12;
    public const int MIN_DAY = 1;

    // LastDay[0] is garbage value.
    public static readonly int[] LastDay = { -1, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

    public Calendar(int year, int month, int day)
    {
        _year = year;
        _month = month;
        _day = day;
    }

    private int _year;
    public int Year
    {
        get { return _year; }
        private set
        {
            _year = value;

            Manager.Instance.YearChangeEvent.Invoke(_year);
        }
    }

    private int _month;
    public int Month
    {
        get { return _month; }
        private set
        {
            _month = value;

            if (_month > MAX_MONTH)
            {
                _month = MIN_MONTH;
                ++Year;
            }

            Manager.Instance.MonthChangeEvent.Invoke(_month);
        }
    }

    private int _day;
    public int Day
    {
        get { return _day; }
        set
        {
            _day = value;
            
            if (_day > LastDay[Month])
            {
                _day = MIN_DAY;
                ++Month;
            }
            
            Manager.Instance.DayChangeEvent.Invoke(_day);
        }
    }


    public ESeason Get()
    {
        if (Month <= 0)
            return ESeason.MAX;
        else if (Month <= 2)
            return ESeason.WINTER;
        else if (Month <= 5)
            return ESeason.SPRING;
        else if (Month <= 8)
            return ESeason.SUMMER;
        else if (Month <= 11)
            return ESeason.FALL;
        else if (Month <= 12)
            return ESeason.WINTER;
        else
            return ESeason.MAX;
    }
}