using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class AlbumMiniCGPanel : Panel, IPointerClickHandler
{
    /********* Editor Interface *********/
    // objects
    [SerializeField]
    private Image Image = null;

    
    public void Show(Sprite s)
    {
        Image.sprite = s;

        Show();
    }
    
    // 오브젝트에서 포인터를 누르고 동일한 오브젝트에서 뗄 때 호출됩니다.
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        Hide();
    }

}   // class
