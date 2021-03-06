﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ClearPanel : Panel, IPointerClickHandler
{
    // Use this for initialization
    void Start () {
		
	}

    // 오브젝트에서 포인터를 누르고 동일한 오브젝트에서 뗄 때 호출됩니다.
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        Log.Debug("clicked clear panel");

        if (null != Manager.Instance.UI.StoryPanel.MiniPicturePanel)
            Manager.Instance.UI.StoryPanel.MiniPicturePanel.Hide();

        Manager.Instance.Object.StoryMode.NextCmd();
    }
}
