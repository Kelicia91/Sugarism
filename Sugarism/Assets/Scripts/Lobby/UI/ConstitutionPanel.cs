using UnityEngine;
using UnityEngine.UI;


public class ConstitutionPanel : Panel
{
    /********* Editor Interface *********/
    // prefabs
    public GameObject PrefBackButton;
    public CustomToggle PrefCustomToggle;
    // objects
    public Text GuideText;
    public ToggleGroup ToggleGroup;
    public SubmitButton SubmitButton;

    //
    private Button _backButton = null;

    //
    void Awake()
    {
        create();

        // guide
        setGuide("xx을 선택해주세요.");

        // toggle group

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

        // create toggles in toggle group
        // 데이터 테이블 참조 필요
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
