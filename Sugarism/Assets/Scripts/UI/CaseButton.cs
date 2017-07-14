using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CaseButton : MonoBehaviour
{
    /********* Editor Interface *********/
    // prefabs
    public Text Text;

    //
    private int _key = -1;
    private Button _button = null;


	// Use this for initialization
	void Start ()
    {
        _button = GetComponent<Button>();
        if (null == _button)
        {
            Log.Error("not found button component");
            return;
        }

        _button.onClick.AddListener(onClick);
	}
	
	
    public void Set(int key, string description)
    {
        _key = key;
        set(description);
    }

    private void set(string s)
    {
        if (null == Text)
        {
            Log.Error("not found text in case button");
            return;
        }

        Text.text = s;
    }

    private void onClick()
    {
        SwitchPanel switchPanel = Manager.Instance.UI.StoryPanel.SwitchPanel;
        Story.Mode storyMode = Manager.Instance.Object.StoryMode;

        switchPanel.HideAllCase();

        storyMode.CaseKey = _key;
        storyMode.NextCmd();

        switchPanel.Hide();
    }
}
