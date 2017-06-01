using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 주인공
public partial class Heroine
{
    public Heroine(int age, int money)
    {
        _age = age;
        _money = money;

        _actCount = new int[Manager.Instance.DTAction.Count];
        for (int i = 0; i < _actCount.Length; ++i)
        {
            _actCount[i] = 0;
        }

        // TEST
        {
            Name = @"테스트";
            Zodiac = EZodiac.DRAGON;
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

    // 최종적으로 탄생월에 따라 zodiac, 시작할 월이 달라진다.
    private EZodiac _zodiac = EZodiac.MAX;
    public EZodiac Zodiac
    {
        get { return _zodiac; }
        set { _zodiac = value; init(_zodiac); }
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

    // stats : date
    // @todo


    //
    private void init(EZodiac zodiac)
    {
        int zodiacId = (int)zodiac;

        if ((zodiacId < 0) || (zodiacId >= Manager.Instance.DTZodiac.Count))
        {
            string errMsg = string.Format("invalid zodiac id: {0}", zodiacId);
            Log.Error(errMsg);
            return;
        }

        Stamina = Manager.Instance.DTZodiac[zodiacId].stamina;
        Intellect = Manager.Instance.DTZodiac[zodiacId].intellect;
        Grace = Manager.Instance.DTZodiac[zodiacId].grace;
        Charm = Manager.Instance.DTZodiac[zodiacId].charm;

        Attack = Manager.Instance.DTZodiac[zodiacId].attack;
        Defence = Manager.Instance.DTZodiac[zodiacId].defense;

        Leadership = Manager.Instance.DTZodiac[zodiacId].leadership;
        Tactic = Manager.Instance.DTZodiac[zodiacId].tactic;

        Morality = Manager.Instance.DTZodiac[zodiacId].morality;
        Goodness = Manager.Instance.DTZodiac[zodiacId].goodness;

        Sensibility = Manager.Instance.DTZodiac[zodiacId].sensibility;
        Arts = Manager.Instance.DTZodiac[zodiacId].arts;
    }
}

public partial class Heroine
{
    // count : action
    private int[] _actCount = null;

    public int GetActCount(int actionIndex)
    {
        if (actionIndex < 0)
            return -1;
        else if (actionIndex >= _actCount.Length)
            return -1;
        else
            return _actCount[actionIndex];
    }

    public void Increment(int actionIndex)
    {
        if (actionIndex < 0)
            return;
        else if (actionIndex >= _actCount.Length)
            return;
        else
            ++_actCount[actionIndex];
    }
}

public partial class Heroine
{
    // stats : nurture
    private int _stress = 0;
    public int Stress
    {
        get { return _stress; }
        set
        {
            _stress = value;

            adjust(ref _stress);
            Manager.Instance.HeroineStatEvent.Invoke(EStat.STRESS, _stress);
        }
    }

    private int _stamina;
    public int Stamina
    {
        get { return _stamina; }
        set
        {
            _stamina = value;

            adjust(ref _stamina);
            Manager.Instance.HeroineStatEvent.Invoke(EStat.STAMINA, _stamina);
        }
    }

    private int _intellect;
    public int Intellect
    {
        get { return _intellect; }
        set
        {
            _intellect = value;

            adjust(ref _intellect);
            Manager.Instance.HeroineStatEvent.Invoke(EStat.INTELLECT, _intellect);
        }
    }

    private int _grace;
    public int Grace
    {
        get { return _grace; }
        set
        {
            _grace = value;

            adjust(ref _grace);
            Manager.Instance.HeroineStatEvent.Invoke(EStat.GRACE, _grace);
        }
    }

    private int _charm;
    public int Charm
    {
        get { return _charm; }
        set
        {
            _charm = value;

            adjust(ref _charm);
            Manager.Instance.HeroineStatEvent.Invoke(EStat.CHARM, _charm);
        }
    }

    private int _attack;
    public int Attack
    {
        get { return _attack; }
        set
        {
            _attack = value;

            adjust(ref _attack);
            Manager.Instance.HeroineStatEvent.Invoke(EStat.ATTACK, _attack);
        }
    }

    private int _defence;
    public int Defence
    {
        get { return _defence; }
        set
        {
            _defence = value;

            adjust(ref _defence);
            Manager.Instance.HeroineStatEvent.Invoke(EStat.DEFENSE, _defence);
        }
    }

    private int _leadership;
    public int Leadership
    {
        get { return _leadership; }
        set
        {
            _leadership = value;

            adjust(ref _leadership);
            Manager.Instance.HeroineStatEvent.Invoke(EStat.LEADERSHIP, _leadership);
        }
    }

    private int _tactic;
    public int Tactic
    {
        get { return _tactic; }
        set
        {
            _tactic = value;

            adjust(ref _tactic);
            Manager.Instance.HeroineStatEvent.Invoke(EStat.TACTIC, _tactic);
        }
    }

    private int _morality;
    public int Morality
    {
        get { return _morality; }
        set
        {
            _morality = value;

            adjust(ref _morality);
            Manager.Instance.HeroineStatEvent.Invoke(EStat.MORALITY, _morality);
        }
    }

    private int _goodness;
    public int Goodness
    {
        get { return _goodness; }
        set
        {
            _goodness = value;

            adjust(ref _goodness);
            Manager.Instance.HeroineStatEvent.Invoke(EStat.GOODNESS, _goodness);
        }
    }

    private int _sensibility;
    public int Sensibility
    {
        get { return _sensibility; }
        set
        {
            _sensibility = value;

            adjust(ref _sensibility);
            Manager.Instance.HeroineStatEvent.Invoke(EStat.SENSIBILITY, _sensibility);
        }
    }

    private int _arts;
    public int Arts
    {
        get { return _arts; }
        set
        {
            _arts = value;

            adjust(ref _arts);
            Manager.Instance.HeroineStatEvent.Invoke(EStat.ARTS, _arts);
        }
    }

    
    private void adjust(ref int stat)
    {
        if (stat < Def.MIN_STAT)
            stat = Def.MIN_STAT;
        else if (stat > Def.MAX_STAT)
            stat = Def.MAX_STAT;
    }


    public int Get(EStat statType)
    {
        switch (statType)
        {
            case EStat.STRESS:
                return Stress;

            case EStat.STAMINA:
                return Stamina;

            case EStat.INTELLECT:
                return Intellect;

            case EStat.GRACE:
                return Grace;

            case EStat.CHARM:
                return Charm;

            case EStat.ATTACK:
                return Attack;

            case EStat.DEFENSE:
                return Defence;

            case EStat.LEADERSHIP:
                return Leadership;

            case EStat.TACTIC:
                return Tactic;

            case EStat.MORALITY:
                return Morality;

            case EStat.GOODNESS:
                return Goodness;

            case EStat.SENSIBILITY:
                return Sensibility;

            case EStat.ARTS:
                return Arts;

            default:
                return 0;
        }
    }
}
