using UnityEngine;
using UnityEngine.UI;


public class CustomToggle : MonoBehaviour
{
    /********* Editor Interface *********/
    // objects
    public Image Image;
    public Text Text;

    //
    private Toggle _toggle = null;
    public Toggle Toggle { get { return _toggle; } }

    //
    private int _index = -1;
    public int Index
    {
        get { return _index; }
        set { _index = value; }
    }


    //
    void Awake()
    {
        _toggle = GetComponent<Toggle>();
    }

    //
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
