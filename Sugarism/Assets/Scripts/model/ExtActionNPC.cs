using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExtActionNPC
{
    public static bool IsValid(int id)
    {
        if (id < 0)
            return false;
        else if (id >= Manager.Instance.DTActionNPC.Count)
            return false;
        else
            return true;
    }
}
