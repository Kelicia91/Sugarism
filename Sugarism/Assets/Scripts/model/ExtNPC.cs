using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExtNPC
{
    public static bool IsValid(int id)
    {
        if (id < 0)
            return false;
        else if (id >= Manager.Instance.DTNPC.Count)
            return false;
        else
            return true;
    }
}
