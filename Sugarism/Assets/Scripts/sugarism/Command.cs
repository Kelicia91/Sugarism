using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Command
{
    private Sugarism.Command _model;

    public Command(Sugarism.Command model)
    {
        Log.Assert((null == model), "Not Found Sugarism.Command");

        _model = model;
    }


    public static Command Create(Sugarism.Command model)
    {
        if (null == model)
            return null;

        switch(model.CmdType)
        {
            case Sugarism.Command.Type.Lines:
                return new CmdLines(model as Sugarism.CmdLines);

            case Sugarism.Command.Type.Switch:
                return new CmdCase(model as Sugarism.CmdCase);

            case Sugarism.Command.Type.Case:
                return new CmdSwitch(model as Sugarism.CmdSwitch);

            default:
                return null;
        }
    }


    #region Property

    public Sugarism.Command.Type CmdType
    {
        get { return _model.CmdType; }
    }

    public string StrCmdType
    {
        get { return CmdType.ToString(); }
    }

    #endregion


    public abstract void Execute();

    protected string ToString(string content)
    {
        string s = string.Format("[{0}] {1}", StrCmdType, content);
        return s;
    }
}
