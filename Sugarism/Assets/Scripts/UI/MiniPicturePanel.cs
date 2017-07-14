using UnityEngine;
using UnityEngine.UI;


public class MiniPicturePanel : Panel
{
    /********* Editor Interface *********/
    // prefabs
    public Image Image;


    // Use this for initialization
    void Awake()
    {
        Manager.Instance.Object.StoryMode.CmdMiniPictureEvent.Attach(onCmdMiniPicture);

        Hide();
	}


    private void set(Sprite s)
    {
        if (null == Image)
        {
            Log.Error("not found mini picture image");
            return;
        }

        Image.sprite = s;
    }

    private void onCmdMiniPicture(int id)
    {
        if (false == ExtMiniPicture.IsValid(id))
            return;

        MiniPicture p = Manager.Instance.DTMiniPicture[id];
        set(p.sprite);

        Show();
    }
}
