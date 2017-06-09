using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialoguePanel : Panel
{
    /********* Editor Interface *********/
    // prefabs
    public Text NameText;
    public Text LinesText;
    // exposed variables
    public float InitShakeAmount = 1.0f;
    public float FixedShakeAmount = 5.0f;
    public float DecreaseFactor = 2.0f;

    //
    private float _shake = 0.0f;
    private Vector3 _initPos;


    //
    void Awake()
    {
        Manager.Instance.CmdLinesEvent.Attach(onCmdLines);

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
            Log.Error("not found name text");
        else
            NameText.text = s;
    }

    private void setLines(string s)
    {
        if (null == LinesText)
            Log.Error("not found lines text");
        else
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


    // CmdLinesEventHandler
    private void onCmdLines(int characterId, bool isAnonymous, string lines, Sugarism.ELinesEffect linesEffect)
    {
        string name = getName(characterId, isAnonymous);
        setName(name);

        setLines(lines);

        set(linesEffect);

        Show();
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
            Character c = Manager.Instance.DTCharacter[characterId];
            name = c.name;
        }

        return name;
    }
}
