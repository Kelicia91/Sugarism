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
        Log.Error("not supported Show()");
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
