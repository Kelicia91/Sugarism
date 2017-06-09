using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtBackground
{
    public static bool IsValid(int id)
    {
        if (id < 0)
            return false;
        else if (id >= Manager.Instance.DTBackground.Count)
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
        else if (id >= Manager.Instance.DTMiniPicture.Count)
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
        else if (id >= Manager.Instance.DTPicture.Count)
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
        else if (id >= Manager.Instance.DTSE.Count)
            return false;
        else
            return true;
    }
}
