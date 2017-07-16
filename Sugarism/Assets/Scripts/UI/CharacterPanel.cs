using UnityEngine;
using UnityEngine.UI;


public class CharacterPanel : Panel
{
    /********* Editor Interface *********/
    // 
    public Image BaseShapeImage;
    public Image FaceExpressionImage;
    public Image CostumeImage;

    //
    void Awake()
    {
        
    }

    public void Set(Rival rival)
    {
        Hide();

        Sprite image = null;
        if (Manager.Instance.Object.MainCharacter.IsChildHood())
        {
            image = rival.childImage;
        }
        else
        {
            int characterId = rival.characterId;
            Character c = Manager.Instance.DTCharacter[characterId];
            image = c.image;
        }

        setBaseShape(image);
        setFaceExpression(false);
        setCostume(false);

        Show();
    }

    public void Set(Character c)
    {
        Hide();

        setBaseShape(c.image);
        setFaceExpression(false);
        setCostume(false);

        Show();
    }


    private void setBaseShape(Sprite s)
    {
        if (null == BaseShapeImage)
        {
            Log.Error("not found base shape image component");
            return;
        }

        if (null == s)
        {
            Log.Error("not found sprite");
            return;
        }

        BaseShapeImage.sprite = s;
        BaseShapeImage.preserveAspect = true;
    }

    private void setFaceExpression(Sprite s)
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

    private void setFaceExpression(int posX, int posY)
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

    private void setFaceExpression(bool isEnabled)
    {
        if (null == FaceExpressionImage)
        {
            Log.Error("not found face expression image component");
            return;
        }

        FaceExpressionImage.enabled = isEnabled;
    }

    private void setCostume(Sprite s)
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
