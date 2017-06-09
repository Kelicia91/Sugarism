using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CmdLines : Command
{
    private Sugarism.CmdLines _model;

    public CmdLines(Sugarism.CmdLines model) : base(model)
    {
        _model = model;
    }


    #region Property

    public int CharacterId
    {
        get { return _model.CharacterId; }
    }

    public string Lines
    {
        get { return _model.Lines; }
    }

    public bool IsAnonymous
    {
        get { return _model.IsAnonymous; }
    }

    public Sugarism.EFace Face
    {
        get { return _model.Face; }
    }

    public Sugarism.ECostume Costume
    {
        get { return _model.Costume; }
    }

    public Sugarism.EPosition Position
    {
        get { return _model.Position; }
    }

    public Sugarism.ELinesEffect LinesEffect
    {
        get { return _model.LinesEffect; }
    }

    #endregion


    public override void Execute()
    {
        Log.Debug(ToString());
    }

    public override bool Play()
    {
        Log.Debug(ToString());

        Manager.Instance.CmdLinesEvent.Invoke(CharacterId, Lines, IsAnonymous,
                                            Face, Costume, Position, LinesEffect);

        return false;   // no more child to play
    }

    public override string ToString()
    {
        string s = string.Format(
                    "CharacterId: {0}, IsAnonymous: {1}, Face: {2}, Costume: {3}, Pos: {4}, LinesEffect: {5}, Lines: \"{6}\"",
                    CharacterId, IsAnonymous, Face, Costume, Position, LinesEffect, Lines);

        return ToString(s);
    }
}
