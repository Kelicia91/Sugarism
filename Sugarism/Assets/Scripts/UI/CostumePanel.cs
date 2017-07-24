using UnityEngine;
using UnityEngine.UI;


public class CostumePanel : Panel
{
    /********* Editor Interface *********/
    // exposed variables
    public Color UNBUY_PANEL_BG_COLOR = Color.gray;
    public Color UNBUY_NAME_TEXT_COLOR = Color.gray;
    public Color UNBUY_BUTTON_BG_COLOR = Color.white;

    public Color BUY_PANEL_BG_COLOR = Color.white;
    public Color BUY_NAME_TEXT_COLOR = Color.black;
    public Color PUT_ON_BUTTON_BG_COLOR = Color.white;
    public Color PUT_OFF_BUTTON_BG_COLOR = Color.gray;
    
    //
    public Text NameText;
    public Image ThumbnailImage;
    public Text DescriptionText;
    public Button Button;
    public Image ButtonImage;
    public Text ButtonText;

    //
    private Image _backgroundImage = null;
    private CostumeController _costume = null;

    //
    private int _charm = 0;
    private int _price = 0;


    //
    void Awake()
    {
        _backgroundImage = GetComponent<Image>();

        MainCharacter mc = Manager.Instance.Object.MainCharacter;
        mc.BuyCostumeEvent.Attach(onBuyCostume);
        mc.WearCostumeEvent.Attach(onWearCostume);
    }

    // Use this for initialization
    void Start()
    {
        MainCharacter mc = Manager.Instance.Object.MainCharacter;
        if (mc.WearingCostumeId != _costume.CostumeId)
            return;
    }

    public void Init(CostumeController costume)
    {
        if (null == costume)
        {
            Log.Error("not found costume");
            return;
        }

        _costume = costume;
        init();
    }

    private void init()
    {
        MainCharacterCostume costume = Manager.Instance.DTMainCharacterCostume[_costume.CostumeId];
        _price = costume.price;
        _charm = costume.charm;

        setName(costume.name);
        setThumbnailImage(costume.thumbnail);
        setDescription();

        if (_costume.IsBuy)
        {
            if (_costume.CostumeId == Manager.Instance.Object.MainCharacter.WearingCostumeId)
                setButtonPutOff();
            else
                setButtonPutOn();
        }
        else
        {
            setButtonBuy();
        }
    }


    private void onBuyCostume(int costumeId)
    {
        if (_costume.CostumeId != costumeId)
            return;

        setButtonPutOn();
    }

    private void onWearCostume(int costumeId)
    {
        if (false == _costume.IsBuy)
            return;

        if (_costume.CostumeId == costumeId)
        {
            setButtonPutOff();
        }
        else
        {
            setButtonPutOn();
        }
    }


    private void setButtonBuy()
    {
        setBackground(UNBUY_PANEL_BG_COLOR);
        setName(UNBUY_NAME_TEXT_COLOR);

        setButtonBackground(UNBUY_BUTTON_BG_COLOR);
        setButtonText(string.Format(Def.MONEY_FORMAT, _price));

        setButton(onClickBuy);
    }

    private void setButtonPutOn()
    {
        setBackground(BUY_PANEL_BG_COLOR);
        setName(BUY_NAME_TEXT_COLOR);

        setButtonBackground(PUT_ON_BUTTON_BG_COLOR);
        setButtonText(Def.PUT_ON_COSTUME);

        setButton(onClickPutOn);
    }

    private void setButtonPutOff()
    {
        setBackground(BUY_PANEL_BG_COLOR);
        setName(BUY_NAME_TEXT_COLOR);

        setButtonBackground(PUT_OFF_BUTTON_BG_COLOR);
        setButtonText(Def.PUT_OFF_COSTUME);

        setButton(onClickPutOff);
    }


    private void onClickBuy()
    {
        _costume.Buy();
    }

    private void onClickPutOn()
    {
        Manager.Instance.Object.MainCharacter.PutOn(_costume.CostumeId);
    }

    private void onClickPutOff()
    {
        Manager.Instance.Object.MainCharacter.PutOff();
    }
    
    
    private void setButton(UnityEngine.Events.UnityAction onClickListener)
    {
        if (null == Button)
        {
            Log.Error("not found costume button component");
            return;
        }

        Button.onClick.RemoveAllListeners();
        Button.onClick.AddListener(onClickListener);
    }

    private void setBackground(Color c)
    {
        if (null == _backgroundImage)
        {
            Log.Error("not found image component");
            return;
        }

        _backgroundImage.color = c;
    }

    private void setName(string s)
    {
        if (null == NameText)
        {
            Log.Error("not found costume's name text component");
            return;
        }

        NameText.text = s;
    }

    private void setName(Color c)
    {
        if (null == NameText)
        {
            Log.Error("not found costume's name text component");
            return;
        }

        NameText.color = c;
    }

    private void setThumbnailImage(Sprite s)
    {
        if (null == ThumbnailImage)
        {
            Log.Error("not found costume's thumbnail image component");
            return;
        }

        ThumbnailImage.sprite = s;
    }

    private void setDescription()
    {
        if (null == DescriptionText)
        {
            Log.Error("not found description text component");
            return;
        }

        int statId = (int)EStat.CHARM;
        Stat stat = Manager.Instance.DTStat[statId];

        string s = string.Format("{0} +{1}", stat.name, _charm);
        DescriptionText.text = s;
    }

    private void setButtonBackground(Color c)
    {
        if (null == ButtonImage)
        {
            Log.Error("not found costume button's image component");
            return;
        }

        ButtonImage.color = c;
    }

    private void setButtonText(string s)
    {
        if (null == ButtonText)
        {
            Log.Error("not found costume button text component");
            return;
        }

        ButtonText.text = s;
    }
}
