using UnityEngine;
using UnityEngine.UI;


public class SubmitButton : MonoBehaviour
{
    /********* Editor Interface *********/
    // objects
    public Text Text;

    //
    private Button _button = null;

    //
    void Awake()
    {
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
        if (null == Text)
        {
            Log.Error("not found text");
            return;
        }

        Text.text = s;
    }
}
