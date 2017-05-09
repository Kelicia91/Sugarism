using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CmdSwitch : Command
{
    private Sugarism.CmdSwitch _model;

    private List<CmdCase> _caseList;


    public CmdSwitch(Sugarism.CmdSwitch model) : base(model)
    {
        _model = model;

        _caseList = new List<CmdCase>();
        foreach(Sugarism.CmdCase mCase in _model.CaseList)
        {
            CmdCase cmdCase = new CmdCase(mCase);
            _caseList.Add(cmdCase);
        }
    }


    #region Property

    public int CharacterId
    {
        get { return _model.CharacterId; }
    }

    #endregion


    public override void Execute()
    {
        Log.Debug(ToString());

        foreach(CmdCase cmdCase in _caseList)
        {
            cmdCase.Execute();
        }
    }

    public override string ToString()
    {
        string s = string.Format(
            "CharacterId : {0}, Case Count : {1}",
            CharacterId, _caseList.Count);

        return ToString(s);
    }
}
