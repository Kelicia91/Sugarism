
public class ExtAction
{
    public static bool isValid(int id)
    {
        if (id < 0)
            return false;
        else if (id >= Manager.Instance.DT.Action.Count)
            return false;
        else
            return true;
    }
}

public class ExtActionLesson
{
    public static bool isValid(int id)
    {
        if (id < 0)
            return false;
        else if (id >= Manager.Instance.DT.ActionLesson.Count)
            return false;
        else
            return true;
    }
}

public class ExtActionPartTime
{
    public static bool isValid(int id)
    {
        if (id < 0)
            return false;
        else if (id >= Manager.Instance.DT.ActionPartTime.Count)
            return false;
        else
            return true;
    }
}