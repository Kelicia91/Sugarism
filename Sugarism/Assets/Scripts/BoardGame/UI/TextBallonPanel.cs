using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TextBallonPanel : Panel
{
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
        const float fixedPower = 5.0f;
        const float waitSeconds = 0.1f;

        for (float power = 1.5f; power > 0.0f; power -= 0.2f)
        {
            _rect.anchoredPosition = Random.insideUnitCircle * fixedPower * power;

            yield return new WaitForSeconds(waitSeconds);
        }

        Manager.Instance.Object.BoardGameMode.JudgeIter();
    }

    private void onAttack(int playerId)
    {
        Hide();
    }
}
