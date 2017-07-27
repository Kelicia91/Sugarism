using UnityEngine;


public class DataTableCollection : MonoBehaviour
{
    // @note : Register this to TagManager
    public const string TAG = "DataTableCollection";

	// Use this for initialization
	void Start ()
    {
        Log.Debug("DataTableCollection.Start");
        DontDestroyOnLoad(this);
	}

    /***** KYI : DataTable *****/
    [SerializeField]
    private ActionObject AsstDTAction = null;
    public ActionObject Action { get { return AsstDTAction; } }

    [SerializeField]
    private ActionLessonObject AsstDTActionLesson = null;
    public ActionLessonObject ActionLesson { get { return AsstDTActionLesson; } }

    [SerializeField]
    private ActionNPCObject AsstDTActionNPC = null;
    public ActionNPCObject ActionNPC { get { return AsstDTActionNPC; } }

    [SerializeField]
    private ActionPartTimeObject AsstDTActionPartTime = null;
    public ActionPartTimeObject ActionPartTime { get { return AsstDTActionPartTime; } }

    [SerializeField]
    private ActionTypeObject AsstDTActionType = null;
    public ActionTypeObject ActionType { get { return AsstDTActionType; } }

    [SerializeField]
    private BackgroundObject AsstDTBackground = null;
    public BackgroundObject Background { get { return AsstDTBackground; } }

    [SerializeField]
    private BoardGamePlayerObject AsstDTBoardGamePlayer = null;
    public BoardGamePlayerObject BoardGamePlayer { get { return AsstDTBoardGamePlayer; } }

    [SerializeField]
    private CharacterObject AsstDTCharacter = null;
    public CharacterObject Character { get { return AsstDTCharacter; } }

    [SerializeField]
    private CombatPlayerObject AsstDTCombatPlayer = null;
    public CombatPlayerObject CombatPlayer { get { return AsstDTCombatPlayer; } }

    [SerializeField]
    private ConstitutionObject AsstDTConstitution = null;
    public ConstitutionObject Constitution { get { return AsstDTConstitution; } }

    [SerializeField]
    private MainCharacterCostumeObject AsstDTMainCharacterCostume = null;
    public MainCharacterCostumeObject MainCharacterCostume { get { return AsstDTMainCharacterCostume; } }

    [SerializeField]
    private MainCharacterLooksObject AsstDTMainCharacterLooks = null;
    public MainCharacterLooksObject MainCharacterLooks { get { return AsstDTMainCharacterLooks; } }

    [SerializeField]
    private MiniPictureObject AsstDTMiniPicture = null;
    public MiniPictureObject MiniPicture { get { return AsstDTMiniPicture; } }

    [SerializeField]
    private NurtureEndingObject AsstDTNurtureEnding = null;
    public NurtureEndingObject NurtureEnding { get { return AsstDTNurtureEnding; } }

    [SerializeField]
    private OneToOneExamObject AsstDTOneToOneExam = null;
    public OneToOneExamObject OneToOneExam { get { return AsstDTOneToOneExam; } }

    [SerializeField]
    private PictureObject AsstDTPicture = null;
    public PictureObject Picture { get { return AsstDTPicture; } }

    [SerializeField]
    private RivalObject AsstDTRival = null;
    public RivalObject Rival { get { return AsstDTRival; } }

    [SerializeField]
    private ScoreExamObject AsstDTScoreExam = null;
    public ScoreExamObject ScoreExam { get { return AsstDTScoreExam; } }

    [SerializeField]
    private ScorePlayerObject AsstDTScorePlayer = null;
    public ScorePlayerObject ScorePlayer { get { return AsstDTScorePlayer; } }

    [SerializeField]
    private SEObject AsstDTSE = null;
    public SEObject SE { get { return AsstDTSE; } }

    [SerializeField]
    private StatObject AsstDTStat = null;
    public StatObject Stat { get { return AsstDTStat; } }

    [SerializeField]
    private TargetObject AsstDTTarget = null;
    public TargetObject Target { get { return AsstDTTarget; } }

    [SerializeField]
    private VacationObject AsstDTVacation = null;
    public VacationObject Vacation { get { return AsstDTVacation; } }

    [SerializeField]
    private ZodiacObject AsstDTZodiac = null;
    public ZodiacObject Zodiac { get { return AsstDTZodiac; } }

}   // class
