
public class ExtRival
{
    public static bool isValid(int id)
    {
        if (id < 0)
            return false;
        else if (id >= Manager.Instance.DTRival.Count)
            return false;
        else
            return true;
    }
}
