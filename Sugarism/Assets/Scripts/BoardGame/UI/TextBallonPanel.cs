using System.Collections;
using UnityEngine;


public class TextBallonPanel : Panel
{
    /********* Editor Interface *********/
    // exposed variables
    public float FixedShakeAmount = 20.0f;

    //
    private RectTransform _rect = null;

    void Awake()
    {
        _rect = GetComponent<RectTransform>();

        var mode = Manager.Instance.Object.BoardGameMode;
        mode.AttackEvent.Attach(onAttack);
        mode.BingoEvent.Attach(onBingo);

        Hide();
    }

    private void onBingo()
    {
        Show();
        StartCoroutine(Shake());
    }
    
    IEnumerator Shake()
    {
        const float waitSeconds = 0.1f;

        for (float power = 1.5f; power > 0.0f; power -= 0.2f)
        {
            _rect.anchoredPosition = Random.insideUnitCircle * FixedShakeAmount * power;

            yield return new WaitForSeconds(waitSeconds);
        }

        Manager.Instance.Object.BoardGameMode.JudgeIter();
    }

    private void onAttack(int playerId)
    {
        Hide();
    }
}
