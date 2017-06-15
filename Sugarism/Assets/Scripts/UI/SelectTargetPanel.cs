using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SelectTargetPanel : Panel
{
    /********* Editor Interface *********/
    // prefabs
    public GameObject PrefBackButton;
    public GameObject PrefSelectTargetButton;
    //
    public ScrollRect ScrollView;


    // Use this for initialization
    void Start ()
    {
        create(PrefBackButton, onClickBackButton);
        create(PrefSelectTargetButton);
    }

	// @todo: panel 클래스로 올리는게 나을지도.
    private void create(GameObject prefab, UnityEngine.Events.UnityAction onClickHandler)
    {
        if (null == prefab)
        {
            Log.Error("not found prefab");
            return;
        }

        GameObject o = Instantiate(PrefBackButton);
        o.transform.SetParent(transform, false);

        Button backButton = o.GetComponent<Button>();
        if (null == backButton)
        {
            Log.Error("not found button component");
            return;
        }

        backButton.onClick.AddListener(onClickHandler);
    }

    private void create(GameObject prefab)
    {
        if (null == prefab)
        {
            Log.Error("not found prefab");
            return;
        }
        else if (null == prefab.GetComponent<SelectTargetButton>())
        {
            Log.Error("not found select target component");
            return;
        }

        if (null == ScrollView)
        {
            Log.Error("not found scroll view");
            return;
        }
        
        RectTransform parent = ScrollView.content;

        int numOfTarget = Manager.Instance.DTTarget.Count;        
        for (int i = 0; i < numOfTarget; ++i)
        {
            GameObject o = Instantiate(PrefSelectTargetButton);
            o.transform.SetParent(parent, false);

            SelectTargetButton btn = o.GetComponent<SelectTargetButton>();
            btn.Set(i);
        }
    }

	private void onClickBackButton()
    {
        Hide();
        Manager.Instance.UI.MainPanel.Show();
    }
}
