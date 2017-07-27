using UnityEngine;
using UnityEngine.UI;


public class CustomToggle : MonoBehaviour
{
    /********* Editor Interface *********/
    // objects
    public Image Image;
    public Text Text;

    //
    private Toggle _component = null;
    public Toggle Component { get { return _component; } }
    
    //
    void Awake()
    {
        _component = GetComponent<Toggle>();
    }

    //
    public void SetColor(Color c)
    {
        Image.color = c;
        Text.color = c;
    }

    public void SetImage(Sprite s)
    {
        if (null == s)
        {
            Log.Error("not found sprite for CustomToggle");
            return;
        }

        if (null == Image)
        {
            Log.Error("not found image component");
            return;
        }

        Image.sprite = s;
    }

    public void SetText(string s)
    {
        if (null == s)
        {
            Log.Error("not found text for CustomToggle");
            return;
        }

        if (null == Text)
        {
            Log.Error("not found text component");
            return;
        }

        Text.text = s;
    }
}
