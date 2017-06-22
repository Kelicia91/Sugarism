using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtCombatPlayer
{
    public static bool IsValid(int id)
    {
        if (id < 0)
            return false;
        else if (id >= Manager.Instance.DTCombatPlayer.Count)
            return false;
        else
            return true;
    }
}
