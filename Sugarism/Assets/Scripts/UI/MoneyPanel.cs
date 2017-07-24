using UnityEngine;


public class MoneyPanel : MonoBehaviour
{
    //
    private ChargeablePanel _chargeablePanel = null;
    

    // Use this for initialization
    void Start()
    {
        _chargeablePanel = gameObject.GetComponent<ChargeablePanel>();
        
        MainCharacter mainCharacter = Manager.Instance.Object.MainCharacter;
        mainCharacter.MoneyChangeEvent.Attach(onMoneyChanged);

        int money = mainCharacter.Money;
        _chargeablePanel.SetText(get(money));

        _chargeablePanel.AddClickListener(onClickCharge);
    }

    private void onMoneyChanged(int money)
    {
        _chargeablePanel.SetText(get(money));
    }

    private string get(int money)
    {
        return money.ToString();
    }

    private void onClickCharge()
    {
        Log.Debug("MoneyPanel.onClickCharge");
    }
}
