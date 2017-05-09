using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// @note : differ from struct 'UnityEngine.Scene'
public class Scene
{
    private Sugarism.Scene _model;

    private List<Command> _cmdList;

    public Scene(Sugarism.Scene model)
    {
        Log.Assert((null == model), "Not Found Sugarism.Scene");

        _model = model;

        _cmdList = new List<Command>();
        foreach (Sugarism.Command mCmd in _model.CmdList)
        {
            Command cmd = Command.Create(mCmd);
            _cmdList.Add(cmd);
        }
    }


    #region Property

    public string Description
    {
        get { return _model.Description; }
    }

    #endregion


    public void Execute()
    {
        Log.Debug(ToString());

        foreach (Command cmd in _cmdList)
        {
            cmd.Execute();
        }
    }

    public override string ToString()
    {
        string s = string.Format("[Scene] Description : {0}", Description);
        return s;
    }
}
