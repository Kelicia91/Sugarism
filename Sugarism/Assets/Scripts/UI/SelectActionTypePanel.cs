using System;
using UnityEngine;


public class SelectActionTypePanel : Panel
{
    /********* Editor Interface *********/
    // prefabs
    public GameObject PrefSelectActionTypeButton;

    //
    private const float BUTTON_SPACE_Y = 50.0f;
    private SelectActionTypeButton[] _btnArray = null;

    private EActionType _selectedActionType = EActionType.MAX;
    public EActionType SelectedActionType
    {
        get { return _selectedActionType; }
        set { _selectedActionType = value; }
    }


    // Use this for initialization
    void Awake()
    {
        Array actionTypeArray = Enum.GetValues(typeof(EActionType));

        // create button (action type + back)
        int NUM_BUTTON = (int)EActionType.MAX + 1;

        _btnArray = new SelectActionTypeButton[NUM_BUTTON];
        for (int i = 0; i < NUM_BUTTON; ++i)
        {
            GameObject o = Instantiate(PrefSelectActionTypeButton);
            SelectActionTypeButton btn = o.GetComponent<SelectActionTypeButton>();
            _btnArray[i] = btn;
        }

        // init button : pos.y from top to bottom.
        float button_pos_y = (NUM_BUTTON -1) * BUTTON_SPACE_Y;
        for (int i = 0; i < NUM_BUTTON; ++i)
        {
            _btnArray[i].transform.SetParent(transform, false);
            
            RectTransform rectTransform = _btnArray[i].GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(0.0f, button_pos_y);

            button_pos_y -= BUTTON_SPACE_Y;

            EActionType val = (EActionType)actionTypeArray.GetValue(i);
            _btnArray[i].SetActionType(val);
        }
    }
}
