using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CmdCase : Command
{
    private Sugarism.CmdCase _model;

    private List<Command> _cmdList;
    private IEnumerator<Command> _cmdIter;

    public CmdCase(Sugarism.CmdCase model) : base(model)
    {
        _model = model;

        _cmdList = new List<Command>();
        foreach(Sugarism.Command mCmd in _model.CmdList)
        {
            Command cmd = Command.Create(mCmd);
            _cmdList.Add(cmd);
        }

        _cmdIter = _cmdList.GetEnumerator();
        _cmdIter.MoveNext();
    }


    #region Property

    public int Key
    {
        get { return _model.Key; }
    }

    public string Description
    {
        get { return _model.Description; }
    }

    #endregion


    public override void Execute()
    {
        Log.Debug(ToString());

        foreach(Command cmd in _cmdList)
        {
            cmd.Execute();
        }
    }

    public override bool Play()
    {
        Log.Debug(ToString());

        if (_cmdList.Count <= 0)
            return false;

        Command cmd = _cmdIter.Current;

        if (cmd.Play())
            return true;
        else
            return _cmdIter.MoveNext();
    }

    public override string ToString()
    {
        string s = string.Format(
                "Key : {0}, Description : {1}",
                Key, Description);

        return ToString(s);
    }
}
