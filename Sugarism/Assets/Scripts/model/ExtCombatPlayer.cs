
public class ExtCombatPlayer
{
    public static bool IsValid(int id)
    {
        if (id < 0)
            return false;
        else if (id >= Manager.Instance.DT.CombatPlayer.Count)
            return false;
        else
            return true;
    }
}
