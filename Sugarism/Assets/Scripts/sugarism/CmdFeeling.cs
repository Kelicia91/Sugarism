using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmdFeeling : Command
{
    private Sugarism.CmdFeeling _model;

    public CmdFeeling(Sugarism.CmdFeeling model) : base(model)
    {
        _model = model;
    }


    #region Property

    public int CharacterId
    {
        get { return _model.CharacterId; }
    }

    public Sugarism.EOperation Op
    {
        get { return _model.Op; }
    }

    public int Value
    {
        get { return _model.Value; }
    }

    #endregion


    public override void Execute()
    {
        Log.Debug(ToString());
    }

    public override bool Play()
    {
        Log.Debug(ToString());

        Manager.Instance.CmdFeelingEvent.Invoke(CharacterId, Op, Value);

        return false;   // no more child to play
    }
}
