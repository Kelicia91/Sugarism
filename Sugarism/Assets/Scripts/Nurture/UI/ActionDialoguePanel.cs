using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ActionDialoguePanel : Panel, IPointerClickHandler
{
    /********* Editor Interface *********/
    // prefabs
    public CharacterPanel CharacterPanel;
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
        string userName = Manager.Instance.Object.MainCharacter.Name;

        show(userName, lines, clickHandler);
    }

    // for Rival
    public void Show(Rival rival, string lines, UnityEngine.Events.UnityAction clickHandler)
    {
        CharacterPanel.Set(rival);

        Character c = Manager.Instance.DT.Character[rival.characterId];

        show(c.name, lines, clickHandler);
    }

    // for NPC
    public void Show(int npcId, string lines, UnityEngine.Events.UnityAction clickHandler)
    {
        ActionNPC npc = Manager.Instance.DT.ActionNPC[npcId];
        Character c = Manager.Instance.DT.Character[npc.characterId];

        CharacterPanel.Set(c);
        show(c.name, lines, clickHandler);
    }

    private void show(string name, string lines, UnityEngine.Events.UnityAction clickHandler)
    {
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
