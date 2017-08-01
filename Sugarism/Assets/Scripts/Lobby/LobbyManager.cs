using UnityEngine;


// prohibit from abusing Singleton.
// @IMPORTANT : prohibit from re-define constuctor because of inheriting MonoBehaviour.
public class LobbyManager : MonoBehaviour
{
    private static LobbyManager _instance = null;
    public static LobbyManager Instance { get { return _instance; } }


    /********* Editor Interface *********/
    // Prefabs
    [SerializeField]
    private DataTableCollection PrefDataTableCollection = null;
    [SerializeField]
    private PlayerInitProperty PrefPlayerInitProperty = null;
    [SerializeField]
    private LobbyUIManager PrefLobbyUIManager = null;

    /********* Game Interface *********/
    private DataTableCollection _dataTableCollection = null;
    public DataTableCollection DT { get { return _dataTableCollection; } }

    private PlayerInitProperty _playerInitProperty = null;
    public PlayerInitProperty PlayerInitProperty { get { return _playerInitProperty; } }

    private LobbyUIManager _ui = null;
    public LobbyUIManager UI { get { return _ui; } }


    //
    void Awake()
    {
        _instance = this;
        
        // data table
        GameObject dtObject = GameObject.FindWithTag(DataTableCollection.TAG);
        if (null == dtObject)
            _dataTableCollection = Instantiate(PrefDataTableCollection);
        else
            _dataTableCollection = dtObject.GetComponent<DataTableCollection>();

        // manager
        _playerInitProperty = Instantiate(PrefPlayerInitProperty);        
        _ui = Instantiate(PrefLobbyUIManager);

        // @todo: move below code to main object of first scene loaded when start game
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.SetResolution(Screen.width, (Screen.width / Def.RESOLUTION_WIDTH_RATIO * Def.RESOLUTION_HEIGHT_RATIO), true);
        Log.Debug(string.Format("Screen width: {0}, height: {1}", Screen.width, Screen.height));
    }
    
    //
    public bool IsSavedData()
    {
        string invalidName = string.Empty;
        string playerName = CustomPlayerPrefs.GetString(PlayerPrefsKey.NAME, invalidName);
        if (playerName.Equals(invalidName))
            return false;
        else
            return true;
    }

    public void NewStart()
    {
        initialize();

        loadScene(SceneDef.MAIN);
    }

    public void Continue()
    {
        loadScene(SceneDef.MAIN);
    }

    private void loadScene(string sceneName)
    {
        Log.Debug(string.Format("==========> LoadScene; {0} <==========", sceneName));
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    private void initialize()
    {
        // DeleteAll() 바로 적용은 안 되는듯. 약간의 딜레이 필요.
        CustomPlayerPrefs.DeleteAll();  // @WARN : 업적 관련 key 추가 되면 호출 x.

        int constitutionId = (int)PlayerInitProperty.Constitution;
        int zodiacId = (int)PlayerInitProperty.Zodiac;
        int condition = (int)Nurture.ECondition.Healthy;

        CustomPlayerPrefs.SetInt(PlayerPrefsKey.YEAR, Def.INIT_YEAR);
        CustomPlayerPrefs.SetInt(PlayerPrefsKey.MONTH, Def.INIT_MONTH);

        CustomPlayerPrefs.SetString(PlayerPrefsKey.NAME, PlayerInitProperty.Name);
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
