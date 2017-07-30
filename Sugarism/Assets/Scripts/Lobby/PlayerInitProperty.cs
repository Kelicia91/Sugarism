using UnityEngine;


public class PlayerInitProperty : MonoBehaviour
{
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

    private EZodiac _zodiac = EZodiac.MAX;
    public EZodiac Zodiac
    {
        get { return _zodiac; }
        set { _zodiac = value; }
    }
}
