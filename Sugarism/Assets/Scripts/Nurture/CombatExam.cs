using System.Collections;
using UnityEngine;

namespace Exam
{
    public class CombatExam : Exam
    {
        //
        private readonly OneToOneExam _exam;
        
        private string _userName = null;
        private string _rivalName = null;

        private string _userFirstResultLines = null;
        private string _userResultLines = null;
        private string _rivalResultLines = null;

        // constructor
        public CombatExam(int id, int npcId, int rivalId, bool isFirst) : base(EType.COMBAT, id, npcId, rivalId, isFirst)
        {
            if (false == ExtOneToOneExam.isValid(id))
                Log.Error(string.Format("invalid one-to-one exam.id : {0}", id));
            else
                _exam = Manager.Instance.DTOneToOneExam[id];
            
            _userName = Manager.Instance.Object.MainCharacter.Name;

            if (false == ExtCharacter.IsValid(_rival.characterId))
                Log.Error(string.Format("invalid character id: {0}", _rival.characterId));
            else
                _rivalName = Manager.Instance.DTCharacter[_rival.characterId].name;

            Manager.Instance.Object.CombatMode.EndEvent.Attach(onEnd);
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
            Combat.UserPlayer userPlayer = new Combat.UserPlayer(Manager.Instance.Object.CombatMode);
            Combat.AIPlayer aiPlayer = getRivalPlayer(_rival.combatPlayerId, userPlayer);

            Manager.Instance.Object.CombatMode.Start(userPlayer, aiPlayer);
            yield return null;
            
            // RIVAL
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

            Manager.Instance.Object.CombatMode.EndEvent.Detach(onEnd);
        }

        private void onEnd(Combat.CombatMode.EUserGameState state)
        {
            string winnerName = null;
            switch (state)
            {
                case Combat.CombatMode.EUserGameState.Win:
                    winnerName = _userName;
                    _userFirstResultLines = Def.EXAM_USER_WIN;
                    _rivalResultLines = _exam.RivalLose;
                    _userResultLines = _exam.UserWin;
                    break;

                case Combat.CombatMode.EUserGameState.Lose:
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

        private Combat.AIPlayer getRivalPlayer(int combatPlayerId, Combat.UserPlayer userPlayer)
        {
            if (null == userPlayer)
            {
                Log.Error("not found combat.userplayer");
                return null;
            }

            if (false == ExtCombatPlayer.IsValid(combatPlayerId))
            {
                Log.Error(string.Format("invalid combat player id; {0}", combatPlayerId));
                return null;
            }

            // @note: CombatPlayer(DataTable) differ from Combat.UserPlayer, Combat.AIPlayer
            CombatPlayer rivalPlayer = Manager.Instance.DTCombatPlayer[combatPlayerId];

            int hp = Mathf.RoundToInt(userPlayer.Hp * Def.COMBAT_RIVAL_AI_STAT_MAX_RATIO);
            hp = Nurture.Character.Adjust(hp);
            
            int criticalProbability = userPlayer.CriticalProbability;

            int mp = 0, attack = 0, defense = 0, intellect = 0, tactic = 0;
            switch (rivalPlayer.orient)
            {
                case Combat.AIPlayer.EOrient.Attack:
                    mp = Def.MIN_STAT;
                    attack = Nurture.Character.Adjust(Mathf.RoundToInt(userPlayer.AttackPower * Def.COMBAT_RIVAL_AI_STAT_MAX_RATIO));
                    defense = getRivalStat(userPlayer.Defense, Def.COMBAT_RIVAL_AI_STAT_MIN_RATIO, Def.COMBAT_RIVAL_AI_STAT_MAX_RATIO);
                    intellect = Def.MIN_STAT;
                    tactic = Nurture.Character.Adjust(Mathf.RoundToInt(userPlayer.Tactic * Def.COMBAT_RIVAL_AI_STAT_MAX_RATIO));
                    break;

                case Combat.AIPlayer.EOrient.Trick:
                    mp = getRivalStat(userPlayer.Mp, Def.COMBAT_RIVAL_AI_STAT_MIN_RATIO, Def.COMBAT_RIVAL_AI_STAT_MAX_RATIO);
                    attack = Def.MIN_STAT;
                    defense = Nurture.Character.Adjust(Mathf.RoundToInt(userPlayer.Defense * Def.COMBAT_RIVAL_AI_STAT_MAX_RATIO));
                    intellect = Nurture.Character.Adjust(Mathf.RoundToInt(userPlayer.Intellect * Def.COMBAT_RIVAL_AI_STAT_MAX_RATIO));
                    tactic = getRivalStat(userPlayer.Tactic, Def.COMBAT_RIVAL_AI_STAT_MIN_RATIO, Def.COMBAT_RIVAL_AI_STAT_MAX_RATIO);
                    break;

                case Combat.AIPlayer.EOrient.All:
                    mp = getRivalStat(userPlayer.Mp, Def.COMBAT_RIVAL_AI_STAT_MIN_RATIO, Def.COMBAT_RIVAL_AI_STAT_MAX_RATIO);
                    attack = getRivalStat(userPlayer.AttackPower, Def.COMBAT_RIVAL_AI_STAT_MIN_RATIO, Def.COMBAT_RIVAL_AI_STAT_MAX_RATIO);
                    defense = getRivalStat(userPlayer.Defense, Def.COMBAT_RIVAL_AI_STAT_MIN_RATIO, Def.COMBAT_RIVAL_AI_STAT_MAX_RATIO);
                    intellect = getRivalStat(userPlayer.Intellect, Def.COMBAT_RIVAL_AI_STAT_MIN_RATIO, Def.COMBAT_RIVAL_AI_STAT_MAX_RATIO);
                    tactic = getRivalStat(userPlayer.Tactic, Def.COMBAT_RIVAL_AI_STAT_MIN_RATIO, Def.COMBAT_RIVAL_AI_STAT_MAX_RATIO);
                    break;

                default:
                    Log.Error("invalid combat.ai.player orient");
                    break;
            }

            return new Combat.AIPlayer(Manager.Instance.Object.CombatMode, combatPlayerId, 
                                    hp, mp, criticalProbability, attack, defense, intellect, tactic);
        }

        private int getRivalStat(int baseStat, float minRaio, float maxRatio)
        {
            int min = Mathf.RoundToInt(baseStat * minRaio);
            min = Nurture.Character.Adjust(min);

            int max = Mathf.RoundToInt(baseStat * maxRatio);
            max = Nurture.Character.Adjust(max);

            return Random.Range(min, (max + 1));
        }

    }   // class

}   // namespace

