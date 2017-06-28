using UnityEngine;


public class PersonalInfoPanel : Panel
{
    /********* Editor Interface *********/
    // prefabs
    public GameObject PrefPlayerInfoPanel;

    //
    private PlayerInfoPanel _userInfoPanel = null;
    private PlayerInfoPanel _aiInfoPanel = null;


    void Awake()
    {
        _userInfoPanel = create();
        _aiInfoPanel = create();
    }

    private PlayerInfoPanel create()
    {
        if (null == PrefPlayerInfoPanel)
        {
            Log.Error("not found prefab player info panel");
            return null;
        }

        GameObject o = Instantiate(PrefPlayerInfoPanel);
        o.transform.SetParent(transform, false);

        return o.GetComponent<PlayerInfoPanel>();
    }

    public void OnStart(BoardGame.EValuationBasis valuationBasis, BoardGame.UserPlayer user, BoardGame.AIPlayer ai)
    {
        if (null != _userInfoPanel)
        {
            _userInfoPanel.Set(valuationBasis, user);
            _userInfoPanel.Show();
        }
        else
        {
            Log.Error("not found user info panel");
            _userInfoPanel.Hide();
        }

        if (null != _aiInfoPanel)
        {
            _aiInfoPanel.Set(valuationBasis, ai);
            _aiInfoPanel.Show();
        }
        else
        {
            Log.Error("not found ai info panel");
            _aiInfoPanel.Hide();
        }
    }
}
