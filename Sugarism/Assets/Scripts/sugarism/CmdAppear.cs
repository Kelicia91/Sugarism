
public class CmdAppear : Command
{
    private Sugarism.CmdAppear _model;

    public CmdAppear(Sugarism.CmdAppear model) : base(model)
    {
        _model = model;
    }


    #region Property

    public int CharacterId
    {
        get { return _model.CharacterId; }
    }

    public Sugarism.EFace Face
    {
        get { return _model.Face; }
    }

    public Sugarism.ECostume Costume
    {
        get { return _model.Costume; }
    }

    public Sugarism.EPosition Position
    {
        get { return _model.Position; }
    }

    #endregion


    public override void Execute()
    {
        Log.Debug(ToString());
    }

    public override bool Play()
    {
        Log.Debug(ToString());

        Manager.Instance.CmdAppearEvent.Invoke(CharacterId, Face, Costume, Position);

        return false;   // no more child to play
    }

    public override string ToString()
    {
        string s = string.Format(
                    "CharacterId: {0}, Face: {1}, Costume: {2}, Pos: {3}",
                    CharacterId, Face, Costume, Position);

        return ToString(s);
    }
}
