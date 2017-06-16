using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CardPanel : Panel
{
    /********* Editor Interface *********/
    // prefabs
    public GameObject PrefPlayerCardPanel;

    //
    private PlayerCardPanel _userCardPanel = null;
    private PlayerCardPanel _aiCardPanel = null;


    void Awake()
    {
        _userCardPanel = create();
        _aiCardPanel = create();
    }

    private PlayerCardPanel create()
    {
        if (null == PrefPlayerCardPanel)
        {
            Log.Error("not found prefab player card panel");
            return null;
        }

        GameObject o = Instantiate(PrefPlayerCardPanel);
        o.transform.SetParent(transform, false);

        return o.GetComponent<PlayerCardPanel>();
    }

    public void OnStart(BoardGame.UserPlayer user, BoardGame.AIPlayer ai)
    {
        if (null != _userCardPanel)
        {
            _userCardPanel.Set(user);
            _userCardPanel.Show();
        }
        else
        {
            Log.Error("not found user card panel");
            _userCardPanel.Hide();
        }

        if (null != _aiCardPanel)
        {
            _aiCardPanel.Set(ai);
            _aiCardPanel.Show();
        }
        else
        {
            Log.Error("not found ai card panel");
            _aiCardPanel.Hide();
        }            
    }
}
