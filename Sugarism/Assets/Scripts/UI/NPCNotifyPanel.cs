using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class NPCNotifyPanel : Panel, IPointerClickHandler
{
    /********* Editor Interface *********/
    // prefabs
    public Image Image;
    public Text NameText;
    public Text LinesText;

    //
    private UnityEngine.Events.UnityAction _clickHandler = null;


    public override void Show()
    {
        return; // blocking base.Show()
    }

    public void Show(int npcId, string lines, UnityEngine.Events.UnityAction clickHandler)
    {
        NPC npc = Manager.Instance.DTNPC[npcId];
        NameText.text = npc.name;

        LinesText.text = lines;

        _clickHandler = clickHandler;

        base.Show();
    }

    // 오브젝트에서 포인터를 누르고 동일한 오브젝트에서 뗄 때 호출됩니다.
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        Log.Debug("clicked NPCNotifyPanel");

        Hide();

        if (null != _clickHandler)
            _clickHandler.Invoke();
        else
            Log.Debug("not found click handler");
    }
}
