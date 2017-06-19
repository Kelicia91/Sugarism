using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Command : IPlayable
{
    private Sugarism.Command _model = null;

    public Command(Sugarism.Command model)
    {
        if (null == model)
        {
            Log.Error("Not Found Sugarism.Command");
            return;
        }

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

            case Sugarism.Command.Type.Text:
                return new CmdText(model as Sugarism.CmdText);

            case Sugarism.Command.Type.Appear:
                return new CmdAppear(model as Sugarism.CmdAppear);

            case Sugarism.Command.Type.Background:
                return new CmdBackground(model as Sugarism.CmdBackground);

            case Sugarism.Command.Type.MiniPicture:
                return new CmdMiniPicture(model as Sugarism.CmdMiniPicture);

            case Sugarism.Command.Type.Picture:
                return new CmdPicture(model as Sugarism.CmdPicture);

            case Sugarism.Command.Type.Filter:
                return new CmdFilter(model as Sugarism.CmdFilter);

            case Sugarism.Command.Type.SE:
                return new CmdSE(model as Sugarism.CmdSE);

            case Sugarism.Command.Type.Feeling:
                return new CmdFeeling(model as Sugarism.CmdFeeling);

            case Sugarism.Command.Type.Switch:
                return new CmdSwitch(model as Sugarism.CmdSwitch);

            case Sugarism.Command.Type.Case:
                return new CmdCase(model as Sugarism.CmdCase);
                
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
    public abstract bool Play();

    protected string ToString(string content)
    {
        string s = string.Format("[Cmd.{0}] {1}", StrCmdType, content);
        return s;
    }
}
