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
    private TextButton GalleryButton = null;


    // 
    void Start()
    {
        StartButton.SetText(Def.CMD_NEW_START);
        StartButton.AddClick(onClickStart);
        
        ContinueButton.SetText(Def.CMD_CONTINUE);
        ContinueButton.AddClick(onClickContinue);

        GalleryButton.SetText(Def.CMD_GALLERY);
        GalleryButton.AddClick(onClickGallery);
    }

    private void onClickStart()
    {
        Hide();
        LobbyManager.Instance.UI.FormPanel.Show();
    }

    private void onClickContinue()
    {
        Hide();
        LobbyManager.Instance.Continue();
    }

    private void onClickGallery()
    {
        Log.Debug("click gallery");
    }
}
