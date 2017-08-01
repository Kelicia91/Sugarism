using UnityEngine;


// prohibit from abusing Singleton.
// @IMPORTANT : prohibit from re-define constuctor because of inheriting MonoBehaviour.
public class LobbyManager : SceneManager
{
    private static LobbyManager _instance = null;
    public static LobbyManager Instance { get { return _instance; } }


    /********* Editor Interface *********/
    // Prefabs
    [SerializeField]
    private PlayerInitProperty PrefPlayerInitProperty = null;
    [SerializeField]
    private LobbyUIManager PrefLobbyUIManager = null;

    /********* Game Interface *********/
    private PlayerInitProperty _playerInitProperty = null;
    public PlayerInitProperty PlayerInitProperty { get { return _playerInitProperty; } }

    private LobbyUIManager _ui = null;
    public LobbyUIManager UI { get { return _ui; } }


    //
    void Awake()
    {
        _instance = this;
        Initialize();
        
        // manager
        _playerInitProperty = Instantiate(PrefPlayerInitProperty);        
        _ui = Instantiate(PrefLobbyUIManager);
    }
    
    //
    public void NewStart()
    {
        initialize();

        LoadScene(SceneDef.MAIN);
    }

    public void Continue()
    {
        LoadScene(SceneDef.MAIN);
    }

    private void initialize()
    {
        SetContinueData(PlayerInitProperty.Name);

        int constitutionId = (int)PlayerInitProperty.Constitution;
        int zodiacId = (int)PlayerInitProperty.Zodiac;
        int condition = (int)Nurture.ECondition.Healthy;

        CustomPlayerPrefs.SetInt(PlayerPrefsKey.YEAR, Def.INIT_YEAR);
        CustomPlayerPrefs.SetInt(PlayerPrefsKey.MONTH, Def.INIT_MONTH);
        
        CustomPlayerPrefs.SetInt(PlayerPrefsKey.ZODIAC, zodiacId);
        CustomPlayerPrefs.SetInt(PlayerPrefsKey.CONDITION, condition);

        int actionCount = DT.Action.Count;
        for (int id = 0; id < actionCount; ++id)
        {
            string key = PlayerPrefsKey.GetActionCountKey(id);
            CustomPlayerPrefs.SetInt(key, 0);
        }

        CustomPlayerPrefs.SetInt(PlayerPrefsKey.MONEY, Def.INIT_MONEY);
        CustomPlayerPrefs.SetInt(PlayerPrefsKey.CONSTITUTION, constitutionId);
        CustomPlayerPrefs.SetInt(PlayerPrefsKey.WEARING_COSTUME, Def.DEFAULT_COSTUME_ID);

        int costumeCount = DT.MainCharacterCostume.Count;
        for (int id = 0; id < costumeCount; ++id)
        {
            string key = PlayerPrefsKey.GetCostumeKey(id);

            bool isBuy = false;
            if (Def.DEFAULT_COSTUME_ID == id)
                isBuy = true;

            int isBuyInteger = PlayerPrefsKey.GetBoolToInt(isBuy);

            CustomPlayerPrefs.SetInt(key, isBuyInteger);
        }

        CustomPlayerPrefs.SetInt(PlayerPrefsKey.STRESS, Def.MIN_STAT);
        Zodiac zodiac = DT.Zodiac[zodiacId];
        CustomPlayerPrefs.SetInt(PlayerPrefsKey.STAMINA, zodiac.stamina);
        CustomPlayerPrefs.SetInt(PlayerPrefsKey.INTELLECT, zodiac.intellect);
        CustomPlayerPrefs.SetInt(PlayerPrefsKey.GRACE, zodiac.grace);
        CustomPlayerPrefs.SetInt(PlayerPrefsKey.CHARM, zodiac.charm);
        CustomPlayerPrefs.SetInt(PlayerPrefsKey.ATTACK, zodiac.attack);
        CustomPlayerPrefs.SetInt(PlayerPrefsKey.DEFENSE, zodiac.defense);
        CustomPlayerPrefs.SetInt(PlayerPrefsKey.LEADERSHIP, zodiac.leadership);
        CustomPlayerPrefs.SetInt(PlayerPrefsKey.TACTIC, zodiac.tactic);
        CustomPlayerPrefs.SetInt(PlayerPrefsKey.MORALITY, zodiac.morality);
        CustomPlayerPrefs.SetInt(PlayerPrefsKey.GOODNESS, zodiac.goodness);
        CustomPlayerPrefs.SetInt(PlayerPrefsKey.SENSIBILITY, zodiac.sensibility);
        CustomPlayerPrefs.SetInt(PlayerPrefsKey.ARTS, zodiac.arts);

        CustomPlayerPrefs.SetInt(PlayerPrefsKey.TARGET, -1);
        CustomPlayerPrefs.SetInt(PlayerPrefsKey.FEELING, Def.MIN_FEELING);
        CustomPlayerPrefs.SetInt(PlayerPrefsKey.LAST_OPENED_SCENARIO_NO, Def.MIN_SCENARIO - 1);
    }

}   // class
