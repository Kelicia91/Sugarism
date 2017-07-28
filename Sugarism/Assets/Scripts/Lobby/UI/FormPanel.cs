using UnityEngine;
using UnityEngine.UI;


public class FormPanel : Panel
{
    /********* Editor Interface *********/
    // prefabs
    [SerializeField]
    private GameObject PrefBackButton = null;
    // objects
    [SerializeField]
    private Text GuideText = null;
    [SerializeField]
    private InputField NameInputField = null;
    [SerializeField]
    private Text ErrorText = null;
    [SerializeField]
    private TextButton TextButton = null;

    //
    private Button _backButton = null;

    //
    private readonly string _TOO_SHORT_PLAYER_NAME = string.Format(Def.TOO_SHORT_PLAYER_NAME, Def.MIN_LENGTH_PLAYER_NAME);
    private readonly string _TOO_LONG_PLAYER_NAME = string.Format(Def.TOO_LONG_PLAYER_NAME, Def.MAX_LENGTH_PLAYER_NAME);

    //
    void Awake()
    {
        create();

        // guide
        setGuide(Def.GUIDE_FORM_PANEL);

        // name input field
        NameInputField.text = Def.DEFAULT_PLAYER_NAME;
        NameInputField.characterLimit = Def.MAX_LENGTH_PLAYER_NAME;
        Text placeHolderText = NameInputField.placeholder.GetComponent<Text>();
        placeHolderText.text = string.Format(Def.GUIDE_PLAYER_NAME, Def.MIN_LENGTH_PLAYER_NAME, Def.MAX_LENGTH_PLAYER_NAME);

        NameInputField.onValidateInput += onValidateInput;

        // error
        setError(null);
    }

    void Start()
    {
        // back button
        _backButton.onClick.AddListener(onClickBackButton);

        // submit button
        TextButton.SetText(Def.NEXT);
        TextButton.AddClick(onClickSubmitButton);
    }
    

    private void create()
    {
        GameObject o = null;

        o = Instantiate(PrefBackButton);
        o.transform.SetParent(transform, false);
        _backButton = o.GetComponent<Button>();
    }


    private void setGuide(string s)
    {
        if (null == GuideText)
        {
            Log.Error("not found guide text");
            return;
        }

        GuideText.text = s;
    }

    private void setError(string s)
    {
        if (null == ErrorText)
        {
            Log.Error("not found error text");
            return;
        }

        if (null != s)
            ErrorText.text = s;
        else
            ErrorText.text = string.Empty;
    }


    private bool isValidName()
    {
        string rawName = NameInputField.text;
        if (rawName.Length < Def.MIN_LENGTH_PLAYER_NAME)
        {
            setError(_TOO_SHORT_PLAYER_NAME);
            return false;
        }
        else if (rawName.Length > Def.MAX_LENGTH_PLAYER_NAME)
        {
            setError(_TOO_LONG_PLAYER_NAME);
            return false;
        }
        else
        {
            setError(null);
            return true;
        }
    }

    private void onClickSubmitButton()
    {
        if (false == isValidName())
            return;

        LobbyManager.Instance.PlayerInitProperty.Name = NameInputField.text;

        Hide();
        LobbyManager.Instance.UI.ConstitutionPanel.Show();
    }

    private void onClickBackButton()
    {
        Hide();
        LobbyManager.Instance.UI.LobbyPanel.Show();
    }


    // @ISSUE : In Editor Mode, UGUI.InputField
    // 입력한 값이 있는 상태에서 포커스를 주면 입력값이 모두 선택됨.
    // 문제는 그 상태에서 '한글 문자를 입력하면' 기존 입력값이 사라지고 금방 입력한 값만
    // 존재해야 한다. 하지만 금방 입력한 값이 기존 입력값 뒤에 입력이 되고,
    // 입력값의 선택 범위가 첫글자를 제외한 나머지 글자들이 선택된다. 
    // 그리고 onValidateInput 함수가 호출되지 않는다.
    // (.apk 빌드 후 Nox 에서 돌렸을때는 재현 안됨)
    private char onValidateInput(string input, int charIndex, char addedChar)
    {
        if (char.IsWhiteSpace(addedChar))
            return char.MinValue;
        else
            return addedChar;

        /*** for debug the issue **/
        //System.Globalization.UnicodeCategory.OtherLetter; // whether korean or not

        //Log.Debug(string.Format("=====> input: {0}", input));
        //Log.Debug(string.Format("char index: {0}, added char: {1}", charIndex.ToString(), addedChar.ToString()));

        //return addedChar;
    }

}   // class
