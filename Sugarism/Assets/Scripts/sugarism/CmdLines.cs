using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

    public override bool Play()
    {
        Log.Debug(ToString());

        Manager.Instance.CmdLinesEvent.Invoke(CharacterId, Lines);

        return false;   // no more child to play
    }

    public override string ToString()
    {
        string s = string.Format(
                    "CharacterId : {0}, Lines : \"{1}\"",
                    CharacterId, Lines);

        return ToString(s);
    }
}
