using UnityEngine;
using UnityEngine.UI;


public class StoryPanel : Panel
{
    /********* Editor Interface *********/
    // prefabs
    public GameObject BackgroundImage;
    public StoryCharacterPanel StoryCharacterPanel;
    public GameObject ForegroundImage;  // grayscale image
    public DialoguePanel DialogPanel;
    public MiniPicturePanel MiniPicturePanel;
    public ClearPanel InputPanel;
    public SwitchPanel SwitchPanel;
    public GameObject PrefFadePanel;

    // Layer z-order (back->front)
    // : bg -> character -> fg -> dialogue -> mini cg -> clear(input) -> switch -> fade

    //
    private AudioSource _seAudioSource = null;
    private FadePanel _fadePanel = null;


    // Use this for initialization
    void Awake()
    {
        _seAudioSource = gameObject.AddComponent<AudioSource>();
        _seAudioSource.loop = false;

        GameObject o = Instantiate(PrefFadePanel);
        o.transform.SetParent(transform, false);
        _fadePanel = o.GetComponent<FadePanel>();

        // attach event handler
        Story.Mode storyMode = Manager.Instance.Object.StoryMode;

        storyMode.CmdAppearEvent.Attach(onCmdAppear);
        storyMode.CmdFilterEvent.Attach(onCmdFilter);
        storyMode.CmdBackgroundEvent.Attach(onCmdBackground);
        storyMode.CmdPictureEvent.Attach(onCmdPicture);
        storyMode.CmdSEEvent.Attach(onCmdSE);
        storyMode.CmdTargetAppearEvent.Attach(onCmdTargetAppear);
        storyMode.CmdDisappearEvent.Attach(onCmdDisappear);

        storyMode.ScenarioStartEvent.Attach(onScenarioStart);
        storyMode.ScenarioEndEvent.Attach(onScenarioEnd);
    }


    private void showFadeIn()
    {
        base.Show();

        _fadePanel.FadeIn();
    }

    private void hideFadeOut()
    {
        float animLength = _fadePanel.FadeOut();

        Invoke(END_FADE_OUT_METHOD_NAME, animLength);
    }

    private const string END_FADE_OUT_METHOD_NAME = "endFadeOut";
    private void endFadeOut()
    {
        base.Hide();
    }


    private void initialize()
    {
        set(BackgroundImage, null);
        set(Sugarism.EFilter.None);

        StoryCharacterPanel.Hide();

        DialogPanel.Hide();
        MiniPicturePanel.Hide();

        InputPanel.Show();
        SwitchPanel.Hide();
    }

    private void set(GameObject o, Sprite sprite)
    {
        if (null == o)
        {
            Log.Error("not found object");
            return;
        }

        Image img = o.GetComponent<Image>();
        if (null == img)
        {
            Log.Error("not found image component");
            return;
        }

        img.sprite = sprite;
    }

    private void set(Sugarism.EFilter filter)
    {
        if (null == ForegroundImage)
        {
            Log.Error("not found object");
            return;
        }

        switch(filter)
        {
            case Sugarism.EFilter.Grayscale:
                ForegroundImage.SetActive(true);
                break;

            case Sugarism.EFilter.None:
            default:
                ForegroundImage.SetActive(false);
                break;
        }
    }

    private void playSE(AudioClip clip)
    {
        if (null == _seAudioSource)
        {
            Log.Error("not found SE audio source");
            return;
        }

        _seAudioSource.Stop();
        _seAudioSource.clip = clip;
        _seAudioSource.Play();
    }


    // callback handler
    private void onCmdAppear(int characterId, Sugarism.EPosition position)
    {
        //InputPanel.Hide();

        StoryCharacterPanel.Set(characterId, position);

        //Invoke(ON_TIMER_NAME, 0.1f);
    }

    private void onCmdTargetAppear(int targetId, bool isBlush, Sugarism.EFace face,
                            Sugarism.ECostume costume, Sugarism.EPosition position)
    {
        //InputPanel.Hide();

        StoryCharacterPanel.Set(targetId, isBlush, face, costume, position);

        //Invoke(ON_TIMER_NAME, 0.1f);
    }

    private void onCmdDisappear()
    {
        StoryCharacterPanel.Hide();
    }

    private void onCmdFilter(Sugarism.EFilter filter)
    {
        InputPanel.Hide();

        set(filter);

        Invoke(ON_TIMER_NAME, 0.1f);
    }

    private void onCmdBackground(int id)
    {
        if (false == ExtBackground.IsValid(id))
            return;

        InputPanel.Hide();

        Background bg = Manager.Instance.DTBackground[id];
        set(BackgroundImage, bg.sprite);

        Invoke(ON_TIMER_NAME, 0.1f);
    }

    // for auto-NextCmd()
    private const string ON_TIMER_NAME = "onTimer";
    private void onTimer()
    {
        Manager.Instance.Object.StoryMode.NextCmd();
        InputPanel.Show();
    }
    
    private void onCmdPicture(int id)
    {
        if (false == ExtPicture.IsValid(id))
            return;

        Picture cg = Manager.Instance.DTPicture[id];
        set(BackgroundImage, cg.sprite);
        
        StoryCharacterPanel.Hide();
        set(Sugarism.EFilter.None);
        DialogPanel.Hide();
    }

    private void onCmdSE(int id)
    {
        if (false == ExtSE.IsValid(id))
            return;

        SE se = Manager.Instance.DTSE[id];
        playSE(se.audioClip);
    }

    // call handler
    private void onScenarioStart()
    {
        initialize();
        showFadeIn();
    }

    private void onScenarioEnd()
    {
        hideFadeOut();
    }
}
