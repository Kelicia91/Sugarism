using System;
using UnityEngine;


public class ActionListPanel : Panel
{
    /********* Editor Interface *********/
    // prefabs
    public GameObject PrefActionScrollView;

    //
    private ActionScrollView[] _viewArray = null;


    // Use this for initialization
    void Awake()
    {
	    if (null == PrefActionScrollView)
        {
            Log.Error("not found prefab action scroll view");
            return;
        }

        Array actionTypeArray = Enum.GetValues(typeof(EActionType));
        int NUM_VIEW = (int)EActionType.MAX;

        _viewArray = new ActionScrollView[NUM_VIEW];
        for (int i = 0; i < NUM_VIEW; ++i)
        {
            GameObject o = Instantiate(PrefActionScrollView);
            o.transform.SetParent(transform, false);

            ActionScrollView view = o.GetComponent<ActionScrollView>();
            EActionType val = (EActionType)actionTypeArray.GetValue(i);
            view.Set(val);

            view.Hide();
            _viewArray[i] = view;
        }
    }

    public void Show(EActionType actionType)
    {
        hideAllChildren();

        if (EActionType.MAX == actionType)
            return;

        int index = (int)actionType;
        _viewArray[index].Show();
    }

    private void hideAllChildren()
    {
        int numViewArray = _viewArray.Length;
        for (int i = 0; i < numViewArray; ++i)
        {
            _viewArray[i].Hide();
        }
    }
}
