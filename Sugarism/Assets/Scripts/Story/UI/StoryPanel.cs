using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StoryPanel : Panel
{
    /********* Editor Interface *********/
    // exposed variables
    public float DELTA_POS_X = 100.0f;
    public float DELTA_SCALE = 0.1f;
    // prefabs
    public GameObject BackgroundImage;
    public GameObject CharacterImage;
    public GameObject ForegroundImage;  // grayscale image
    public DialoguePanel DialogPanel;
    public MiniPicturePanel MiniPicturePanel;
    public ClearPanel ClearPanel;
    public SwitchPanel SwitchPanel;

    // Layer z-order (back->front)
    // : bg -> character -> fg -> dialogue -> mini cg -> clear(touch) -> switch

    //
    private AudioSource _seAudioSource = null;


    // Use this for initialization
    void Awake()
    {
        _seAudioSource = gameObject.AddComponent<AudioSource>();
        _seAudioSource.loop = false;

        Story.Mode storyMode = Manager.Instance.Object.StoryMode;

        storyMode.CmdAppearEvent.Attach(onCmdAppear);
        storyMode.CmdFilterEvent.Attach(onCmdFilter);
        storyMode.CmdBackgroundEvent.Attach(onCmdBackground);
        storyMode.CmdPictureEvent.Attach(onCmdPicture);
        storyMode.CmdSEEvent.Attach(onCmdSE);

        storyMode.ScenarioStartEvent.Attach(onScenarioStart);
        storyMode.ScenarioEndEvent.Attach(onScenarioEnd);
    }

    private void initialize()
    {
        set(BackgroundImage, null);

        set(Sugarism.EFilter.None);
        set(Sugarism.EPosition.None);

        DialogPanel.Hide();
        MiniPicturePanel.Hide();

        ClearPanel.Show();
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

    private void set(Sugarism.EPosition pos)
    {
        if (null == CharacterImage)
        {
            Log.Error("not found character image object");
            return;
        }

        float posX = 0.0f;
        float scale = 1.0f;
        switch(pos)
        {
            case Sugarism.EPosition.Middle:
                posX = 0.0f;
                scale = 1.0f;
                break;

            case Sugarism.EPosition.Left:
                posX -= DELTA_POS_X;
                scale = 1.0f;
                break;

            case Sugarism.EPosition.Right:
                posX += DELTA_POS_X;
                scale = 1.0f;
                break;

            case Sugarism.EPosition.Front:
                posX = 0.0f;
                scale += DELTA_SCALE;
                break;

            case Sugarism.EPosition.Back:
                posX = 0.0f;
                scale -= DELTA_SCALE;
                break;

            case Sugarism.EPosition.None:
                CharacterImage.SetActive(false);
                return;

            default:
                return;
        }

        RectTransform rect = CharacterImage.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(posX, rect.anchoredPosition.y);
        rect.localScale = new Vector2(scale, scale);

        CharacterImage.SetActive(true);
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
    private void onCmdAppear(int characterId, Sugarism.EFace face, 
                            Sugarism.ECostume costume, Sugarism.EPosition position)
    {
        ClearPanel.Hide();

        // @todo: face
        // @todo: costume
        set(position);

        Invoke(ON_TIMER_NAME, 0.1f);
    }

    private void onCmdFilter(Sugarism.EFilter filter)
    {
        ClearPanel.Hide();

        set(filter);

        Invoke(ON_TIMER_NAME, 0.1f);
    }

    private void onCmdBackground(int id)
    {
        if (false == ExtBackground.IsValid(id))
            return;

        ClearPanel.Hide();

        Background bg = Manager.Instance.DTBackground[id];
        set(BackgroundImage, bg.sprite);

        Invoke(ON_TIMER_NAME, 0.1f);
    }

    // for auto-NextCmd()
    private const string ON_TIMER_NAME = "onTimer";
    private void onTimer()
    {
        Manager.Instance.Object.StoryMode.NextCmd();
        ClearPanel.Show();
    }
    
    private void onCmdPicture(int id)
    {
        if (false == ExtPicture.IsValid(id))
            return;

        Picture cg = Manager.Instance.DTPicture[id];
        set(BackgroundImage, cg.sprite);

        set(Sugarism.EPosition.None);
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
        Show();
    }

    private void onScenarioEnd()
    {
        Hide();
    }
}
