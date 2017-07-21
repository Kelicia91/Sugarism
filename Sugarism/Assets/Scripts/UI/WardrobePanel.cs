using UnityEngine;
using UnityEngine.UI;


public class WardrobePanel : Panel
{
    /********* Editor Interface *********/
    //
    public Text TitleText;
    public RectTransform ContentRect;
    public VerticalLayoutGroup VerticalLayoutGroup;
    // prefabs
    public GameObject PrefBackButton;
    public GameObject PrefCostumePanel;

    //
    private Wardrobe _wardrobe = null;

    //
    void Awake()
    {
        _wardrobe = Manager.Instance.Object.MainCharacter.Wardrobe;

        if (null != PrefBackButton)
        {
            GameObject o = Instantiate(PrefBackButton);
            o.transform.SetParent(transform, false);

            Button BackButton = o.GetComponent<Button>();
            BackButton.onClick.AddListener(onClickBackButton);
        }
        else
        {
            Log.Error("not found prefab back button");
        }

        setText(Def.CMD_WARDROBE_NAME);

        if (null == ContentRect)
        {
            Log.Error("not found scroll view's content in wardrobe panel");
            return;
        }

        if (null == PrefCostumePanel)
        {
            Log.Error("not found prefab costume panel");
            return;
        }
        
        createCostumePanel();
    }

    //
    void Start()
    {
        if (null == VerticalLayoutGroup)
        {
            Log.Error("not found vertical layout group");
            return;
        }
        
        RectTransform childRect = PrefCostumePanel.GetComponent<RectTransform>();
        setContentRect(childRect);
    }

    private void createCostumePanel()
    {
        int costumeCount = _wardrobe.CostumeList.Count;
        for (int i = 0; i < costumeCount; ++i)
        {
            /** except default costume **/
            if (Def.DEFAULT_COSTUME_ID == _wardrobe.CostumeList[i].CostumeId)
                continue;

            GameObject o = Instantiate(PrefCostumePanel);

            RectTransform rect = o.GetComponent<RectTransform>();
            rect.SetParent(ContentRect, false);

            CostumePanel costumePanel = o.GetComponent<CostumePanel>();
            costumePanel.Init(_wardrobe.CostumeList[i]);
        }
    }

    private void setContentRect(RectTransform childRect)
    {
        int childCount = ContentRect.childCount;

        float width = ContentRect.sizeDelta.x;
        float height = 0;

        float childRectHeight = childRect.rect.height;
        float spacing = VerticalLayoutGroup.spacing;

        height += childCount * (childRectHeight + spacing);
        height -= spacing;

        height += VerticalLayoutGroup.padding.top;
        height += VerticalLayoutGroup.padding.bottom;

        ContentRect.sizeDelta = new Vector2(width, height);
    }

    private void setText(string s)
    {
        if (null == TitleText)
        {
            Log.Error("not found title text in wardrobe panel");
            return;
        }

        TitleText.text = s;
    }

    private void onClickBackButton()
    {
        Hide();
    }
}
