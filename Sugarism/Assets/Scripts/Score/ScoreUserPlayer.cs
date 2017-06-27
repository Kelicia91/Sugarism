
namespace Score
{
    public class UserPlayer : Player
    {
        public UserPlayer() : base(Def.MAIN_CHARACTER_ID)
        {
            Nurture.Character c = Manager.Instance.Object.NurtureMode.Character;
            _stress = c.Stress;
            _charm = c.Charm;
            _sensibility = c.Sensibility;
            _arts = c.Arts;
        }

        public string GetPoem(EGrade grade)
        {
            switch (grade)
            {
                case EGrade.S:
                    return Def.USER_POEM_S;

                case EGrade.A:
                    return Def.USER_POEM_A;

                case EGrade.B:
                    return Def.USER_POEM_B;

                case EGrade.C:
                    return Def.USER_POEM_C;

                case EGrade.D:
                    return Def.USER_POEM_D;

                default:
                    Log.Error(string.Format("invalid grade; {0}", grade));
                    return null;
            }
        }

        public string GetCommentReact(EGrade grade)
        {
            switch (grade)
            {
                case EGrade.S:
                    return Def.SCORE_EXAM_USER_COMMENT_REACT_S;

                case EGrade.A:
                    return Def.SCORE_EXAM_USER_COMMENT_REACT_A;

                case EGrade.B:
                    return Def.SCORE_EXAM_USER_COMMENT_REACT_B;

                case EGrade.C:
                    return Def.SCORE_EXAM_USER_COMMENT_REACT_C;

                case EGrade.D:
                    return Def.SCORE_EXAM_USER_COMMENT_REACT_D;

                default:
                    Log.Error(string.Format("invalid grade; {0}", grade));
                    return null;
            }
        }

    }   // class

}   // namespace