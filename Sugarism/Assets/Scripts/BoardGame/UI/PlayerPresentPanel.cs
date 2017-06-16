using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerPresentPanel : Panel
{
    /********* Editor Interface *********/
    // prefabs
    public GameObject PlayerImage;

    private Image _image = null;


    void Awake()
    {
        if (null != PlayerImage)
            _image = PlayerImage.GetComponent<Image>();
        else
            Log.Error("not found player image object");
    }

    public void SetImage(Sprite s)
    {
        if (null == _image)
        {
            Log.Error("not found image");
            return;
        }

        _image.sprite = s;
    }
}
