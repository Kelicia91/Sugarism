using UnityEngine;
using UnityEngine.UI;


public class DialoguePanel : Panel
{
    /********* Editor Interface *********/
    // prefabs
    public GameObject NamePanel;
    public Text NameText;
    public Text LinesText;
    // exposed variables
    public float InitShakeAmount = 1.0f;
    public float FixedShakeAmount = 20.0f;
    public float DecreaseFactor = 1.0f;

    //
    private float _shake = 0.0f;
    private Vector3 _initPos;


    //
    void Awake()
    {
        Story.Mode storyMode = Manager.Instance.Object.StoryMode;
        storyMode.CmdLinesEvent.Attach(onCmdLines);
        storyMode.CmdTextEvent.Attach(onCmdText);

        Hide();
    }

    // Use this for initialization
    void Start()
    {
        _initPos = GetComponent<RectTransform>().anchoredPosition;
    }

    //
    void Update()
    {
        if (_shake > 0.0f)
        {
            set(Random.insideUnitCircle * FixedShakeAmount * _shake);
            _shake -= (Time.deltaTime * DecreaseFactor);

            if (_shake <= 0.0f)
                resetShake();
        }
    }

    private void set(Vector2 pos)
    {
        RectTransform rect = GetComponent<RectTransform>();
        rect.anchoredPosition = pos;
    }

    private void resetShake()
    {
        _shake = 0.0f;
        set(_initPos);
    }

    private void setName(string s)
    {
        if (null == NameText)
        {
            Log.Error("not found name text");
            return;
        }

        NameText.text = s;
    }

    private void setLines(string s)
    {
        if (null == LinesText)
        {
            Log.Error("not found lines text");
            return;
        }

        LinesText.text = s;
    }

    private void set(Sugarism.ELinesEffect linesEffect)
    {
        switch(linesEffect)
        {
            case Sugarism.ELinesEffect.Shake:
                _shake = InitShakeAmount;
                break;

            case Sugarism.ELinesEffect.None:
            default:
                resetShake();
                break;
        }
    }

    private void setActiveNamePanel(bool isShow)
    {
        if (null == NamePanel)
        {
            Log.Error("not found name panel");
            return;
        }

        NamePanel.SetActive(isShow);
    }

    private string getName(int characterId, bool isAnonymous)
    {
        if (false == ExtCharacter.IsValid(characterId))
            return string.Empty;

        string name = null;
        if (characterId == Def.MAIN_CHARACTER_ID)
        {
            name = Manager.Instance.Object.MainCharacter.Name;
        }
        else if (isAnonymous)
        {
            name = Def.ANONYMOUS;
        }
        else
        {
            Character c = Manager.Instance.DT.Character[characterId];
            name = c.name;
        }

        return name;
    }


    // EventHandler
    private void onCmdLines(int characterId, bool isAnonymous, string lines, Sugarism.ELinesEffect linesEffect)
    {
        string name = getName(characterId, isAnonymous);
        setName(name);
        setActiveNamePanel(true);

        setLines(lines);

        set(linesEffect);

        Show();
    }

    private void onCmdText(string text)
    {
        setActiveNamePanel(false);
        
        setLines(text);

        set(Sugarism.ELinesEffect.None);

        Show();
    }
}
