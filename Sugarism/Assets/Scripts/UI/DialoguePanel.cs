﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialoguePanel : MonoBehaviour
{
    public Text NameText;
    public Text LinesText;


	// Use this for initialization
	void Start()
    {
        Manager.Instance.CmdLinesEvent.Attach(OnCmdLines);
	}

    //
    void OnDestroy()
    {
        Manager.Instance.CmdLinesEvent.Detach(OnCmdLines);
    }

    

    private void setName(string s)
    {
        if (null == NameText)
            Log.Error("not found name text");
        else
            NameText.text = s;
    }

    private void setLines(string s)
    {
        if (null == LinesText)
            Log.Error("not found lines text");
        else
            LinesText.text = s;
    }


    // CmdLinesEventHandler
    void OnCmdLines(int characterId, string lines)
    {
        Sugarism.Character ch = Manager.Instance.Object.Get(characterId);
        if (null == ch)
        {
            string errMsg = string.Format("not found character {0}", characterId);
            Log.Error(errMsg);
            return;
        }

        setName(ch.Name);
        setLines(lines);
    }
}
