using UnityEngine;


// Singleton 남발 방지용.
// @IMPORTANT : MonoBehaviour를 상속받은 이상 생성자 재정의 금지!!
public class Manager : MonoBehaviour
{
    private static Manager _instance = null;
    public static Manager Instance { get { return _instance; } }
    
    void Awake()
    {
        _instance = this;

        // custom event
        _moneyChangeEvent = new MoneyChangeEvent();

        // manager
        _object = Instantiate(PrefObjectManager);
        _ui = Instantiate(PrefUIManager);

        // game
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.SetResolution(Screen.width, (Screen.width / Def.RESOLUTION_WIDTH_RATIO * Def.RESOLUTION_HEIGHT_RATIO), true);
        Log.Debug(string.Format("Screen width: {0}, height: {1}", Screen.width, Screen.height));
    }


    /********* Editor Interface *********/
    // Prefabs
    public ObjectManager PrefObjectManager;
    public UIManager PrefUIManager;

    // KYI : DataTable assets
    public ActionObject AsstDTAction;
    public ActionLessonObject AsstDTActionLesson;
    public ActionNPCObject AsstDTActionNPC;
    public ActionPartTimeObject AsstDTActionPartTime;
    public ActionTypeObject AsstDTActionType;
    public BackgroundObject AsstDTBackground;
    public BoardGamePlayerObject AsstDTBoardGamePlayer;
    public CharacterObject AsstDTCharacter;
    public CombatPlayerObject AsstDTCombatPlayer;
    public ConstitutionObject AsstDTConstitution;
    public MainCharacterCostumeObject AsstDTMainCharacterCostume;
    public MainCharacterLooksObject AsstDTMainCharacterLooks;
    public MiniPictureObject AsstDTMiniPicture;
    public OneToOneExamObject AsstDTOneToOneExam;
    public PictureObject AsstDTPicture;
    public RivalObject AsstDTRival;
    public ScoreExamObject AsstDTScoreExam;
    public SEObject AsstDTSE;
    public StatObject AsstDTStat;
    public TargetObject AsstDTTarget;
    public VacationObject AsstDTVacation;
    public ZodiacObject AsstDTZodiac;
    

    /********* Game Interface *********/
    // Manager
    private ObjectManager _object = null;
    public ObjectManager Object { get { return _object; } }

    private UIManager _ui = null;
    public UIManager UI { get { return _ui; } }

    // KYI : DataTable
    public ActionObject DTAction { get { return AsstDTAction; } }
    public ActionLessonObject DTActionLesson { get { return AsstDTActionLesson; } }
    public ActionNPCObject DTActionNPC { get { return AsstDTActionNPC; } }
    public ActionPartTimeObject DTActionPartTime { get { return AsstDTActionPartTime; } }
    public ActionTypeObject DTActionType { get { return AsstDTActionType; } }
    public BackgroundObject DTBackground { get { return AsstDTBackground; } }
    public BoardGamePlayerObject DTBoardGamePlayer { get { return AsstDTBoardGamePlayer; } }
    public CharacterObject DTCharacter { get { return AsstDTCharacter; } }
    public CombatPlayerObject DTCombatPlayer { get { return AsstDTCombatPlayer; } }
    public ConstitutionObject DTConstitution { get { return AsstDTConstitution; } }
    public MainCharacterCostumeObject DTMainCharacterCostume { get { return AsstDTMainCharacterCostume; } }
    public MainCharacterLooksObject DTMainCharacterLooks { get { return AsstDTMainCharacterLooks; } }
    public MiniPictureObject DTMiniPicture { get { return AsstDTMiniPicture; } }
    public OneToOneExamObject DTOneToOneExam { get { return AsstDTOneToOneExam; } }
    public PictureObject DTPicture { get { return AsstDTPicture; } }
    public RivalObject DTRival { get { return AsstDTRival; } }
    public ScoreExamObject DTScoreExam { get { return AsstDTScoreExam; } }
    public SEObject DTSE { get { return AsstDTSE; } }
    public StatObject DTStat { get { return AsstDTStat; } }
    public TargetObject DTTarget { get { return AsstDTTarget; } }
    public VacationObject DTVacation { get { return AsstDTVacation; } }
    public ZodiacObject DTZodiac { get { return AsstDTZodiac; } }

    // CustomEvent
    private MoneyChangeEvent _moneyChangeEvent = null;
    public MoneyChangeEvent MoneyChangeEvent { get { return _moneyChangeEvent; } }
}
 
 