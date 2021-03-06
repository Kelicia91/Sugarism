﻿/*
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


// 스텟 계열
public enum EStatLine : byte
{
    NONE        = 0x0,

    ATTACTER    = 0x1,
    POLITICIAN  = 0x2,
    TRICKER     = 0x4,
    FIGHTER     = 0x8,

    MAX         = byte.MaxValue
}



// 나중에 파일로 빼자.
public class Def
{
    // Resolution
    //public const int RESOLUTION_WIDTH = 900;
    //public const int RESOLUTION_HEIGHT = 1600;
    public const int RESOLUTION_WIDTH_RATIO = 9;
    public const int RESOLUTION_HEIGHT_RATIO = 16;

    // UI
    public const string PROGRESS = "진행도";

    // Command in UI
    public const string CMD_SCHEDULE_NAME = "일 정";
    public const string CMD_RUN_SCHEDULE_NAME = "실 행";
    public const string CMD_WARDROBE_NAME = "옷 장";
    public const string CMD_STATE_NAME = "상 태";
    public const string CMD_FEELING_CHECK = "친밀도";
    public const string CMD_ENDING = "엔 딩";
    public const string CMD_GO_OUT_NAME = "외 출";
    public const string CMD_TEST_BOARD_GAME = "보드게임";
    public const string CMD_TEST_COMBAT = "일기토";

    // Currency
    public const int INIT_MONEY = 1500;
    public const int MIN_MONEY = 0;
    public const int MAX_MONEY = 999999;
    public const string MONEY_FORMAT = "은전 {0}";

    // Nurture
    public const int NURTURE_BAD_ENDING_ID = 0;

    // Stat
    public const int MIN_STAT = 0;
    public const int MAX_STAT = 999;

    // Calendar
    public const int INIT_YEAR = 199;
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

    // Main Character
    public const int MAIN_CHARACTER_ID = 0; // DTCharacter, DTBoardGamePlayer, DTCombatPlayer
    public const int INIT_AGE = 16;
    public const int PERIOD_YEAR = 4;
    public const int MAX_AGE = INIT_AGE + PERIOD_YEAR;
    public const int DEFAULT_COSTUME_ID = 0;
    public const string PUT_OFF_COSTUME = "해  제";
    public const string PUT_ON_COSTUME = "착  용";
    public const string STRESS_FORMAT = "스트레스 {0}";
    public const int SICK_WARNING = 10;
    public const int SICK_MAX = 100;

    // Story
    public const string STORY_ENDING_START_MSG = "그러던 어느 날...";

    // Target Character
    public const string CMD_SELECT_TARGET = "공략 대상 선택";
    public const int MIN_FEELING = 0;
    public const int MAX_FEELING = 100;
    public const int ENDING_MIN_FEELING_PERCENT = 80;
    public const int MIN_SCENARIO = 1;
    public const int MAX_SCENARIO = Nurture.Calendar.MAX_MONTH * PERIOD_YEAR;

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
    public const string HP = "H  P";
    public const string MP = "M  P";
    public const string WARRIOR_VALUES = "전사평가";
    public const string TRICKER_VALUES = "책사평가";
    public const string COMBAT_CMD_ATTACK = "공 격";
    public const string COMBAT_CMD_TRICK = "책 략";
    public const string COMBAT_COMMENT_PLAYER_TURN = "의 차례!";
    public const string COMBAT_COMMENT_WINNER = " 승리!";

    // Lesson
    public const int EXAM_PERIOD = 4;   // divide ActionCount.
    public const string EXAM_USER_START = "(후우- 조금 긴장되는걸..?)";

    // Lesson - Rival
    public const int RIVAL_MEET_COUNT_FOR_SPECIAL_SCENARIO = 4;

    // Lesson - OneToOne Exam(Combat, BoardGame)
    public const string EXAM_USER_WIN = "이겼다!";
    public const string EXAM_USER_LOSE = "칫..";

    // Lesson - Combat Exam
    public const float COMBAT_RIVAL_AI_STAT_MIN_RATIO = 0.95f;
    public const float COMBAT_RIVAL_AI_STAT_MAX_RATIO = 1.10f;

    // Lesson - BoardGame Exam
    public const float BOARDGAME_RIVAL_AI_STAT_MIN_RATIO = 0.90f;
    public const float BOARDGAME_RIVAL_AI_STAT_MAX_RATIO = 1.10f;

    // Lesson - Score Exam
    public const int SCORE_EXAM_EXCELLENT_STRESS = -30;
    public const int SCORE_EXAM_BAD_STRESS = 30;

    public const float SCORE_RIVAL_AI_STAT_MIN_RATIO = 0.95f;
    public const float SCORE_RIVAL_AI_STAT_MAX_RATIO = 1.10f;

    public const string SCORE_EXAM_USER_COMMENT_REACT_S = "(후훗, 이정도야 뭘.)";
    public const string SCORE_EXAM_USER_COMMENT_REACT_A = "(좋았어, 좀더 분발하자!)";
    public const string SCORE_EXAM_USER_COMMENT_REACT_B = "(..뭐, 나쁘진 않네.)";
    public const string SCORE_EXAM_USER_COMMENT_REACT_C = "모..몸상태가 안 좋았던 것 뿐이에요..! 하..하..";
    public const string SCORE_EXAM_USER_COMMENT_REACT_D = "우우.. 창피해..";

    // Poem
    public const string USER_POEM_S = "S등급 시 blah-blah";
    public const string USER_POEM_A = "A등급 시 blah-blah";
    public const string USER_POEM_B = "B등급 시 blah-blah";
    public const string USER_POEM_C = "C등급 시 blah-blah";
    public const string USER_POEM_D = "D등급 시 blah-blah";

    // Lobby
    public const string CMD_NEW_START = "시 작 하 기";
    public const string CMD_CONTINUE = "이 어 하 기";
    public const string CMD_ALBUM = "회 상 록";
    public const string GUIDE_FORM_PANEL = "이름을 입력해주세요.";
    public const int MIN_LENGTH_PLAYER_NAME = 2;
    public const int MAX_LENGTH_PLAYER_NAME = 5;
    public const string DEFAULT_PLAYER_NAME = "화담";    
    public const string GUIDE_PLAYER_NAME = "최소 {0}, 최대 {1}글자";
    public const string TOO_SHORT_PLAYER_NAME = "이름이 너무 짧습니다.\n(최소 {0}글자)";
    public const string TOO_LONG_PLAYER_NAME = "이름이 너무 깁니다.\n(최대 {0}글자)";
    public const string NEXT = "다음";
    public const string START_GAME = "시작하기";
    public const string GUIDE_SELECT = "{0}을 선택해주세요.";

    // Constitution
    public const string CONSTITUTION = "체질";
    public const EConstitution DEFAULT_CONSTITUTION = EConstitution.MOON;

    // Zodiac
    public const string ZODIAC = "황도궁";
    public const EZodiac DEFAULT_ZODIAC = EZodiac.RAT;

    // Album
    public const string ETC = "그외";
    public const string ALBUM_TYPE_NAME_PICTURE = "일러스트";
    public const string ALBUM_TYPE_NAME_MINIPICTURE = "미니컷";
    public const string ALBUM_TYPE_NAME_NURTURE_ENDING = "엔딩";
    public const string ALBUM_TYPE_NAME_VACTION = "휴양지";
}




/***** Definition For Programmer *****/

public class SceneDef
{
    public const string ENTRY = "Entry";
    public const string LOBBY = "Lobby";
    public const string MAIN = "Main";
}

public class PlayerPrefsKey
{
    public const string YEAR = "year";
    public const string MONTH = "month";

    public const string NAME = "name";
    public const string ZODIAC = "zodiac";
    public const string CONDITION = "condition";
    public const string CONSTITUTION = "constitution";
    public const string MONEY = "money";
    public const string WEARING_COSTUME = "wearingCostume";

    public const string STRESS = "stress";
    public const string STAMINA = "stamina";
    public const string INTELLECT = "intellect";
    public const string GRACE = "grace";
    public const string CHARM = "charm";
    public const string ATTACK = "attack";
    public const string DEFENSE = "defense";
    public const string LEADERSHIP = "leadership";
    public const string TACTIC = "tactic";
    public const string MORALITY = "morality";
    public const string GOODNESS = "goodness";
    public const string SENSIBILITY = "sensibility";
    public const string ARTS = "arts";

    public const string TARGET = "target";
    public const string FEELING = "feeling";
    public const string LAST_OPENED_SCENARIO_NO = "lastOpenedScenarioNo";
    
    /*** Prefix key ***/
    public const string ACTION_COUNT = "actionCount";
    public const string ISBUY_COSTUME = "isBuyCostume";
    public const string ISLOCKED_PICTURE = "isLockedPicture";
    public const string ISLOCKED_MINIPICTURE = "isLockedMiniPicture";
    public const string ISLOCKED_NURTURE_ENDING = "isLockedNurtureEnding";
    public const string ISLOCKED_VACATION_CHILD = "isLockedVacationChild";
    public const string ISLOCKED_VACATION_ADULT = "isLockedVacationAdult";

    public static string GetKey(string prefixKey, int id)
    {
        string key = string.Format("{0}{1}", prefixKey, id);
        return key;
    }

    /*** Utils ***/
    public const int TRUE_INTEGER = 1;
    public const int FALSE_INTEGER = 0;

    public static int GetBoolToInt(bool boolean)
    {
        // If boolean is true, return 1. otherwise 0.
        return System.Convert.ToInt32(boolean);
    }
    
    public static bool GetIntToBool(int value)
    {
        // If value is 0, return false. otherwise true.
        return System.Convert.ToBoolean(value);
    }
}