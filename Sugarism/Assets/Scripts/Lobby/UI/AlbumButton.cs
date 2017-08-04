using UnityEngine;
using UnityEngine.UI;


public class AlbumButton : MonoBehaviour
{
    /********* Editor Interface *********/
    // objects
    [SerializeField]
    private Image Image = null;
    [SerializeField]
    private Text Text = null;

    //
    private Button _button = null;

    //
    private int _albumId = AlbumController.MAX_ALBUM_ID;
    public int AlbumId
    {
        get { return _albumId; }
        private set
        {
            _albumId = value;

            //Image.sprite = ;  // @todo: album icon
            Text.text = AlbumController.GetName(_albumId);
        }
    }

    //
    void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(onClick);
    }

    //
    public void Set(int albumId)
    {
        if (false == AlbumController.IsValid(albumId))
        {
            Log.Error(string.Format("invalid album id; {0}", albumId));
            return;
        }

        AlbumId = albumId;
    }


    private void onClick()
    {
        Log.Debug(string.Format("click album button; Id({0})", AlbumId));

        LobbyManager.Instance.UI.AlbumPanel.ShowUnitAlbum(AlbumId);
    }
}
