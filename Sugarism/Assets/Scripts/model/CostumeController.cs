
// Main Character's Costume Controller
public class CostumeController
{
    private int _costumeId = -1;
    public int CostumeId { get { return _costumeId; } }

    private bool _isBuy = false;
    public bool IsBuy
    {
        get { return _isBuy; }
        private set
        {
            _isBuy = value;

            if (_isBuy)
                Manager.Instance.Object.MainCharacter.BuyCostumeEvent.Invoke(CostumeId);

            string key = PlayerPrefsKey.GetKey(PlayerPrefsKey.ISBUY_COSTUME, CostumeId);
            int isBuyInteger = PlayerPrefsKey.GetBoolToInt(_isBuy);
            CustomPlayerPrefs.SetInt(key, isBuyInteger);
        }
    }

    // constructor
    public CostumeController(int costumeId, bool isBuy = false)
    {
        _costumeId = costumeId;
        _isBuy = isBuy;
    }

    public void Buy()
    {
        if (IsBuy)
        {
            Log.Error(string.Format("bought the costume({0}) already", CostumeId));
            return;
        }

        MainCharacterCostume costume = Manager.Instance.DT.MainCharacterCostume[CostumeId];
        MainCharacter mc = Manager.Instance.Object.MainCharacter;
        mc.Money -= costume.price;
        mc.Charm += costume.charm;

        IsBuy = true;
    }
}
