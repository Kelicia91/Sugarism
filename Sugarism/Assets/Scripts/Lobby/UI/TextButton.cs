using UnityEngine;
using UnityEngine.UI;


public class TextButton : MonoBehaviour
{
    //
    private Text _text = null;
    private Button _button = null;

    //
    void Awake()
    {
        _text = GetComponentInChildren<Text>();
        _button = GetComponent<Button>();
    }

    public void AddClick(UnityEngine.Events.UnityAction listener)
    {
        if (null == _button)
        {
            Log.Error("not found button component");
            return;
        }

        _button.onClick.AddListener(listener);
    }

    public void SetText(string s)
    {
        if (null == _text)
        {
            Log.Error("not found text");
            return;
        }

        _text.text = s;
    }
}
