
public class MainCharacter : Nurture.Character
{
    public MainCharacter(int age, int money, int costumeId) : base(age)
    {
        _money = money;

        _wardrobe = new Wardrobe();
        int mainCharacterCostumeCount = Manager.Instance.DT.MainCharacterCostume.Count;
        for (int i = 0; i < mainCharacterCostumeCount; ++i)
        {
            CostumeController costumeCtrl = new CostumeController(i);
            _wardrobe.CostumeList.Add(costumeCtrl);
        }

        _wearingCostumeId = costumeId;

        // events
        _moneyChangeEvent = new MoneyChangeEvent();
        _buyCostumeEvent = new BuyCostumeEvent();
        _wearCostumeEvent = new WearCostumeEvent();
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
            MoneyChangeEvent.Invoke(_money);
        }
    }

    private int _wearingCostumeId = -1;
    public int WearingCostumeId
    {
        get { return _wearingCostumeId; }
        private set
        {
            _wearingCostumeId = value;
            WearCostumeEvent.Invoke(_wearingCostumeId);
        }
    }

    private Wardrobe _wardrobe = null;
    public Wardrobe Wardrobe { get { return _wardrobe; } }


    #region Events

    private MoneyChangeEvent _moneyChangeEvent = null;
    public MoneyChangeEvent MoneyChangeEvent { get { return _moneyChangeEvent; } }

    private BuyCostumeEvent _buyCostumeEvent = null;
    public BuyCostumeEvent BuyCostumeEvent { get { return _buyCostumeEvent; } }

    private WearCostumeEvent _wearCostumeEvent = null;
    public WearCostumeEvent WearCostumeEvent { get { return _wearCostumeEvent; } }

    #endregion


    //
    public void PutOn(int costumeId)
    {
        WearingCostumeId = costumeId;
    }

    public void PutOff()
    {
        WearingCostumeId = Def.DEFAULT_COSTUME_ID;
    }


    public override void Die()
    {
        string sickScenarioPath = string.Format("{0}{1}{2}",
                            RsrcLoader.SCENARIO_FOLDER_PATH, RsrcLoader.DIR_SEPARATOR,
                            RsrcLoader.SICK_BAD_ENDING_FILENAME);

        Story.Mode storyMode = Manager.Instance.Object.StoryMode;

        bool isLoaded = storyMode.LoadScenario(sickScenarioPath);
        if (false == isLoaded)
            return; // @todo: 에러. 게임종료.

        storyMode.ScenarioEndEvent.Attach(onScenarioEnd);
    }

    private void onScenarioEnd()
    {
        // @todo: 질병 시나리오 끝. 게임종료.
        Log.Debug("die scenario end.");
    }
}
