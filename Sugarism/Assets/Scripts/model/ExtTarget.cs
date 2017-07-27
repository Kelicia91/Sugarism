
public class ExtTarget
{
    public static bool isValid(int id)
    {
        if (id < 0)
            return false;
        else if (id >= Manager.Instance.DT.Target.Count)
            return false;
        else
            return true;
    }
}
