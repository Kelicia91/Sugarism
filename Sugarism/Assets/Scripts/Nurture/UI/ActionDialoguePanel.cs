using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ActionDialoguePanel : Panel, IPointerClickHandler
{
    /********* Editor Interface *********/
    // prefabs
    public Image CharacterImage;
    public Text NameText;
    public Text LinesText;

    //
    private UnityEngine.Events.UnityAction _clickHandler = null;


    public override void Show()
    {
        Log.Error("not supported Show()");
        return; // blocking base.Show()
    }

    // for MainCharacter
    public void Show(string lines, UnityEngine.Events.UnityAction clickHandler)
    {
        Sprite userImage = null;
        string userName = Manager.Instance.Object.MainCharacter.Name;

        show(userImage, userName, lines, clickHandler);
    }

    // for Rival
    public void Show(Rival rival, string lines, UnityEngine.Events.UnityAction clickHandler)
    {
        Sprite rivalImage = null;
        if (Manager.Instance.Object.MainCharacter.IsChildHood())
            rivalImage = rival.childHood;
        else
            rivalImage = rival.adultHood;

        Character c = Manager.Instance.DTCharacter[rival.characterId];

        show(rivalImage, c.name, lines, clickHandler);
    }

    // for NPC
    public void Show(int npcId, string lines, UnityEngine.Events.UnityAction clickHandler)
    {
        ActionNPC npc = Manager.Instance.DTActionNPC[npcId];
        Character c = Manager.Instance.DTCharacter[npc.characterId];

        show(npc.image, c.name, lines, clickHandler);
    }

    private void show(Sprite s, string name, string lines, UnityEngine.Events.UnityAction clickHandler)
    {
        CharacterImage.sprite = s;
        NameText.text = name;
        LinesText.text = lines;

        _clickHandler = clickHandler;

        base.Show();
    }

    // 오브젝트에서 포인터를 누르고 동일한 오브젝트에서 뗄 때 호출됩니다.
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        Log.Debug("clicked ActionDialoguePanel");

        Hide();

        if (null != _clickHandler)
            _clickHandler.Invoke();
        else
            Log.Debug("not found click handler");
    }
}
