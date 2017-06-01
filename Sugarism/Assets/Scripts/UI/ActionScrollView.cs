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
    public float PADDING_X;
    public float PADDING_Y;
    public float SPACE_X_BUTTON;
    public float SPACE_Y_BUTTON;

    //
    private EActionType _actionType = EActionType.MAX;
    private RectTransform _viewport = null;
    private RectTransform _content = null;
    private GameObject[] _childObject = null;


    // Use this for initialization
    void Awake ()
    {
        ScrollRect scrollRect = GetComponent<ScrollRect>();
        if (null != scrollRect)
        {
            _viewport = scrollRect.GetComponent<RectTransform>();
            _content = scrollRect.content;
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
            createActionButton();

        if (null == _viewport)
            Log.Error("not found viewport");
        else if (null == _content)
            Log.Error("not found content");
        else
            layout();
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

    private void createActionButton()
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

    // @note : Call after Drawing, because Get Stretched RectTransfrom Size.
    private void layout()
    {
        int numChildren = _childObject.Length;
        if (numChildren <= 0)
        {
            Log.Error("invalid num of child object");
            return;
        }
        
        // viewport (w,h)
        float viewWidth = _viewport.rect.width;
        float viewHeight = _viewport.rect.height;

        string msg = string.Format("viewport (w,h) = ({0},{1})", viewWidth, viewHeight);
        Log.Debug(msg);

        // button (w,h)
        RectTransform btnRectTransform = PrefActionButton.GetComponent<RectTransform>();
        float btnWidth = btnRectTransform.rect.width;
        float btnHeight = btnRectTransform.rect.height;
                
        // child object area (w,h)
        float deltaWidth = btnWidth + SPACE_X_BUTTON;
        if (deltaWidth <= 0)
        {
            Log.Debug("failed to layout; btnWidth + SPACE_X_BUTTON <= 0");
            return;
        }

        float deltaHeight = btnHeight + SPACE_Y_BUTTON;
        if (deltaHeight <= 0)
        {
            Log.Debug("failed to layout; btnHeight + SPACE_Y_BUTTON <= 0");
            return;
        }

        // num(Column) in viewport
        float quotient = (viewWidth - (2 * PADDING_X) + SPACE_X_BUTTON) / deltaHeight;
        int numViewColumn = Mathf.FloorToInt(quotient);
        
        // num(Column) in content
        int numColumn = numViewColumn;
        if (numColumn <= 0)
        {
            Log.Debug("too small viewport.width; num(Column) <= 0");
            numColumn = 1;
        }

        // num(Row) in content
        int numRow = numChildren / numColumn;
        int remainder = numChildren % numColumn;
        if (remainder > 0)
            numRow += 1;

        // content size; anchor (stretch,center)
        float contentHeight = (2 * PADDING_Y) + (deltaHeight * numRow) - SPACE_Y_BUTTON;
        _content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, contentHeight);

        // child; anchor (top,left), pivot (0,1)
        float posX = 0.0f;
        float posY = 0.0f;
        for (int i = 0; i < numChildren; ++i)
        {
            if (0 == i)
            {
                posX = PADDING_X;
                posY = -1 * PADDING_Y;
            }
            else if (i % numColumn == 0)
            {
                posX = PADDING_X;
                posY -= deltaHeight;
            }
            else
            {
                posX += deltaWidth;
            }

            RectTransform rt = _childObject[i].GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(posX, posY);
        }
    }

    private void get(EActionType actionType, out List<int> actionIdList)
    {
        actionIdList = new List<int>();

        // query : select row.id from DTAction where row.type = actionType
        for (int i = 0; i < Manager.Instance.DTAction.Count; ++i)
        {
            if (actionType == Manager.Instance.DTAction[i].type)
            {
                actionIdList.Add(i);
            }
        }
    }
}
