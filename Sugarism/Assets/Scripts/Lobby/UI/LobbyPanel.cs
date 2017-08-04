using UnityEngine;


public class LobbyPanel : Panel
{
    /********* Editor Interface *********/
    // objects
    [SerializeField]
    private TextButton StartButton = null;
    [SerializeField]
    private TextButton ContinueButton = null;
    [SerializeField]
    private TextButton AlbumButton = null;


    // 
    void Start()
    {
        StartButton.SetText(Def.CMD_NEW_START);
        StartButton.AddClick(onClickStart);
        
        ContinueButton.SetText(Def.CMD_CONTINUE);
        ContinueButton.AddClick(onClickContinue);

        AlbumButton.SetText(Def.CMD_ALBUM);
        AlbumButton.AddClick(onClickAlbum);
    }

    private void onClickStart()
    {
        LobbyManager.Instance.UI.FormPanel.Show();
    }

    private void onClickContinue()
    {
        LobbyManager.Instance.UI.HideAllPanel();
        LobbyManager.Instance.Continue();
    }

    private void onClickAlbum()
    {
        LobbyManager.Instance.UI.AlbumPanel.Show();
    }
}
