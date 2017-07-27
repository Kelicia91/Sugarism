using UnityEngine;

public class ExtMainCharacterLooks
{
    public static bool IsValid(int id)
    {
        if (id < 0)
            return false;
        else if (id >= Manager.Instance.DT.MainCharacterLooks.Count)
            return false;
        else
            return true;
    }
}

public class ExtMainCharacterCostume
{
    public static bool IsValid(int id)
    {
        if (id < 0)
            return false;
        else if (id >= Manager.Instance.DT.MainCharacterCostume.Count)
            return false;
        else
            return true;
    }

    public static Sprite Get(int id, int looksId)
    {
        if (false == IsValid(id))
            return null;

        if (false == ExtMainCharacterLooks.IsValid(looksId))
            return null;

        MainCharacterCostume costume = Manager.Instance.DT.MainCharacterCostume[id];

        switch (looksId)
        {
            case 0: return costume.age0;
            case 1: return costume.age1;
            case 2: return costume.age2;
            case 3: return costume.age3;
            default: return null;
        }
    }
}
