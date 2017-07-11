using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CmdButton : MonoBehaviour
{
    // prefabs
    public Image IconImage;
    public Text Text;

    //
    private Button _button = null;

    //
    void Awake()
    {
        _button = GetComponent<Button>();
    }

    public void AddClickListener(UnityEngine.Events.UnityAction clickHandler)
    {
        if (null == _button)
        {
            Log.Error("not found button component");
            return;
        }

        if (null == clickHandler)
        {
            Log.Error("not found click handler");
            return;
        }

        _button.onClick.AddListener(clickHandler);
    }

    public void SetIcon(Sprite s)
    {
        if (null == IconImage)
        {
            Log.Error("not found cmd button's icon image");
            return;
        }

        if (null == s)
        {
            Log.Error("not found icon sprite");
            return;
        }

        IconImage.sprite = s;
    }

    public void SetText(string s)
    {
        if (null == Text)
        {
            Log.Error("not found cmd button's text");
            return;
        }

        if (null == s)
        {
            Log.Error("not found string for text");
            return;
        }

        Text.text = s;
    }
}
