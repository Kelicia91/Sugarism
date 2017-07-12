using UnityEngine;
using UnityEngine.UI;


public class ChargeablePanel : MonoBehaviour
{
    /********* Editor Interface *********/
    // prefabs
    public Image IconImage;
    public Text Text;
    public Button ChargeButton;


    public void AddClickListener(UnityEngine.Events.UnityAction clickHandler)
    {
        if (null == ChargeButton)
        {
            Log.Error("not found charge button");
            return;
        }

        if (null == clickHandler)
        {
            Log.Error("not found click handler");
            return;
        }

        ChargeButton.onClick.AddListener(clickHandler);
    }

    public void SetIcon(Sprite s)
    {
        if (null == IconImage)
        {
            Log.Error("not found image component");
            return;
        }

        if (null == s)
        {
            Log.Error("not found sprite");
            return;
        }

        IconImage.sprite = s;
    }

    public void SetText(string s)
    {
        if (null == Text)
        {
            Log.Error("not found text");
            return;
        }

        if (null == s)
        {
            Log.Error("not found string in text");
            return;
        }

        Text.text = s;
    }
}
