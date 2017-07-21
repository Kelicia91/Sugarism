using System.Collections.Generic;


public class Wardrobe
{
    private List<CostumeController> _costumeList = null;
    public List<CostumeController> CostumeList { get { return _costumeList; } }

    // constructor
    public Wardrobe()
    {
        _costumeList = new List<CostumeController>();
    }
}
