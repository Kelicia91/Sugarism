using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CmdLines : Command
{
    private Sugarism.CmdLines _model;

    public CmdLines(Sugarism.CmdLines model) : base(model)
    {
        _model = model;
    }


    #region Property

    public int CharacterId
    {
        get { return _model.CharacterId; }
    }

    public string Lines
    {
        get { return _model.Lines; }
    }

    #endregion


    public override void Execute()
    {
        Log.Debug(ToString());
    }

    public override string ToString()
    {
        string s = string.Format(
                    "CharacterId : {0}, Lines : \"{1}\"",
                    CharacterId, Lines);

        return ToString(s);
    }
}
