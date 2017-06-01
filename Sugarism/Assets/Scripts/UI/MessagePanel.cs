using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class MessagePanel : Panel, IPointerClickHandler
{
    /********* Editor Interface *********/
    // prefabs
    public Text MessageText;

    private UnityEngine.Events.UnityAction _clickHandler = null;


    public override void Show()
    {
        return; // blocking base.Show()
    }

    public void Show(string msg 
        , UnityEngine.Events.UnityAction clickHandler)
    {
        MessageText.text = msg;
        _clickHandler = clickHandler;

        base.Show();
    }

    public override void Hide()
    {
        // @todo: UI 컴포넌트들을 가지고 있는 상위 Panel에서
        // 아래 메소드를 사용해 하위 컴포넌트들까지 일괄적으로 alpha 조정 가능한듯.
        // 문제는 알파만 사라지고 setActive(false) 되는 건 아니라서
        // 아래 메소드를 쓰는게 맞을지 코루틴으로 제어해야 될지 고민된다.
        // CrossFadeAlpha()

        base.Hide();
    }

    // 오브젝트에서 포인터를 누르고 동일한 오브젝트에서 뗄 때 호출됩니다.
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        Log.Debug("clicked MessagePanel");
        
        Hide();

        if (null != _clickHandler)
            _clickHandler.Invoke();
        else
            Log.Debug("not found click handler");
    }
}
