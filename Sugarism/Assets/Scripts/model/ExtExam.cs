
public class ExtScoreExam
{
    public static bool isValid(int id)
    {
        if (id < 0)
            return false;
        else if (id >= Manager.Instance.DT.ScoreExam.Count)
            return false;
        else
            return true;
    }
}

public class ExtOneToOneExam
{
    public static bool isValid(int id)
    {
        if (id < 0)
            return false;
        else if (id >= Manager.Instance.DT.OneToOneExam.Count)
            return false;
        else
            return true;
    }
}