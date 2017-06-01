using System.Collections.Generic;
using UnityEngine;


public class SelectActionPanel : Panel
{
    /********* Editor Interface *********/
    // prefabs
    public GameObject PrefSelectActionButton;

    //
    private const float BUTTON_SPACE_X = 110.0f;
    private const float BUTTON_SPACE_Y = 70.0f;

    private const int NUM_BUTTON = 6;
    private SelectActionButton[] _btnArray = null;


    // Use this for initialization
    void Awake()
    {
        // create buttons
        _btnArray = new SelectActionButton[NUM_BUTTON];
        for (int i = 0; i < NUM_BUTTON; ++i)
        {
            GameObject o = Instantiate(PrefSelectActionButton);
            SelectActionButton btn = o.GetComponent<SelectActionButton>();
            _btnArray[i] = btn;

            _btnArray[i].transform.SetParent(transform, false);
        }
    }

    public override void Show()
    {
        EActionType actionType = EActionType.MAX;// Manager.Instance.UI.SelectActionTypePanel.SelectedActionType;
        if (EActionType.MAX == actionType)
        {
            Log.Error("invalid action type");
            return;
        }

        List<int> actionIdList = new List<int>();
        get(actionType, ref actionIdList);
        if (null == actionIdList)
            return;

        // @note : Last button is BACK button.
        if (actionIdList.Count >= NUM_BUTTON)
        {
            string errMsg = string.Format("too much actionIdList.count '{0}' of {1}, actionButton.maxCount {2}",
                                        actionIdList.Count, actionType.ToString(), (NUM_BUTTON - 1));
            Log.Error(errMsg);
            return;
        }

        init();
        set(actionIdList);

        layout(actionIdList.Count);
        show(actionIdList.Count);

        base.Show();
    }

    private void get(EActionType actionType, ref List<int> actionIdList)
    {
        // query : select row.id from DTAction where row.type = actionType
        for (int i = 0; i < Manager.Instance.DTAction.Count; ++i)
        {
            if (actionType == Manager.Instance.DTAction[i].type)
            {
                actionIdList.Add(i);
            }
        }
    }

    private void init()
    {
        for (int i = 0; i < NUM_BUTTON; ++i)
        {
            _btnArray[i].SetActionId(-1);
            _btnArray[i].gameObject.SetActive(false);
        }
    }

    private void layout(int numOfAction)
    {
        const float x = -1.0f * BUTTON_SPACE_X;

        // handle special case
        if (numOfAction == 0)
        {
            //  ㅁ
            float center_pos_x = x / 2.0f;
            float center_pos_y = BUTTON_SPACE_Y;

            set(0, new Vector2(center_pos_x, center_pos_y));

            _btnArray[0].gameObject.SetActive(true);
            return;
        }

        // layout (num of button : 2~MAX) -> from left to right, from top to bottom.
        float y = 0.0f;
        if (numOfAction == 1)
        {
            // ㅁㅁ
            y = BUTTON_SPACE_Y;
        }
        else if (numOfAction == 2)
        {
            // ㅁㅁ
            //   ㅁ
            y = (BUTTON_SPACE_Y + (BUTTON_SPACE_Y * 2.0f)) / 2.0f;
        }
        else if (numOfAction == 3)
        {
            // ㅁㅁ
            // ㅁㅁ
            y = (BUTTON_SPACE_Y + (BUTTON_SPACE_Y * 2.0f)) / 2.0f;
        }
        else if (numOfAction == 4)
        {
            // ㅁㅁ
            // ㅁㅁ
            //   ㅁ
            y = BUTTON_SPACE_Y * 2.0f;
        }
        else
        {
            // ㅁㅁ
            // ㅁㅁ   // = max
            // ㅁㅁ
            y = BUTTON_SPACE_Y * 2.0f;
        }
        
        // button index : even
        float tempY = y;
        for (int i = 0; i < numOfAction; i = i + 2)
        {
            set(i, new Vector2(x, tempY));
            tempY -= BUTTON_SPACE_Y;
        }

        // button index : odd
        tempY = y;
        for (int i = 1; i < numOfAction; i = i + 2)
        {
            set(i, new Vector2(0.0f, tempY));
            tempY -= BUTTON_SPACE_Y;
        }

        // button index : last
        int backBtnIndex = numOfAction;
        set(backBtnIndex, new Vector2(0.0f, tempY));
    }

    private void set(List<int> actionIdList)
    {
        for (int i = 0; i < actionIdList.Count; ++i)
        {
            _btnArray[i].SetActionId(actionIdList[i]);
        }
    }

    private void show(int numOfAction)
    {
        // contains BACK button
        for (int i = 0; i <= numOfAction; ++i)
        {
            _btnArray[i].gameObject.SetActive(true);
        }
    }

    private void set(int btnIndex, Vector2 pos)
    {
        RectTransform rectTransform = _btnArray[btnIndex].GetComponent<RectTransform>();
        rectTransform.anchoredPosition = pos;
    }
}
