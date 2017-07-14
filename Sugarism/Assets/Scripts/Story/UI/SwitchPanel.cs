using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchPanel : Panel
{
    /********* Editor Interface *********/
    // prefabs
    public GameObject PrefCaseButton;
    public GameObject CasePanel;

    //
    private CaseButton[] _caseBtnArray = null;


    // Use this for initialization
    void Awake()
    {
        create();

        Manager.Instance.Object.StoryMode.CmdSwitchEvent.Attach(onCmdSwitch);

        Hide();
    }


    public void HideAllCase()
    {
        if (null == _caseBtnArray)
            return;

        int numCaseBtnArray = _caseBtnArray.Length;
        for (int i = 0; i < numCaseBtnArray; ++i)
            _caseBtnArray[i].gameObject.SetActive(false);
    }


    private void create()
    {
        if (null == PrefCaseButton)
        {
            Log.Error("not found prefab case button object");
            return;
        }

        if (null == PrefCaseButton.GetComponent<CaseButton>())
        {
            Log.Error("not found prefab case button component");
            return;
        }

        if (null == CasePanel)
        {
            Log.Error("not found case panel");
            return;
        }

        _caseBtnArray = new CaseButton[Sugarism.CmdSwitch.MAX_COUNT_CASE];

        int numCaseBtnArray = _caseBtnArray.Length;
        for (int i = 0; i < numCaseBtnArray; ++i)
        {
            GameObject o = Instantiate(PrefCaseButton);
            o.transform.SetParent(CasePanel.transform, false);
            o.SetActive(false);

            CaseButton btn = o.GetComponent<CaseButton>();
            _caseBtnArray[i] = btn;
            _caseBtnArray[i].Set(-1, string.Empty);
        }
    }


    private void onCmdSwitch(Story.CmdCase[] caseArray)
    {
        int numCaseArray = caseArray.Length;
        int numCaseBtnArray = _caseBtnArray.Length;

        if (numCaseArray > numCaseBtnArray)
        {
            Log.Error("invalid case count; bigger then max case count");
            return;
        }

        for (int i = 0; i < numCaseArray; ++i)
        {
            Story.CmdCase c = caseArray[i];
            _caseBtnArray[i].Set(c.Key, c.Description);
            _caseBtnArray[i].gameObject.SetActive(true);
        }

        for (int i = numCaseArray; i < numCaseBtnArray; ++i)
        {
            _caseBtnArray[i].Set(-1, string.Empty);
            _caseBtnArray[i].gameObject.SetActive(false);
        }

        Show();
    }
}
