using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FloatingText : MonoBehaviour
{
    //
    private Text _text = null;
    private RectTransform _textRectTransform = null;

    //
    void Awake()
    {
        _text = GetComponent<Text>();
        if (null == _text)
        {
            Log.Error("not found floating damage text");
        }
        else
        {
            _textRectTransform = _text.rectTransform;
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Float(string s, int fontSize, Color c)
    {
        StopAllCoroutines();
        Hide();

        setText(s);
        _text.fontSize = fontSize;
        _text.color = c;
        
        _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, FROM_TEXT_COLOR_ALPHA);
        _textRectTransform.anchoredPosition = new Vector2(0.0f, FROM_TEXT_POS_Y);

        show();
        
        StartCoroutine(floating());
    }

    private const float FROM_TEXT_COLOR_ALPHA = 1.0f;
    //private const float TO_TEXT_COLOR_ALPHA = 0.0f;
    private const float FROM_TEXT_POS_Y = 0.0f;
    private const float TO_TEXT_POS_Y = 40.0f;

    private IEnumerator floating()
    {
        for (float alpha = FROM_TEXT_COLOR_ALPHA, y = FROM_TEXT_POS_Y; 
            y < TO_TEXT_POS_Y; 
            alpha -= 0.1f, y += 4.0f)
        {
            _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, alpha);
            _textRectTransform.anchoredPosition = new Vector2(0.0f, y);

            yield return new WaitForSeconds(.1f);
        }
        
        Hide();
    }

    private void setText(string s)
    {
        _text.text = s;
    }

    private void show()
    {
        gameObject.SetActive(true);
    }
}
