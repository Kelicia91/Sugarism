
public class CmdText : Command
{
    private Sugarism.CmdText _model;

    public CmdText(Sugarism.CmdText model) : base(model)
    {
        _model = model;
    }


    #region Property

    public string Text
    {
        get { return _model.Text; }
    }

    #endregion


    public override void Execute()
    {
        Log.Debug(ToString());
    }

    public override bool Play()
    {
        Log.Debug(ToString());

        Manager.Instance.CmdTextEvent.Invoke(Text);

        return false;   // no more child to play
    }

    public override string ToString()
    {
        string s = string.Format("Text({0})", Text);

        return ToString(s);
    }
}
