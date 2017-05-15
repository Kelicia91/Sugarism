using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ClearPanel : MonoBehaviour, IPointerClickHandler
{
    // Use this for initialization
    void Start () {
		
	}

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        Log.Debug("clicked clear panel");
    }
}
