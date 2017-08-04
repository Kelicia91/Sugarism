using UnityEngine;
using UnityEngine.UI;


public class AlbumPanel : Panel
{
    /********* Editor Interface *********/
    // prefabs
    [SerializeField]
    private Button PrefBackButton = null;
    [SerializeField]
    private AlbumButton PrefAlbumButton = null;
    [SerializeField]
    private UnitAlbumPanel PrefUnitAlbumPanel = null;
    [SerializeField]
    private AlbumCGPanel PrefAlbumCGPanel = null;
    // objects
    [SerializeField]
    private Text TitleText = null;
    [SerializeField]
    private GameObject ButtonListPanel = null;

    //
    private UnitAlbumPanel _mainCharacterAlbum = null;
    private UnitAlbumPanel _etcAlbum = null;
    private UnitAlbumPanel[] _targetAlbum = null;

    private AlbumCGPanel _cgPanel = null;
    public AlbumCGPanel CGPanel { get { return _cgPanel; } }


    //
    void Awake()
    {
        create();
    }

    // Use this for initialization
    void Start()
    {
        TitleText.text = Def.CMD_ALBUM;
	}

    private void create()
    {
        /***** Back *****/
        Button backButton = Instantiate(PrefBackButton);
        backButton.transform.SetParent(transform, false);
        backButton.onClick.AddListener(onClickBackButton);

        /***** AlbumButton, UnitAlbum *****/
        // etc
        AlbumButton etcButton = Instantiate(PrefAlbumButton);
        etcButton.transform.SetParent(ButtonListPanel.transform, false);
        etcButton.Set(AlbumController.ETC_ALBUM_ID);

        _etcAlbum = Instantiate(PrefUnitAlbumPanel);
        _etcAlbum.transform.SetParent(transform, false);
        _etcAlbum.Set(AlbumController.ETC_ALBUM_ID);
        _etcAlbum.Hide();

        // main character
        AlbumButton mainCharacterButton = Instantiate(PrefAlbumButton);
        mainCharacterButton.transform.SetParent(ButtonListPanel.transform, false);
        mainCharacterButton.Set(AlbumController.MAIN_CHARACTER_ALBUM_ID);

        _mainCharacterAlbum = Instantiate(PrefUnitAlbumPanel);
        _mainCharacterAlbum.transform.SetParent(transform, false);
        _mainCharacterAlbum.Set(AlbumController.MAIN_CHARACTER_ALBUM_ID);
        _mainCharacterAlbum.Hide();

        // targets
        int targetCount = LobbyManager.Instance.DT.Target.Count;
        _targetAlbum = new UnitAlbumPanel[targetCount];
        for (int targetId = 0; targetId < targetCount; ++targetId)
        {
            AlbumButton btn = Instantiate(PrefAlbumButton);
            btn.transform.SetParent(ButtonListPanel.transform, false);
            btn.Set(targetId);

            _targetAlbum[targetId] = Instantiate(PrefUnitAlbumPanel);
            _targetAlbum[targetId].transform.SetParent(transform, false);
            _targetAlbum[targetId].Set(targetId);
            _targetAlbum[targetId].Hide();
        }

        /***** AlbumCGPanel *****/
        _cgPanel = Instantiate(PrefAlbumCGPanel);
        _cgPanel.transform.SetParent(transform, false);
    }

    //
    public void ShowUnitAlbum(int albumId)
    {
        ButtonListPanel.SetActive(false);   // hide

        if (false == AlbumController.IsValid(albumId))
        {
            Log.Error(string.Format("ShowUnitAlbum; invalid album id; {0}", albumId));
            return;
        }

        if (AlbumController.ETC_ALBUM_ID == albumId)
            _etcAlbum.Show();
        else if (AlbumController.MAIN_CHARACTER_ALBUM_ID == albumId)
            _mainCharacterAlbum.Show();
        else
            _targetAlbum[albumId].Show();
    }

    public void ShowAlbumList()
    {
        ButtonListPanel.SetActive(true);   // show
    }

    //
    private void onClickBackButton()
    {
        Hide();
    }
}
