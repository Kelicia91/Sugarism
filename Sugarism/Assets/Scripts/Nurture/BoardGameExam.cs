using System.Collections;
using UnityEngine;

namespace Exam
{
    public class BoardGameExam : Exam
    {
        //
        private readonly BoardGame.EValuationBasis _valuationBasis = BoardGame.EValuationBasis.MAX;
        private readonly OneToOneExam _exam;
        
        private string _userName = null;
        private string _rivalName = null;

        private string _userFirstResultLines = null;
        private string _userResultLines = null;
        private string _rivalResultLines = null;

        // constructor
        public BoardGameExam(EType type, int id, int npcId, int rivalId, bool isFirst) : base(type, id, npcId, rivalId, isFirst)
        {
            switch (type)
            {
                case EType.BOARD_GAME_TRICKER:
                    _valuationBasis = BoardGame.EValuationBasis.Tricker;
                    break;

                case EType.BOARD_GAME_POLITICIAN:
                    _valuationBasis = BoardGame.EValuationBasis.Politician;
                    break;

                default:
                    Log.Error(string.Format("not converted from ExamType({0}) to BoardGame.EValuationBasis", type));
                    break;
            }

            if (false == ExtOneToOneExam.isValid(id))
                Log.Error(string.Format("invalid one-to-one exam.id : {0}", id));
            else
                _exam = Manager.Instance.DT.OneToOneExam[id];

            _userName = Manager.Instance.Object.MainCharacter.Name;

            if (false == ExtCharacter.IsValid(_rival.characterId))
                Log.Error(string.Format("invalid character id: {0}", _rival.characterId));
            else
                _rivalName = Manager.Instance.DT.Character[_rival.characterId].name;

            Manager.Instance.Object.BoardGameMode.EndEvent.Attach(onEnd);
        }

        protected override IEnumerator ExamRoutine()
        {
            DialogueEvent.Invoke(NPCId, _exam.NPCAlarm);
            yield return null;

            if (IsFirst)
            {
                DialogueEvent.Invoke(NPCId, _exam.NPCProcessMethod);
                yield return null;

                DialogueEvent.Invoke(NPCId, _exam.NPCEvaluateMethod);
                yield return null;
            }

            string startMsg = string.Format(_exam.NPCStartPlayersName, _userName, _rivalName);
            DialogueEvent.Invoke(NPCId, startMsg);
            yield return null;

            DialogueEvent.Invoke(Def.EXAM_USER_START);
            yield return null;

            //
            BoardGame.UserPlayer userPlayer = new BoardGame.UserPlayer(Manager.Instance.Object.BoardGameMode);
            BoardGame.AIPlayer aiPlayer = getRivalPlayer(_rival.boardGamePlayerId, userPlayer);

            Manager.Instance.Object.BoardGameMode.Start(_valuationBasis, userPlayer, aiPlayer);
            yield return null;

            if (IsFirst)
            {
                DialogueEvent.Invoke(_userFirstResultLines);
                yield return null;

                Log.Debug("@todo: open rival first meet scenerio");
            }
            else
            {
                DialogueEvent.Invoke(_rival, _rivalResultLines);
                yield return null;

                DialogueEvent.Invoke(_userResultLines);
                yield return null;
            }

            Manager.Instance.Object.BoardGameMode.EndEvent.Detach(onEnd);
        }
        
        private void onEnd(BoardGame.EUserGameState state)
        {
            string winnerName = null;
            switch (state)
            {
                case BoardGame.EUserGameState.Win:
                    winnerName = _userName;
                    _userFirstResultLines = Def.EXAM_USER_WIN;
                    _rivalResultLines = _exam.RivalLose;
                    _userResultLines = _exam.UserWin;
                    break;

                case BoardGame.EUserGameState.Lose:
                    winnerName = _rivalName;
                    _userFirstResultLines = Def.EXAM_USER_LOSE;
                    _rivalResultLines = _exam.RivalWin;
                    _userResultLines = _exam.UserLose;
                    break;

                default:
                    Log.Error(string.Format("invalid user game state: {0}", state));
                    break;
            }

            string resultMsg = string.Format(_exam.NPCEndWinnerName, winnerName);
            DialogueEvent.Invoke(NPCId, resultMsg);
        }

        private BoardGame.AIPlayer getRivalPlayer(int boardGamePlayerId, BoardGame.UserPlayer userPlayer)
        {
            if (null == userPlayer)
            {
                Log.Error("not found board game.user player");
                return null;
            }

            int intellect = 0, tactic = 0, leadership = 0, grace = 0, morality = 0, goodness = 0;
            switch(_valuationBasis)
            {
                case BoardGame.EValuationBasis.Tricker:
                    intellect = getRivalStat(userPlayer.Intellect, Def.BOARDGAME_RIVAL_AI_STAT_MIN_RATIO, Def.BOARDGAME_RIVAL_AI_STAT_MAX_RATIO);
                    tactic = getRivalStat(userPlayer.Tactic, Def.BOARDGAME_RIVAL_AI_STAT_MIN_RATIO, Def.BOARDGAME_RIVAL_AI_STAT_MAX_RATIO);
                    leadership = getRivalStat(userPlayer.Leadership, Def.BOARDGAME_RIVAL_AI_STAT_MIN_RATIO, Def.BOARDGAME_RIVAL_AI_STAT_MAX_RATIO);
                    break;

                case BoardGame.EValuationBasis.Politician:
                    grace = getRivalStat(userPlayer.Grace, Def.BOARDGAME_RIVAL_AI_STAT_MIN_RATIO, Def.BOARDGAME_RIVAL_AI_STAT_MAX_RATIO);
                    morality = getRivalStat(userPlayer.Morality, Def.BOARDGAME_RIVAL_AI_STAT_MIN_RATIO, Def.BOARDGAME_RIVAL_AI_STAT_MAX_RATIO);
                    goodness = getRivalStat(userPlayer.Goodness, Def.BOARDGAME_RIVAL_AI_STAT_MIN_RATIO, Def.BOARDGAME_RIVAL_AI_STAT_MAX_RATIO);
                    break;

                default:
                    Log.Error("invalid BoardGame.EValuationBasis");
                    break;
            }

            return new BoardGame.AIPlayer(Manager.Instance.Object.BoardGameMode, boardGamePlayerId
                                    , intellect, tactic, leadership, grace, morality, goodness);
        }

        private int getRivalStat(int baseStat, float minRaio, float maxRatio)
        {
            int min = Mathf.RoundToInt(baseStat * minRaio);
            min = Nurture.Character.Adjust(min);

            int max = Mathf.RoundToInt(baseStat * maxRatio);
            max = Nurture.Character.Adjust(max);

            return Random.Range(min, (max + 1));
        }
    }
}
