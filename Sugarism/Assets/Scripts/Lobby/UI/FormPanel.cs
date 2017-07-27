using UnityEngine;
using UnityEngine.UI;


public class FormPanel : Panel
{
    /********* Editor Interface *********/
    // prefabs
    public GameObject PrefBackButton;
    // objects
    public Text GuideText;
    public InputField NameInputField;
    public SubmitButton SubmitButton;

    //
    private Button _backButton = null;

    //
    void Awake()
    {
        create();

        // guide
        setGuide("이름을 입력해주세요");

        // name input field
        NameInputField.text = "기본값"; // 매번 입력하기 귀찮으니까
        NameInputField.characterLimit = 4;  // 최대 몇글자까지 허용할지
        Text placeHolderText = NameInputField.placeholder.GetComponent<Text>();
        placeHolderText.text = "최대 xx글자"; // 입력된 텍스트 없는경우, 입력 가이드

        // back button
        _backButton.onClick.AddListener(onClickBackButton);

        // submit button
        SubmitButton.SetText("확인");
        SubmitButton.AddClick(onClickSubmitButton);
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

    private void onClickSubmitButton()
    {
        // 입력 값 검사
    }

    private void onClickBackButton()
    {
        // click back button
    }
}
