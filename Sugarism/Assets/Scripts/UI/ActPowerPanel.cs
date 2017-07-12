using UnityEngine;


public class ActPowerPanel : MonoBehaviour
{
    //
    private ChargeablePanel _chargeablePanel = null;


    // Use this for initialization
    void Start()
    {
        _chargeablePanel = gameObject.GetComponent<ChargeablePanel>();
        
        _chargeablePanel.AddClickListener(onClickCharge);
    }

    private void onClickCharge()
    {
        Log.Debug("ActPowerPanel.onClickCharge");
    }
}
