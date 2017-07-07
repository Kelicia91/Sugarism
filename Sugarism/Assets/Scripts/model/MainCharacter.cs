using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 주인공
public partial class MainCharacter : Nurture.ICurrency
{
    public MainCharacter(int age, int money)
    {
        _age = age;
        _money = money;
        
        // TEST
        {
            Name = @"테스트";
            Constitution = EConstitution.MOON;
        }
    }

    // profile
    private int _age;
    public int Age
    {
        get { return _age; }
        set { _age = value; }
    }

    private string _name = string.Empty;
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }
    
    private EConstitution _constitution = EConstitution.MAX;
    public EConstitution Constitution
    {
        get { return _constitution; }
        set { _constitution = value; }
    }

    // money : income-expense during 1 day
    private int _money;
    public int Money
    {
        get { return _money; }
        set
        {
            _money = value;

            if (_money < Def.MIN_MONEY)
                _money = Def.MIN_MONEY;
            else if (_money > Def.MAX_MONEY)
                _money = Def.MAX_MONEY;

            Manager.Instance.MoneyChangeEvent.Invoke(_money);
        }
    }

    public bool IsChildHood()
    {
        int midAge = (Def.INIT_AGE + (Def.INIT_AGE + Def.PERIOD_YEAR)) / 2;
        if (Age < midAge)
            return true;
        else
            return false;
    }

    // stats : date
    // @todo
}
