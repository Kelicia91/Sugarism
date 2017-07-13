using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ActionScrollView : Panel
{
    /********* Editor Interface *********/
    // prefabs
    public GameObject PrefActionButton;

    //
    private EActionType _actionType = EActionType.MAX;

    private RectTransform _content = null;
    private GridLayoutGroup _gridLayoutGroup = null;

    private GameObject[] _childObject = null;


    // Use this for initialization
    void Awake ()
    {
        ScrollRect scrollRect = GetComponent<ScrollRect>();
        if (null != scrollRect)
        {
            _content = scrollRect.content;

            _gridLayoutGroup = _content.gameObject.GetComponent<GridLayoutGroup>();
        }
        else
        {
            Log.Error("not found scroll rect");
        }
    }

    // called after Drawing.
    void Start()
    {
        if (null == PrefActionButton)
            Log.Error("not found prefab action button");
        else
            createActionButtons();

        setContentHeight();
    }

    // @note : called before Start(), after OnEnabled()
    public void Set(EActionType actionType)
    {
        if (EActionType.MAX == actionType)
        {
            Log.Error("invalid action type");
            return;
        }

        _actionType = actionType;
    }

    private void createActionButtons()
    {
        List<int> actionIdList = null;
        get(_actionType, out actionIdList);
        if (null == actionIdList)
            return;

        int numAction = actionIdList.Count;
        if (numAction <= 0)
            return;

        _childObject = new GameObject[numAction];
        for (int i = 0; i < numAction; ++i)
        {
            GameObject o = Instantiate(PrefActionButton);
            o.transform.SetParent(_content, false);

            _childObject[i] = o;

            ActionButton btn = o.GetComponent<ActionButton>();
            btn.SetActionId(actionIdList[i]);
        }
    }
    
    // Calculate content's height for activating vertical scrollbar
    private void setContentHeight()
    {
        if (null == _gridLayoutGroup)
        {
            Log.Error("not found grid layout group");
            return;
        }

        if (null == _content)
        {
            Log.Error("not found content");
            return;
        }

        RectTransform contentRect = _content.gameObject.GetComponent<RectTransform>();

        int buttonCount = _childObject.Length;

        float viewportWidth = GetComponent<RectTransform>().rect.width;
        viewportWidth -= _gridLayoutGroup.padding.left;
        viewportWidth -= _gridLayoutGroup.padding.right;
        viewportWidth += _gridLayoutGroup.spacing.x;
        int columnCountInGrid = (int) (viewportWidth / (_gridLayoutGroup.cellSize.x + _gridLayoutGroup.spacing.x));

        if (columnCountInGrid <= 0)
        {
            Log.Error("invalid column(acton button) count");
            return;
        }

        int rowCount = buttonCount / columnCountInGrid;

        int remainder = buttonCount % columnCountInGrid;
        if (remainder > 0)
            ++rowCount;

        float width = contentRect.sizeDelta.x;
        float height = _gridLayoutGroup.padding.top + _gridLayoutGroup.padding.bottom
                    + (_gridLayoutGroup.cellSize.y * rowCount)
                    + (_gridLayoutGroup.spacing.y * (rowCount - 1));

        contentRect.sizeDelta = new Vector2(width, height);
    }

    private void get(EActionType actionType, out List<int> actionIdList)
    {
        actionIdList = new List<int>();

        // query : select row.id from DTAction where row.type = actionType
        int actionCount = Manager.Instance.DTAction.Count;
        for (int i = 0; i < actionCount; ++i)
        {
            if (actionType == Manager.Instance.DTAction[i].type)
            {
                actionIdList.Add(i);
            }
        }
    }
}
