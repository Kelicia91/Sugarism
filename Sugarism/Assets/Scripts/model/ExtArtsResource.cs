using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtBackground
{
    public static bool IsValid(int id)
    {
        if (id < 0)
            return false;
        else if (id >= Manager.Instance.DT.Background.Count)
            return false;
        else
            return true;
    }
}

public class ExtMiniPicture
{
    public static bool IsValid(int id)
    {
        if (id < 0)
            return false;
        else if (id >= Manager.Instance.DT.MiniPicture.Count)
            return false;
        else
            return true;
    }
}

public class ExtPicture
{
    public static bool IsValid(int id)
    {
        if (id < 0)
            return false;
        else if (id >= Manager.Instance.DT.Picture.Count)
            return false;
        else
            return true;
    }
}

public class ExtSE
{
    public static bool IsValid(int id)
    {
        if (id < 0)
            return false;
        else if (id >= Manager.Instance.DT.SE.Count)
            return false;
        else
            return true;
    }
}
