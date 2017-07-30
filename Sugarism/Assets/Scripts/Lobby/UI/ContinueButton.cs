using UnityEngine.UI;


public class ContinueButton : TextButton
{
    // @note: Awake() not inherited!
    void Awake()
    {
        _text = GetComponentInChildren<Text>();
        _button = GetComponent<Button>();

        SetInteractable(LobbyManager.Instance.IsSavedData());
    }
}
