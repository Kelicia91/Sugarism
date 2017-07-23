using UnityEngine;
using UnityEngine.UI;


public class MainCharacterPanel : CharacterPanel
{
    /********* Editor Interface *********/
    // 
    public Image FaceExpressionImage;
    public Image CostumeImage;

    //
    private MainCharacter _mainCharacter = null;

    //
    void Awake()
    {
        _mainCharacter = Manager.Instance.Object.MainCharacter;

        _mainCharacter.AgeChangeEvent.Attach(onAgeChanged);
        _mainCharacter.ConditionEvent.Attach(onConditionChanged);

        Manager.Instance.WearCostumeEvent.Attach(onWearCostume);

        show();
    }

    //
    private void show()
    {
        Hide();

        int looksId = _mainCharacter.Age - _mainCharacter.INIT_AGE;
        if (false == ExtMainCharacterLooks.IsValid(looksId))
        {
            Log.Error(string.Format("invalid main character's looks id: {0}", looksId));
            return;
        }

        MainCharacterLooks looks = Manager.Instance.DTMainCharacterLooks[looksId];
        Sprite baseShape = looks.baseShape;
        setBaseShape(baseShape);
        
        setFaceExpression(_mainCharacter.Condition);
        setFaceExpression(looks.faceExpressionPosX, looks.faceExpressionPosY);

        int costumeId = _mainCharacter.WearingCostumeId;
        Sprite costumeSprite = ExtMainCharacterCostume.Get(costumeId, looksId);
        if (null == costumeSprite)
        {
            Log.Error(string.Format("not found main character's costume id: {0}, looks id: {1}", costumeId, looksId));
            return;
        }
        setCostume(costumeSprite);

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


    private void onAgeChanged(int age)
    {
        if (Def.MAX_AGE == age)
            return;

        show();
    }

    private void onWearCostume(int costumeId)
    {
        int looksId = _mainCharacter.Age - _mainCharacter.INIT_AGE;

        Sprite s = ExtMainCharacterCostume.Get(costumeId, looksId);
        setCostume(s);
    }

    private void onConditionChanged(Nurture.ECondition condition)
    {
        setFaceExpression(condition);
    }


    private void setFaceExpression(Nurture.ECondition condition)
    {
        int looksId = _mainCharacter.Age - _mainCharacter.INIT_AGE;
        if (false == ExtMainCharacterLooks.IsValid(looksId))
        {
            Log.Error(string.Format("invalid main character's looks id: {0}", looksId));
            return;
        }

        MainCharacterLooks looks = Manager.Instance.DTMainCharacterLooks[looksId];

        Sprite faceExpression = null;
        switch (condition)
        {
            case Nurture.ECondition.Healthy:
                faceExpression = looks.faceExpressionDefault;
                break;

            case Nurture.ECondition.Sick:
            case Nurture.ECondition.Die:
                faceExpression = looks.faceExpressionSick;
                break;

            default:
                return;
        }
        
        setFaceExpression(faceExpression);
    }
}
