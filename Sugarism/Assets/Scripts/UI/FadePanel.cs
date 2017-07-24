using UnityEngine;
using UnityEngine.UI;


public class FadePanel : MonoBehaviour
{
    // @note: window 'Animator' - Parameters 'Triggers'
    public enum ETrigger
    {
        FadeIn,
        FadeOut
    }

    public const int DEFAULT_LAYER_INDEX = 0;

    //
    private Image _image = null;
    private Animator _animator = null;


    //
    void Awake()
    {
        _image = GetComponent<Image>();
        _animator = GetComponent<Animator>();
    }

    
    public void FadeIn()
    {
        Log.Debug("FadeIn");
        reset(ETrigger.FadeOut);
        set(ETrigger.FadeIn);

        _image.raycastTarget = true;

        float animLength = getCurrentStateLength(DEFAULT_LAYER_INDEX);
        Invoke(END_FADE_IN_METHOD_NAME, animLength);
    }

    private const string END_FADE_IN_METHOD_NAME = "endFadeIn";
    private void endFadeIn()
    {
        Log.Debug(END_FADE_IN_METHOD_NAME);
        _image.raycastTarget = false;
        reset(ETrigger.FadeIn);
    }


    public float FadeOut()
    {
        Log.Debug("FadeOut");
        reset(ETrigger.FadeIn);
        set(ETrigger.FadeOut);

        _image.raycastTarget = true;

        float animLength = getCurrentStateLength(DEFAULT_LAYER_INDEX);
        Invoke(END_FADE_OUT_METHOD_NAME, animLength);

        return animLength;
    }

    private const string END_FADE_OUT_METHOD_NAME = "endFadeOut";
    private void endFadeOut()
    {
        Log.Debug(END_FADE_OUT_METHOD_NAME);
        _image.raycastTarget = false;
        reset(ETrigger.FadeOut);
    }


    private void set(ETrigger triggerType)
    {
        string s = triggerType.ToString();
        _animator.SetTrigger(s);
    }

    private void reset(ETrigger triggerType)
    {
        string s = triggerType.ToString();
        _animator.ResetTrigger(s);
    }


    private float getCurrentStateLength(int layerIndex)
    {
        AnimatorStateInfo info = _animator.GetCurrentAnimatorStateInfo(layerIndex);

        float length = info.length;
        Log.Debug(string.Format("animator state info. length: {0}", length));

        return length;
    }
}
