using UnityEngine;
using UnityEngine.UI;


public class ScheduleProgressPanel : Panel
{
    /********* Editor Interface *********/
    // prefabs
    public Image ActionIconImage;
    public Text ActionNameText;

    public GameObject ActionProgressPanel;
    public Text ProgressDescriptionText;


    //
    void OnEnable()
    {
        ActionIconImage.enabled = false;

        setActionNameText(string.Empty);

        HideActionProgressPanel();
    }

    public void OnActionStart(Sprite actionIcon, string actionName)
    {
        setActionIconImage(actionIcon);
        ActionIconImage.enabled = true;

        setActionNameText(actionName);
    }

    private void setActionIconImage(Sprite s)
    {
        if (null == ActionIconImage)
        {
            Log.Error("not found action icon image");
            return;
        }

        ActionIconImage.sprite = s;
    }

    private void setActionNameText(string s)
    {
        if (null == ActionNameText)
        {
            Log.Error("not found action name text");
            return;
        }

        ActionNameText.text = s;
    }

    public void HideActionProgressPanel()
    {
        if (null == ActionProgressPanel)
        {
            Log.Error("not found action progress panel");
            return;
        }

        ActionProgressPanel.gameObject.SetActive(false);
    }

    public void SetProgressDescriptionText(string s)
    {
        if (null == ProgressDescriptionText)
        {
            Log.Error("not found progress description text");
            return;
        }

        ProgressDescriptionText.text = s;

        ActionProgressPanel.gameObject.SetActive(true);
    }
}
