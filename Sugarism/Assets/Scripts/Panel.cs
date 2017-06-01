using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 내용만 보면 panel 이 아니라 UI 인데...고민이 필요.
public class Panel : MonoBehaviour
{
    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}
