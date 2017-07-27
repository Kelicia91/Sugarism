
public class ExtCharacter
{
    public static bool IsValid(int id)
    {
        if (id < 0)
            return false;
        else if (id >= Manager.Instance.DT.Character.Count)
            return false;
        else
            return true;
    }
}
