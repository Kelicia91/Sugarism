using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PresentPanel : Panel
{
    /********* Editor Interface *********/
    // prefabs
    public GameObject PrefPlayerPresentPanel;

    //
    private PlayerPresentPanel _userPresentPanel = null;
    private PlayerPresentPanel _aiPresentPanel = null;


    void Awake()
    {
        _userPresentPanel = create();
        _aiPresentPanel = create();
    }

    private PlayerPresentPanel create()
    {
        if (null == PrefPlayerPresentPanel)
        {
            Log.Error("not found prefab player present panel");
            return null;
        }

        GameObject o = Instantiate(PrefPlayerPresentPanel);
        o.transform.SetParent(transform, false);

        return o.GetComponent<PlayerPresentPanel>();
    }

    public void OnStart(BoardGame.UserPlayer user, BoardGame.AIPlayer ai)
    {
        if (null != _userPresentPanel)
        {
            //_userPresentPanel.Set(user);
            _userPresentPanel.Show();
        }
        else
        {
            Log.Error("not found user present panel");
            _userPresentPanel.Hide();
        }

        if (null != _aiPresentPanel)
        {
            //_aiPresentPanel.Set(ai);
            _aiPresentPanel.Show();
        }
        else
        {
            Log.Error("not found ai present panel");
            _aiPresentPanel.Hide();
        }
    }
}
