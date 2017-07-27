
public class ExtActionNPC
{
    public static bool IsValid(int id)
    {
        if (id < 0)
            return false;
        else if (id >= Manager.Instance.DT.ActionNPC.Count)
            return false;
        else
            return true;
    }
}
