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

    //
    private ChargeablePanel _actPower = null;
    public ChargeablePanel ActPowePanel { get { return _actPower; } }

    private ChargeablePanel _gold = null;
    public ChargeablePanel GoldPanel { get { return _gold; } }

    private ChargeablePanel _money = null;
    public ChargeablePanel MoneyPanel { get { return _money; } }

    //
    void Awake()
    {
        _actPower = create(typeof(ActPowerPanel), ActPowerIcon);
        _gold = create(typeof(GoldPanel), GoldIcon);
        _money = create(typeof(MoneyPanel), MoneyIcon);
    }

    private ChargeablePanel create(Type componentType, Sprite iconSprite)
    {
        GameObject o = Instantiate(PrefChargeablePanel);
        o.transform.SetParent(transform, false);

        o.AddComponent(componentType);

        ChargeablePanel c = o.GetComponent<ChargeablePanel>();
        c.SetIcon(iconSprite);

        return c;
    }
}