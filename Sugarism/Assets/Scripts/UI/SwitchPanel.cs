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
    private CaseButton[] _caseBtnArray;


    // Use this for initialization
    void Awake()
    {
        create();

        Manager.Instance.CmdSwitchEvent.Attach(onCmdSwitch);

        Hide();
    }


    public void HideAllCase()
    {
        if (null == _caseBtnArray)
            return;

        for (int i = 0; i < _caseBtnArray.Length; ++i)
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

        for (int i = 0; i < _caseBtnArray.Length; ++i)
        {
            GameObject o = Instantiate(PrefCaseButton);
            o.transform.SetParent(CasePanel.transform, false);
            o.SetActive(false);

            CaseButton btn = o.GetComponent<CaseButton>();
            _caseBtnArray[i] = btn;
            _caseBtnArray[i].Set(-1, string.Empty);
        }
    }


    private void onCmdSwitch(CmdCase[] caseArray)
    {
        if (caseArray.Length > _caseBtnArray.Length)
        {
            Log.Error("invalid case count; bigger then max case count");
            return;
        }

        for (int i = 0; i < caseArray.Length; ++i)
        {
            CmdCase c = caseArray[i];
            _caseBtnArray[i].Set(c.Key, c.Description);
            _caseBtnArray[i].gameObject.SetActive(true);
        }

        for (int i = caseArray.Length; i < _caseBtnArray.Length; ++i)
        {
            _caseBtnArray[i].Set(-1, string.Empty);
            _caseBtnArray[i].gameObject.SetActive(false);
        }

        Show();
    }
}
