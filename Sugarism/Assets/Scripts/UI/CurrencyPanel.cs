using System;
using UnityEngine;

public class CurrencyPanel : Panel
{
    /********* Editor Interface *********/
    // prefabs
    public GameObject PrefChargeablePanel;
    // sprites
    public Sprite ActPowerIcon;
    public Sprite GoldIcon;
    public Sprite MoneyIcon;


    // Use this for initialization
    void Start ()
    {
        createChargeablePanel(typeof(ActPowerPanel), ActPowerIcon);
        createChargeablePanel(typeof(GoldPanel), GoldIcon);
        createChargeablePanel(typeof(MoneyPanel), MoneyIcon);
    }


    private void createChargeablePanel(Type componentType, Sprite iconSprite)
    {
        GameObject o = Instantiate(PrefChargeablePanel);
        o.transform.SetParent(transform, false);

        o.AddComponent(componentType);

        ChargeablePanel c = o.GetComponent<ChargeablePanel>();
        c.SetIcon(iconSprite);
    }
}