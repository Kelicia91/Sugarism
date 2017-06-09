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

    private bool _isCalled = false;
    public override bool Play()
    {
        Log.Debug(ToString());

        if (false == _isCalled)
        {
            Manager.Instance.CmdSwitchEvent.Invoke(_caseList.ToArray());

            _isCalled = true;
            return true;    // can play more.
        }

        int selectedKey = Manager.Instance.Object.CaseKey;
        if (false == isValid(selectedKey))
        {
            Log.Error(string.Format("invalid key: {0}", selectedKey));
            return false;
        }
        
        CmdCase cmdCase = _caseList[selectedKey];
        return cmdCase.Play();
    }

    public override string ToString()
    {
        string s = string.Format(
            "CharacterId : {0}, Case Count : {1}",
            CharacterId, _caseList.Count);

        return ToString(s);
    }


    private bool isValid(int caseKey)
    {
        if (caseKey < 0)
            return false;
        else if (caseKey >= _caseList.Count)
            return false;
        else
            return true;
    }
}
