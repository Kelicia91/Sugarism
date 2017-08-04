using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UnitAlbumPanel : Panel
{
    /********* Editor Interface *********/
    // prefabs
    [SerializeField]
    private Button PrefBackButton = null;
    [SerializeField]
    private AlbumTypeToggle PrefAlbumTypeToggle = null;
    [SerializeField]
    private CGScrollView PrefCGScrollView = null;
    // objects
    [SerializeField]
    private Image Image = null;
    [SerializeField]
    private Text Text = null;
    [SerializeField]
    private ToggleGroup ToggleGroup = null;
    [SerializeField]
    private GameObject CGScrollViewListPanel = null;

    //
    private int _albumId = AlbumController.MAX_ALBUM_ID;
    public int AlbumId
    {
        get { return _albumId; }
        private set
        {
            _albumId = value;

            //Image.sprite = ;  // @todo: select album image
            Text.text = AlbumController.GetName(_albumId);
        }
    }

    private List<CGScrollView> _cgScrollViewList = null;


    //
    void Awake()
    {
        create();
    }

    private void create()
    {
        // back
        Button backButton = Instantiate(PrefBackButton);
        backButton.transform.SetParent(transform, false);
        backButton.onClick.AddListener(onClickBackButton);
    }


    public void Set(int albumId)
    {
        if (false == AlbumController.IsValid(albumId))
        {
            Log.Error(string.Format("invalid album id; {0}", albumId));
            return;
        }

        AlbumId = albumId;
        initialize();
    }

    private void initialize()
    {
        _cgScrollViewList = new List<CGScrollView>();

        if (AlbumController.ETC_ALBUM_ID == AlbumId)
            initEtc();
        else if (AlbumController.MAIN_CHARACTER_ALBUM_ID == AlbumId)
            initMainCharacter();
        else
            initTarget(AlbumId);
    }


    private void initEtc()
    {
        addAlbumType(AlbumController.EAlbumType.PICTURE);
        addAlbumType(AlbumController.EAlbumType.MINI_PICTURE);        
    }

    private void initMainCharacter()
    {
        addAlbumType(AlbumController.EAlbumType.NURTURE_ENDING);
        addAlbumType(AlbumController.EAlbumType.VACATION);
    }
    
    private void initTarget(int targetId)
    {
        addAlbumType(AlbumController.EAlbumType.PICTURE);
        addAlbumType(AlbumController.EAlbumType.MINI_PICTURE);
    }

    private void addAlbumType(AlbumController.EAlbumType albumType)
    {
        CGScrollView scrollView = Instantiate(PrefCGScrollView);
        scrollView.transform.SetParent(CGScrollViewListPanel.transform, false);
        scrollView.Set(AlbumId, albumType);
        scrollView.Hide();

        _cgScrollViewList.Add(scrollView);

        AlbumTypeToggle toggle = Instantiate(PrefAlbumTypeToggle);
        toggle.transform.SetParent(ToggleGroup.transform, false);
        toggle.Set(ToggleGroup);
        toggle.Set(this, albumType);
    }

    //
    public void ShowCGList(AlbumController.EAlbumType albumType)
    {
        hideAllCGScrollView();

        CGScrollView scrollView = getCGScrollView(albumType);
        if (null == scrollView)
        {
            Log.Error(string.Format("not found CGScrollView; {0}", albumType));
            return;
        }

        scrollView.Show();
    }

    private void hideAllCGScrollView()
    {
        int count = _cgScrollViewList.Count;
        for (int i = 0; i < count; ++i)
        {
            _cgScrollViewList[i].Hide();
        }
    }

    private CGScrollView getCGScrollView(AlbumController.EAlbumType albumType)
    {
        int count = _cgScrollViewList.Count;
        for (int i = 0; i < count; ++i)
        {
            if (_cgScrollViewList[i].AlbumType == albumType)
                return _cgScrollViewList[i];
        }

        return null;
    }

    //
    private void onClickBackButton()
    {
        Hide();
        LobbyManager.Instance.UI.AlbumPanel.ShowAlbumList();
    }
}
