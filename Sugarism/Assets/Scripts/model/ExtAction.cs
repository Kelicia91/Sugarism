
public class ExtAction
{
    public static bool isValid(int id)
    {
        if (id < 0)
            return false;
        else if (id >= Manager.Instance.DTAction.Count)
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
        else if (id >= Manager.Instance.DTActionLesson.Count)
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
        else if (id >= Manager.Instance.DTActionPartTime.Count)
            return false;
        else
            return true;
    }
}