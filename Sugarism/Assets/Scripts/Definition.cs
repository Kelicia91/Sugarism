/*
 *  절대 enum 함부로 고치지 말 것.
 *  데이터 테이블 날아갈 수 있으니 반드시 백업하고 수정할 것!!
 */

// 체질 (음양오행)
public enum EConstitution
{
    SUN = 0,
    MOON,
    FIRE,
    WATER,
    TREE,
    METAL,
    EARTH,

    MAX
}


// 중국 황도궁
public enum EZodiac
{
    RAT = 0,
    COW,
    TIGER,
    RABBIT,

    DRAGON,
    SNAKE,
    HORSE,
    SHEEP,

    MONKEY,
    CHICKEN,
    DOG,
    PIG,

    MAX
}


// 계절
public enum ESeason
{
    SPRING = 0,
    SUMMER,
    FALL,
    WINTER,

    MAX
}


// 행동 타입
public enum EActionType
{
    PARTTIME = 0,
    LESSON,
    RELAX,

    IDLE,
    MAX = IDLE
}


// 스텟
public enum EStat
{
    STRESS = 0,

    STAMINA,
    INTELLECT,
    GRACE,
    CHARM,

    ATTACK,
    DEFENSE,

    LEADERSHIP,
    TACTIC,

    MORALITY,
    GOODNESS,

    SENSIBILITY,
    ARTS,

    MAX
}



// 나중에 파일로 빼자.
public class Def
{
    // Resolution
    public const int RESOLUTION_WIDTH = 540;
    public const int RESOLUTION_HEIGHT = 960;
    public const int RESOLUTION_WIDTH_RATIO = 9;
    public const int RESOLUTION_HEIGHT_RATIO = 16;

    // UI
    public const string BACK = "뒤로";

    // Calendar
    public const int INIT_YEAR = 199;
    public const int PERIOD_YEAR = 4;
    public const int INIT_MONTH = 1;
    public const int INIT_DAY = 1;
    public const string YEAR_UNIT = "年";
    public const string MONTH_UNIT = "月";
    public const string DAY_UNIT = "日";

    // Schedule
    public const int MAX_NUM_ACTION_IN_MONTH = 3;
    public const string WEEK_FORMAT = "{0}주 {1}";
    public const string START_SCHEDULE_DESC = "스케줄을 시작합니다.";
    public const string ALARM_LACK_MONEY_DESC = "은전이 부족합니다. 일정을 변경합니다.";

    // Currency
    public const int INIT_MONEY = 500;
    public const int MIN_MONEY = 0;
    public const int MAX_MONEY = 99999999;
    public const string MONEY_FORMAT = "은전 {0}";

    // Command in UI
    public const string CMD_SCHEDULE_NAME = "일 정";
    public const string CMD_RUN_SCHEDULE_NAME = "실 행";
    public const string CMD_GO_OUT_NAME = "외 출";
    public const string CMD_STATE_NAME = "상 태";
    public const string CMD_TEST_BOARD_GAME = "보드게임";
    public const string CMD_TEST_COMBAT = "일기토";

    // Action
    public const int ACTION_IDLE_ID = 0;
    public const int ACTION_VACATION_ID = 3;
    public const float CRITICAL_WEIGHT = 0.3f;
    public const string ACHIVEMENT_RATIO_FORMAT = "성과도 {0} %";
    public const string ACTION_BEGIN_DESC_FORMAT = "오늘부터 {0}.";
    public const string ACTION_PARTTIME_DOING_SUCCESS_DESC = "오늘은 무사히 일을 마쳤다!";
    public const string ACTION_PARTTIME_DOING_FAIL_DESC = "오늘은 실수를 해버렸다..";
    public const string ACTION_LESSON_DOING_SUCCESS_DESC = "오늘은 수업을 잘 들었다!";
    public const string ACTION_LESSON_DOING_FAIL_DESC = "오늘은 수업에 집중하지 못 했다..";

    // Profile
    public const string PROFILE_FORMAT = "{0} ({1})\n{2}, {3}";

    // Main Character
    public const int MAIN_CHARACTER_ID = 0; // DTCharacter, DTBoardGamePlayer, DTCombatPlayer
    public const int INIT_AGE = 12;
    public const string STRESS_FORMAT = "스트레스 {0}";

    // Stat
    public const int MIN_STAT = 0;
    public const int MAX_STAT = 999;

    // Target Character
    public const int MIN_FEELING = 0;
    public const int MAX_FEELING = 100;
    public const int MIN_SCENARIO = 1;// 시나리오는 이 번호부터 시작한다.
    public const int MAX_SCENARIO = 1;//Calendar.MAX_MONTH * PERIOD_YEAR;   // 12 * 4

    // Lines
    public const string ANONYMOUS = "???";

    // Battle
    public const string START = "START";
    public const string WIN = "W I N";
    public const string LOSE = "L O S E";

    // BoardGame
    public const string BOARD_CRITERION_LOW = "L";
    public const string BOARD_CRITERION_HIGH = "H";

    // Combat
    public const string HP = "HP";
    public const string MP = "MP";
    public const string WARRIOR_VALUES = "전사평가";
    public const string TRICKER_VALUES = "책사평가";
    public const string COMBAT_CMD_ATTACK = "공 격";
    public const string COMBAT_CMD_TRICK = "책 략";
    public const string COMBAT_COMMENT_PLAYER_TURN = "의 차례!";
}