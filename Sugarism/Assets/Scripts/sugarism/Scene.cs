using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// @note : differ from struct 'UnityEngine.Scene'
public class Scene : IPlayable
{
    private Sugarism.Scene _model;

    private List<Command> _cmdList;
    private IEnumerator<Command> _cmdIter;

    public Scene(Sugarism.Scene model)
    {
        if (null == model)
        {
            Log.Error("Not Found Sugarism.Scene");
            return;
        }

        _model = model;

        _cmdList = new List<Command>();
        foreach (Sugarism.Command mCmd in _model.CmdList)
        {
            Command cmd = Command.Create(mCmd);
            _cmdList.Add(cmd);
        }

        _cmdIter = _cmdList.GetEnumerator();
        _cmdIter.MoveNext();
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


    
    public bool Play()
    {
        if (_cmdList.Count <= 0)
            return false;

        Command cmd = _cmdIter.Current;

        if (cmd.Play())
            return true;
        else
            return _cmdIter.MoveNext();
    }
}
