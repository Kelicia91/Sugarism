using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmdBackground : Command
{
    private Sugarism.CmdBackground _model;

    public CmdBackground(Sugarism.CmdBackground model) : base(model)
    {
        _model = model;
    }


    #region Property

    public int Id
    {
        get { return _model.Id; }
    }

    #endregion


    public override void Execute()
    {
        Log.Debug(ToString());
    }

    public override bool Play()
    {
        Log.Debug(ToString());

        Manager.Instance.CmdBackgroundEvent.Invoke(Id);

        return false;   // no more child to play
    }

    public override string ToString()
    {
        string s = string.Format("Id({0})", Id);

        return ToString(s);
    }
}
