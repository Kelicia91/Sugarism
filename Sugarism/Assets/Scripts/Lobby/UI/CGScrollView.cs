using UnityEngine;
using UnityEngine.UI;


public class CGScrollView : Panel
{
    /********* Editor Interface *********/
    // prefabs
    [SerializeField]
    private CGThumbnailButton PrefCGThumbnailButton = null;

    //
    private int _albumId = AlbumController.MAX_ALBUM_ID;
    public int AlbumId
    {
        get { return _albumId; }
        private set { _albumId = value; }
    }

    private AlbumController.EAlbumType _albumType = AlbumController.EAlbumType.MAX;
    public AlbumController.EAlbumType AlbumType
    {
        get { return _albumType; }
        private set { _albumType = value; }
    }

    //
    private RectTransform _content = null;
    private int _buttonCount = 0;


    //
    void Awake()
    {
        ScrollRect scrollRect = GetComponent<ScrollRect>();
        _content = scrollRect.content;
    }

    // called after Drawing.
    void Start()
    {
        setContentHeight(_buttonCount);
    }

    // @note : called before Start(), after OnEnabled()
    public void Set(int albumId, AlbumController.EAlbumType albumType)
    {
        if (false == AlbumController.IsValid(albumId))
        {
            Log.Error(string.Format("invalid album id; {0}", albumId));
            return;
        }

        if (AlbumController.EAlbumType.MAX == albumType)
        {
            Log.Error("invalid album type");
            return;
        }

        AlbumId = albumId;
        AlbumType = albumType;

        _buttonCount = createButtons();
    }

    private int createButtons()
    {
        int createdButtonCount = 0;

        switch (AlbumType)
        {
            case AlbumController.EAlbumType.PICTURE:
                createdButtonCount = initPicture();
                break;

            case AlbumController.EAlbumType.MINI_PICTURE:
                createdButtonCount = initMiniPicture();
                break;

            case AlbumController.EAlbumType.NURTURE_ENDING:
                createdButtonCount = initNurtureEnding();
                break;

            case AlbumController.EAlbumType.VACATION:
                createdButtonCount = initVacation();
                break;

            default:
                break;
        }

        return createdButtonCount;
    }


    private int initPicture()
    {
        int createdButtonCount = 0;

        PictureObject pictureTable = LobbyManager.Instance.DT.Picture;

        int count = pictureTable.Count;
        for (int id = 0; id < count; ++id)
        {
            if (AlbumId != pictureTable[id].albumId)
                continue;

            string key = PlayerPrefsKey.GetKey(PlayerPrefsKey.ISLOCKED_PICTURE, id);
            int value = CustomPlayerPrefs.GetInt(key, PlayerPrefsKey.TRUE_INTEGER);
            bool isLocked = PlayerPrefsKey.GetIntToBool(value);

            CGThumbnailButton btn = create();
            btn.Set(pictureTable[id].sprite, AlbumType, isLocked);

            ++createdButtonCount;
        }

        return createdButtonCount;
    }

    private int initMiniPicture()
    {
        int createdButtonCount = 0;

        MiniPictureObject miniPictureTable = LobbyManager.Instance.DT.MiniPicture;

        int count = miniPictureTable.Count;
        for (int id = 0; id < count; ++id)
        {
            if (AlbumId != miniPictureTable[id].albumId)
                continue;

            string key = PlayerPrefsKey.GetKey(PlayerPrefsKey.ISLOCKED_MINIPICTURE, id);
            int value = CustomPlayerPrefs.GetInt(key, PlayerPrefsKey.TRUE_INTEGER);
            bool isLocked = PlayerPrefsKey.GetIntToBool(value);

            CGThumbnailButton btn = create();
            btn.Set(miniPictureTable[id].sprite, AlbumType, isLocked);

            ++createdButtonCount;
        }

        return createdButtonCount;
    }

    private int initNurtureEnding()
    {
        int createdButtonCount = 0;

        NurtureEndingObject nurtureEndingTable = LobbyManager.Instance.DT.NurtureEnding;

        int count = nurtureEndingTable.Count;
        for (int id = 0; id < count; ++id)
        {
            string key = PlayerPrefsKey.GetKey(PlayerPrefsKey.ISLOCKED_NURTURE_ENDING, id);
            int value = CustomPlayerPrefs.GetInt(key, PlayerPrefsKey.TRUE_INTEGER);
            bool isLocked = PlayerPrefsKey.GetIntToBool(value);

            CGThumbnailButton btn = create();
            btn.Set(nurtureEndingTable[id].image, AlbumType, isLocked);

            ++createdButtonCount;
        }

        return createdButtonCount;
    }

    private int initVacation()
    {
        int createdButtonCount = 0;

        VacationObject vacationTable = LobbyManager.Instance.DT.Vacation;

        int count = vacationTable.Count;

        // child
        for (int id = 0; id < count; ++id)
        {
            string key = PlayerPrefsKey.GetKey(PlayerPrefsKey.ISLOCKED_VACATION_CHILD, id);
            int value = CustomPlayerPrefs.GetInt(key, PlayerPrefsKey.TRUE_INTEGER);
            bool isLocked = PlayerPrefsKey.GetIntToBool(value);

            CGThumbnailButton btn = create();
            btn.Set(vacationTable[id].childHood, AlbumType, isLocked);

            ++createdButtonCount;
        }

        // adult
        for (int id = 0; id < count; ++id)
        {
            string key = PlayerPrefsKey.GetKey(PlayerPrefsKey.ISLOCKED_VACATION_ADULT, id);
            int value = CustomPlayerPrefs.GetInt(key, PlayerPrefsKey.TRUE_INTEGER);
            bool isLocked = PlayerPrefsKey.GetIntToBool(value);

            CGThumbnailButton btn = create();
            btn.Set(vacationTable[id].adultHood, AlbumType, isLocked);

            ++createdButtonCount;
        }

        return createdButtonCount;
    }

    //
    private CGThumbnailButton create()
    {
        CGThumbnailButton btn = Instantiate(PrefCGThumbnailButton);
        btn.transform.SetParent(_content, false);
        return btn;
    }


    // for ScrollView
    // Calculate content's height for activating vertical scrollbar
    private void setContentHeight(int cellCount)
    {
        GridLayoutGroup gridLayoutGroup = _content.GetComponent<GridLayoutGroup>();

        float viewportWidth = GetComponent<RectTransform>().rect.width;
        viewportWidth -= gridLayoutGroup.padding.left;
        viewportWidth -= gridLayoutGroup.padding.right;
        viewportWidth += gridLayoutGroup.spacing.x;

        int columnCountInGrid = (int)(viewportWidth / (gridLayoutGroup.cellSize.x + gridLayoutGroup.spacing.x));
        if (columnCountInGrid < 0)
        {
            Log.Error("invalid column count(CGThumbnailButton)");
            return;
        }

        int rowCount = cellCount / columnCountInGrid;

        int remainder = cellCount % columnCountInGrid;
        if (remainder > 0)
            ++rowCount;

        float width = _content.sizeDelta.x;
        float height = gridLayoutGroup.padding.top + gridLayoutGroup.padding.bottom
                    + (gridLayoutGroup.cellSize.y * rowCount)
                    + (gridLayoutGroup.spacing.y * (rowCount - 1));

        _content.sizeDelta = new Vector2(width, height);
    }

}   // class
