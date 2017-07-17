
public class MainCharacter : Nurture.Character
{
    public MainCharacter(int age, int money, int costumeId) : base(age)
    {
        _money = money;
        _wearingCostumeId = costumeId;
        
        // TEST
        {
            Name = @"테스트";
            Constitution = EConstitution.MOON;
            Zodiac = EZodiac.DRAGON;
        }
    }

    // profile
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
    
    private int _money = 0;
    public override int Money
    {
        get { return _money; }
        set
        {
            int adjustedValue = value;
            if (adjustedValue < Def.MIN_MONEY)
                adjustedValue = Def.MIN_MONEY;
            else if (adjustedValue > Def.MAX_MONEY)
                adjustedValue = Def.MAX_MONEY;

            if (_money.Equals(adjustedValue))
                return;

            _money = adjustedValue;
            Manager.Instance.MoneyChangeEvent.Invoke(_money);
        }
    }

    private int _wearingCostumeId = -1;
    public int WearingCostumeId
    {
        get { return _wearingCostumeId; }
        set { _wearingCostumeId = value; }
    }

    public bool IsChildHood()
    {
        int midAge = (Def.INIT_AGE + Def.MAX_AGE) / 2;
        if (Age < midAge)
            return true;
        else
            return false;
    }
}
