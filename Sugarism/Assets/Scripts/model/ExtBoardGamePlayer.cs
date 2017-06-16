using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtBoardGamePlayer
{
    public static bool isValid(int id)
    {
        if (id < 0)
            return false;
        else if (id >= Manager.Instance.DTBoardGamePlayer.Count)
            return false;
        else
            return true;
    }
}