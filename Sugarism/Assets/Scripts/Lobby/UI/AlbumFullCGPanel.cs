using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class AlbumFullCGPanel : Panel, IPointerClickHandler
{
    /********* Editor Interface *********/
    // prefabs
    [SerializeField]
    private FadePanel PrefFadePanel = null;

    //     
    private FadePanel _fadePanel = null;
    private bool _isHiding = false; // for blocking several input during fade-out

    private Image _image = null;


    //
    void Awake()
    {
        _fadePanel = Instantiate(PrefFadePanel);
        _fadePanel.transform.SetParent(transform, false);

        _image = GetComponent<Image>();

        Hide();
    }

    //
    public void Show(Sprite s)
    {
        _image.sprite = s;

        showFadeIn();
    }

    private void showFadeIn()
    {
        Show();

        _fadePanel.FadeIn();
    }

    private void hideFadeOut()
    {
        _isHiding = true;

        float animLength = _fadePanel.FadeOut();

        Invoke(END_FADE_OUT_METHOD_NAME, animLength);
    }

    private const string END_FADE_OUT_METHOD_NAME = "endFadeOut";
    private void endFadeOut()
    {
        Hide();

        _isHiding = false;
    }

    // 오브젝트에서 포인터를 누르고 동일한 오브젝트에서 뗄 때 호출됩니다.
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (_isHiding)
            return;

        hideFadeOut();
    }

}   // class
