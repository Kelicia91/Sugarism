using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyPanel : MonoBehaviour
{
    /********* Editor Interface *********/
    // prefabs
    public Text Text;
    

    // Use this for initialization
    void Start ()
    {
        MainCharacter mainCharacter = Manager.Instance.Object.MainCharacter;

        int money = mainCharacter.Money;
        setText(get(money));

        Manager.Instance.MoneyChangeEvent.Attach(onMoneyChanged);
    }
	
	private void setText(string s)
    {
        if (null == Text)
        {
            Log.Error("not found text");
            return;
        }

        Text.text = s;
    }

    private void onMoneyChanged(int money)
    {
        setText(get(money));
    }

    private string get(int money)
    {
        return money.ToString();
    }
}
