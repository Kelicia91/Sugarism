using UnityEngine;
using UnityEngine.UI;


public class StoryCharacterPanel : MainCharacterPanel
{
    /********* Editor Interface *********/
    // exposed variables
    public float DELTA_POS_X = 100.0f;
    public float DELTA_SCALE = 0.1f;

    // 
    public Image BlushImage;

    //
    private RectTransform _rect = null;
    private RectTransform _blushRect = null;


    //
    void Awake()
    {
        _rect = GetComponent<RectTransform>();
        _blushRect = BlushImage.GetComponent<RectTransform>();
    }
    
    public override void Set(Character c)
    {
        Log.Error("not supported interface");
    }

    public void Set(int characterId, Sugarism.EPosition pos)
    {
        Hide();

        if (false == ExtCharacter.IsValid(characterId))
        {
            Log.Error(string.Format("invalid character Id; {0}", characterId));
            return;
        }

        Character c = Manager.Instance.DTCharacter[characterId];
        setBaseShape(c.image);
        setBlush(false);
        setFaceExpression(false);
        setCostume(false);

        set(pos);

        Show();
    }

    public void Set(int targetId, bool isBlush, Sugarism.EFace face, Sugarism.ECostume costume, Sugarism.EPosition pos)
    {
        Hide();

        if (false == ExtTarget.isValid(targetId))
        {
            Log.Error(string.Format("invalid target id; {0}", targetId));
            return;
        }

        Target target = Manager.Instance.DTTarget[targetId];
        setBaseShape(target.baseShape);

        if (isBlush)
        {
            setBlush(target.blush);
            setBlush(target.blushPosX, target.blushPosY);
        }
        setBlush(isBlush);

        base.setFaceExpression(get(ref target, face));
        setFaceExpression(target.faceExpPosX, target.faceExpPosY);
        setFaceExpression(true);

        base.setCostume(get(ref target, costume));
        setCostume(true);

        set(pos);

        Show();
    }


    private Sprite get(ref Target target, Sugarism.EFace face)
    {
        switch(face)
        {
            case Sugarism.EFace.Default:
                return target.faceExpDefault;

            case Sugarism.EFace.Happy:
                return target.faceExpHappy;

            case Sugarism.EFace.Sad:
                return target.faceExpSad;

            case Sugarism.EFace.Angry:
                return target.faceExpAngry;

            case Sugarism.EFace.Shock:
                return target.faceExpShock;

            default:
                return null;
        }
    }

    private Sprite get(ref Target target, Sugarism.ECostume costume)
    {
        switch(costume)
        {
            case Sugarism.ECostume.Default:
                return target.costumeDefault;

            case Sugarism.ECostume.Private:
                return target.costumePrivate;

            default:
                return null;
        }
    }

    private void set(Sugarism.EPosition pos)
    {
        float posX = 0.0f;
        float posY = _rect.anchoredPosition.y;
        float scale = 1.0f;

        switch (pos)
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

            default:
                return;
        }
                
        _rect.anchoredPosition = new Vector2(posX, posY);
        _rect.localScale = new Vector2(scale, scale);
    }

    private void setBlush(Sprite s)
    {
        if (null == BlushImage)
        {
            Log.Error("not found blush image component");
            return;
        }

        if (null == s)
        {
            Log.Error("not found sprite");
            return;
        }

        BlushImage.sprite = s;
        BlushImage.preserveAspect = true;
        BlushImage.SetNativeSize();
    }

    private void setBlush(int posX, int posY)
    {
        _blushRect.anchoredPosition = new Vector2(posX, posY);
    }

    private void setBlush(bool isEnabled)
    {
        if (null == BlushImage)
        {
            Log.Error("not found blush image component");
            return;
        }

        BlushImage.enabled = isEnabled;
    }


    private void setFaceExpression(bool isEnabled)
    {
        if (null == FaceExpressionImage)
        {
            Log.Error("not found face expression image component");
            return;
        }

        FaceExpressionImage.enabled = isEnabled;
    }

    private void setCostume(bool isEnabled)
    {
        if (null == CostumeImage)
        {
            Log.Error("not found costume image component");
            return;
        }

        CostumeImage.enabled = isEnabled;
    }
}
