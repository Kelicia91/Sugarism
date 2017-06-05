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
    // UI
    public const string BACK = "뒤로";

    // Calendar
    public const int INIT_YEAR = 190;
    public const int PERIOD_YEAR = 8;
    public const int INIT_MONTH = 1;   // 탄생월은 나중에 user-defined. (zodiac)
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
    public const string CMD_STATE_NAME = "상 태";
    public const string CMD_RUN_SCHEDULE_NAME = "실 행";

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
    public const int INIT_AGE = 12;
    public const string STRESS_FORMAT = "스트레스 {0}";

    // Stat
    public const int MIN_STAT = 0;
    public const int MAX_STAT = 999;
}