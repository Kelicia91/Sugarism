using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtCharacter
{
    public static bool IsValid(int id)
    {
        if (id < 0)
            return false;
        else if (id >= Manager.Instance.DTCharacter.Count)
            return false;
        else
            return true;
    }
}
