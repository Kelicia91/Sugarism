using UnityEngine;
using UnityEngine.UI;


public class CharacterPanel : Panel
{
    /********* Editor Interface *********/
    // 
    public Image BaseShapeImage;


    public virtual void Set(Rival rival)
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

        Show();
    }

    public virtual void Set(Character c)
    {
        Hide();

        setBaseShape(c.image);

        Show();
    }


    protected void setBaseShape(Sprite s)
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
        BaseShapeImage.SetNativeSize();
    }
}
