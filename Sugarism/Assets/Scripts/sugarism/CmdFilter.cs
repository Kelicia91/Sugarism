using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CmdFilter : Command
{
    private Sugarism.CmdFilter _model;

	public CmdFilter(Sugarism.CmdFilter model) : base(model)
    {
        _model = model;
    }


    #region Property

    public Sugarism.EFilter Filter
    {
        get { return _model.Filter; }
    }

    #endregion


    public override void Execute()
    {
        Log.Debug(ToString());
    }

    public override bool Play()
    {
        Log.Debug(ToString());

        Manager.Instance.CmdFilterEvent.Invoke(Filter);

        return false;   // no more child to play
    }
}
