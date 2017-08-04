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


    // Use this for initialization
    void Awake ()
    {
        ScrollRect scrollRect = GetComponent<ScrollRect>();
        _content = scrollRect.content;
    }

    // called after Drawing.
    void Start()
    {
        if (null == PrefActionButton)
        {
            Log.Error("not found prefab action button");
            return;
        }

        int createdButtonCount = createActionButtons();
        setContentHeight(createdButtonCount);
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

    private int createActionButtons()
    {
        int createdButtonCount = 0;

        ActionObject actionTable = Manager.Instance.DT.Action;

        int count = actionTable.Count;
        for (int id = 0; id < count; ++id)
        {
            if (_actionType != actionTable[id].type)
                continue;

            GameObject o = Instantiate(PrefActionButton);
            o.transform.SetParent(_content, false);

            ActionButton btn = o.GetComponent<ActionButton>();
            btn.SetActionId(id);

            ++createdButtonCount;
        }

        return createdButtonCount;
    }

    // for ScrollView
    // Calculate content's height for activating vertical scrollbar
    private void setContentHeight(int cellCount)
    {
        GridLayoutGroup gridLayoutGroup = _content.GetComponent<GridLayoutGroup>();

        float viewportWidth = GetComponent<RectTransform>().rect.width;
        viewportWidth -= gridLayoutGroup.padding.left;
        viewportWidth -= gridLayoutGroup.padding.right;
        viewportWidth += gridLayoutGroup.spacing.x;

        int columnCountInGrid = (int) (viewportWidth / (gridLayoutGroup.cellSize.x + gridLayoutGroup.spacing.x));
        if (columnCountInGrid <= 0)
        {
            Log.Error("invalid column count(ActionButton)");
            return;
        }

        int rowCount = cellCount / columnCountInGrid;

        int remainder = cellCount % columnCountInGrid;
        if (remainder > 0)
            ++rowCount;

        float width = _content.sizeDelta.x;
        float height = gridLayoutGroup.padding.top + gridLayoutGroup.padding.bottom
                    + (gridLayoutGroup.cellSize.y * rowCount)
                    + (gridLayoutGroup.spacing.y * (rowCount - 1));

        _content.sizeDelta = new Vector2(width, height);
    }
    
}   // class
