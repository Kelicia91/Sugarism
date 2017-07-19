using UnityEngine;
using UnityEngine.UI;


public class MainCharacterPanel : CharacterPanel
{
    /********* Editor Interface *********/
    // 
    public Image FaceExpressionImage;
    public Image CostumeImage;


    public virtual void SetMainCharacter()
    {
        Hide();

        MainCharacter mc = Manager.Instance.Object.MainCharacter;

        int looksId = mc.Age - mc.INIT_AGE;
        if (false == ExtMainCharacterLooks.IsValid(looksId))
        {
            Log.Error(string.Format("invalid main character's looks id: {0}", looksId));
            return;
        }

        MainCharacterLooks looks = Manager.Instance.DTMainCharacterLooks[looksId];
        Sprite baseShape = looks.baseShape;
        setBaseShape(baseShape);

        // @todo: 상태에 따른 얼굴 표정
        Sprite faceExpression = looks.faceExpressionDefault;
        setFaceExpression(faceExpression);
        setFaceExpression(looks.faceExpressionPosX, looks.faceExpressionPosY);
        setFaceExpression(true);

        int costumeId = mc.WearingCostumeId;
        Sprite costumeSprite = ExtMainCharacterCostume.Get(costumeId, looksId);
        if (null == costumeSprite)
        {
            Log.Error(string.Format("not found main character's costume id: {0}, looks id: {1}", costumeId, looksId));
            return;
        }

        setCostume(costumeSprite);
        setCostume(true);

        Show();
    }

    protected void setFaceExpression(Sprite s)
    {
        if (null == FaceExpressionImage)
        {
            Log.Error("not found face expression image component");
            return;
        }

        if (null == s)
        {
            Log.Error("not found sprite");
            return;
        }

        FaceExpressionImage.sprite = s;
        FaceExpressionImage.preserveAspect = true;
        FaceExpressionImage.SetNativeSize();
    }

    protected void setFaceExpression(int posX, int posY)
    {
        if (null == FaceExpressionImage)
        {
            Log.Error("not found face expression image component");
            return;
        }

        RectTransform rectTransform = FaceExpressionImage.GetComponent<RectTransform>();
        if (null == rectTransform)
        {
            Log.Error("not found face expression object's rect transform");
            return;
        }

        rectTransform.anchoredPosition = new Vector2(posX, posY);
    }

    protected void setFaceExpression(bool isEnabled)
    {
        if (null == FaceExpressionImage)
        {
            Log.Error("not found face expression image component");
            return;
        }

        FaceExpressionImage.enabled = isEnabled;
    }

    protected void setCostume(Sprite s)
    {
        if (null == CostumeImage)
        {
            Log.Error("not found costume image component");
            return;
        }

        if (null == s)
        {
            Log.Error("not found sprite");
            return;
        }

        CostumeImage.sprite = s;
        CostumeImage.preserveAspect = true;
        CostumeImage.SetNativeSize();
    }

    protected void setCostume(bool isEnabled)
    {
        if (null == CostumeImage)
        {
            Log.Error("not found costume image component");
            return;
        }

        CostumeImage.enabled = isEnabled;
    }
}
