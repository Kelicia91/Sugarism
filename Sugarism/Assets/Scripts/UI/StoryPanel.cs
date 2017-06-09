using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StoryPanel : Panel
{
    /********* Editor Interface *********/
    // exposed variables
    public float DELTA_POS_X = 50.0f;
    public float DELTA_SCALE = 0.3f;
    // prefabs
    public GameObject BackgroundImage;
    public GameObject CharacterImage;
    public GameObject ForegroundImage;  // grayscale image
    public DialoguePanel DialogPanel;
    public ClearPanel ClearPanel;
    public SwitchPanel SwitchPanel;

    // layer z-order (back->front)
    // : bg -> character -> fg -> dialogue -> clear(touch) -> switch

    
    // Use this for initialization
    void Awake()
    {
        set(Sugarism.EPosition.None);
        set(Sugarism.EFilter.None);
        ClearPanel.Show();

        Manager.Instance.CmdAppearEvent.Attach(onCmdAppear);
        Manager.Instance.CmdFilterEvent.Attach(onCmdFilter);
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


    private void onCmdAppear(int characterId, Sugarism.EFace face, 
                            Sugarism.ECostume costume, Sugarism.EPosition position)
    {
        // @todo: face
        // @todo: costume
        set(position);
    }

    private void onCmdFilter(Sugarism.EFilter filter)
    {
        ClearPanel.Hide();

        set(filter);

        Invoke("onTimer", 0.1f);
    }

    // for auto-NextCmd()
    private void onTimer()
    {
        Manager.Instance.Object.NextCmd();
        ClearPanel.Show();
    }
}
