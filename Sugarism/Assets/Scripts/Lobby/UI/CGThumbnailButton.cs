using UnityEngine;
using UnityEngine.UI;


public class CGThumbnailButton : MonoBehaviour
{
    /********* Editor Interface *********/
    // objects
    [SerializeField]
    private GameObject LockPanel = null;

    //
    private Image _image = null;
    private Button _button = null;

    //
    private Sprite _sprite = null;
    private AlbumController.EAlbumType _albumType = AlbumController.EAlbumType.MAX;


    //
    void Awake()
    {
        _image = GetComponent<Image>();

        _button = GetComponent<Button>();
        _button.onClick.AddListener(onClick);

        _button.interactable = false;   // default lock
    }

    //
    public void Set(Sprite s, AlbumController.EAlbumType albumType, bool isLocked)
    {
        set(s);
        
        _albumType = albumType;

        if (false == isLocked)
            unlock();
    }

    private void set(Sprite s)
    {
        if (null == s)
        {
            Log.Error("not found CGThumbnailButton's sprite");
            return;
        }

        _sprite = s;

        _image.sprite = _sprite;
    }

    private void unlock()
    {
        LockPanel.gameObject.SetActive(false);

        _button.interactable = true;
    }

    private void onClick()
    {
        showCG();
    }
    
    private void showCG()
    {
        switch(_albumType)
        {
            case AlbumController.EAlbumType.PICTURE:
            case AlbumController.EAlbumType.NURTURE_ENDING:
            case AlbumController.EAlbumType.VACATION:
                LobbyManager.Instance.UI.AlbumPanel.CGPanel.ShowFullCG(_sprite);
                break;

            case AlbumController.EAlbumType.MINI_PICTURE:
                LobbyManager.Instance.UI.AlbumPanel.CGPanel.ShowMiniCG(_sprite);
                break;

            default:
                break;
        }
    }

}   // class
