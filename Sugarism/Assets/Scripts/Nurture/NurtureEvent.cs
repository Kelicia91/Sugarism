
namespace Nurture
{ 
    public class YearChangeEvent
    {
        public delegate void Handler(int year);
        private event Handler _event = null;

        public YearChangeEvent()
        {
            _event = new Handler(onYearChanged);
        }

        // default handler
        private void onYearChanged(int year)
        {
            Log.Debug(string.Format("onYearChanged; {0}", year));
        }

        public void Invoke(int year) { _event.Invoke(year); }

        public void Attach(Handler handler)
        {
            if (null == handler)
                return;

            _event += handler;
        }

        public void Detach(Handler handler)
        {
            if (null == handler)
                return;

            _event -= handler;
        }
    }

    public class MonthChangeEvent
    {
        public delegate void Handler(int month);
        private event Handler _event = null;

        public MonthChangeEvent()
        {
            _event = new Handler(onMonthChanged);
        }

        // default handler
        private void onMonthChanged(int month)
        {
            Log.Debug(string.Format("onMonthChanged; {0}", month));
        }

        public void Invoke(int month) { _event.Invoke(month); }

        public void Attach(Handler handler)
        {
            if (null == handler)
                return;

            _event += handler;
        }

        public void Detach(Handler handler)
        {
            if (null == handler)
                return;

            _event -= handler;
        }
    }

    public class DayChangeEvent
    {
        public delegate void Handler(int day);
        private event Handler _event = null;

        public DayChangeEvent()
        {
            _event = new Handler(onDayChanged);
        }

        // default handler
        private void onDayChanged(int day)
        {
            Log.Debug(string.Format("onDayChanged; {0}", day));
        }

        public void Invoke(int day) { _event.Invoke(day); }

        public void Attach(Handler handler)
        {
            if (null == handler)
                return;

            _event += handler;
        }

        public void Detach(Handler handler)
        {
            if (null == handler)
                return;

            _event -= handler;
        }
    }

    public class AgeChangeEvent
    {
        public delegate void Handler(int age);
        private event Handler _event = null;

        public AgeChangeEvent()
        {
            _event = new Handler(onAgeChanged);
        }

        // default handler
        private void onAgeChanged(int age)
        {
            Log.Debug(string.Format("onAgeChanged; {0}", age));
        }

        public void Invoke(int age) { _event.Invoke(age); }

        public void Attach(Handler handler)
        {
            if (null == handler)
                return;

            _event += handler;
        }

        public void Detach(Handler handler)
        {
            if (null == handler)
                return;

            _event -= handler;
        }
    }

    public class ScheduleInsertEvent
    {
        public delegate void Handler(int scheduleIndex, int actionId);
        private event Handler _event = null;

        public ScheduleInsertEvent()
        {
            _event = new Handler(onScheduleInserted);
        }

        // default handler
        private void onScheduleInserted(int scheduleIndex, int actionId)
        {
            Log.Debug(string.Format("onScheduleInserted; scheduleIndex({0}), actionId({1})",
                    scheduleIndex, actionId));
        }

        public void Invoke(int scheduleIndex, int actionId) { _event.Invoke(scheduleIndex, actionId); }

        public void Attach(Handler handler)
        {
            if (null == handler)
                return;

            _event += handler;
        }

        public void Detach(Handler handler)
        {
            if (null == handler)
                return;

            _event -= handler;
        }
    }

    public class ScheduleStartEvent
    {
        public delegate void Handler();
        private event Handler _event = null;

        public ScheduleStartEvent()
        {
            _event = new Handler(onScheduleStart);
        }

        // default handler
        private void onScheduleStart()
        {
            Log.Debug(string.Format("onScheduleStart;"));
        }

        public void Invoke() { _event.Invoke(); }

        public void Attach(Handler handler)
        {
            if (null == handler)
                return;

            _event += handler;
        }

        public void Detach(Handler handler)
        {
            if (null == handler)
                return;

            _event -= handler;
        }
    }

    public class ActionCancelEvent
    {
        public delegate void Handler();
        private event Handler _event = null;

        public ActionCancelEvent()
        {
            _event = new Handler(onActionCancel);
        }

        // default handler
        private void onActionCancel()
        {
            Log.Debug(string.Format("onActionCancel;"));
        }

        public void Invoke() { _event.Invoke(); }

        public void Attach(Handler handler)
        {
            if (null == handler)
                return;

            _event += handler;
        }

        public void Detach(Handler handler)
        {
            if (null == handler)
                return;

            _event -= handler;
        }
    }

    public class ActionStartEvent
    {
        public delegate void Handler(int actionId);
        private event Handler _event = null;

        public ActionStartEvent()
        {
            _event = new Handler(onActionStart);
        }

        // default handler
        private void onActionStart(int actionId)
        {
            Log.Debug(string.Format("onActionStart; actionId({0})", actionId));
        }

        public void Invoke(int actionId) { _event.Invoke(actionId); }

        public void Attach(Handler handler)
        {
            if (null == handler)
                return;

            _event += handler;
        }

        public void Detach(Handler handler)
        {
            if (null == handler)
                return;

            _event -= handler;
        }
    }

    public class ActionFirstEvent
    {
        public delegate void Handler();
        private event Handler _event = null;

        public delegate void NPCHandler(int npcId);
        private event NPCHandler _npcEvent = null;

        public ActionFirstEvent()
        {
            _event = new Handler(onActionFirst);
            _npcEvent = new NPCHandler(onActionFirstNPC);
        }

        // default handler
        private void onActionFirst()
        {
            Log.Debug("onActionFirst;");
        }

        public void Invoke() { _event.Invoke(); }

        public void Attach(Handler handler)
        {
            if (null == handler)
                return;

            _event += handler;
        }

        public void Detach(Handler handler)
        {
            if (null == handler)
                return;

            _event -= handler;
        }

        // default handler
        private void onActionFirstNPC(int npcId)
        {
            Log.Debug(string.Format("onActionFirstNPC; npcId({0})", npcId));
        }

        public void Invoke(int npcId) { _npcEvent.Invoke(npcId); }

        public void Attach(NPCHandler handler)
        {
            if (null == handler)
                return;

            _npcEvent += handler;
        }

        public void Detach(NPCHandler handler)
        {
            if (null == handler)
                return;

            _npcEvent -= handler;
        }
    }

    public class ActionDoEvent
    {
        public delegate void Handler();
        private event Handler _event = null;

        public delegate void ResultHandler(bool isSuccessed);
        private event ResultHandler _resultEvent = null;

        public ActionDoEvent()
        {
            _event = new Handler(onActionDo);
            _resultEvent = new ResultHandler(onActionDoResult);
        }

        // default handler
        private void onActionDo()
        {
            Log.Debug("onActionDo;");
        }

        public void Invoke() { _event.Invoke(); }

        public void Attach(Handler handler)
        {
            if (null == handler)
                return;

            _event += handler;
        }

        public void Detach(Handler handler)
        {
            if (null == handler)
                return;

            _event -= handler;
        }

        // default handler
        private void onActionDoResult(bool isSuccessed)
        {
            Log.Debug(string.Format("onActionDoResult; isSuccessed({0})", isSuccessed));
        }

        public void Invoke(bool isSuccessed) { _resultEvent.Invoke(isSuccessed); }

        public void Attach(ResultHandler handler)
        {
            if (null == handler)
                return;

            _resultEvent += handler;
        }

        public void Detach(ResultHandler handler)
        {
            if (null == handler)
                return;

            _resultEvent -= handler;
        }
    }



    public class ActionBeforeEndEvent
    {
        public delegate void Handler();
        private event Handler _event = null;

        public delegate void ExamHandler(Exam.Exam exam);
        private event ExamHandler _examEvent = null;

        public ActionBeforeEndEvent()
        {
            _event = new Handler(onActionBeforeEnd);
            _examEvent = new ExamHandler(onActionBeforeEndExam);
        }

        // default handler
        private void onActionBeforeEnd()
        {
            Log.Debug(string.Format("onActionBeforeEnd;"));
        }

        public void Invoke() { _event.Invoke(); }

        public void Attach(Handler handler)
        {
            if (null == handler)
                return;

            _event += handler;
        }

        public void Detach(Handler handler)
        {
            if (null == handler)
                return;

            _event -= handler;
        }

        // default handler
        private void onActionBeforeEndExam(Exam.Exam exam)
        {
            Log.Debug(string.Format("onActionBeforeEndExam; {0}", exam.Type));
        }

        public void Invoke(Exam.Exam exam) { _examEvent.Invoke(exam); }

        public void Attach(ExamHandler handler)
        {
            if (null == handler)
                return;

            _examEvent += handler;
        }

        public void Detach(ExamHandler handler)
        {
            if (null == handler)
                return;

            _examEvent -= handler;
        }
    }

    public class ActionEndEvent
    {
        public delegate void Handler();
        private event Handler _event = null;

        public delegate void AchievementHandler(int achievementRatio, int npcId, string msg);
        private event AchievementHandler _achivementEvent = null;

        public ActionEndEvent()
        {
            _event = new Handler(onActionEnd);
            _achivementEvent = new AchievementHandler(onActionEndAchievement);
        }

        // default handler
        private void onActionEnd()
        {
            Log.Debug("onActionEnd;");
        }

        public void Invoke() { _event.Invoke(); }

        public void Attach(Handler handler)
        {
            if (null == handler)
                return;

            _event += handler;
        }

        public void Detach(Handler handler)
        {
            if (null == handler)
                return;

            _event -= handler;
        }

        // default handler
        private void onActionEndAchievement(int achievementRatio, int npcId, string msg)
        {
            Log.Debug(string.Format("onActionEndAchievement;, achiveRatio({0}), npcId({1}), msg({2})",
                    achievementRatio, npcId, msg));
        }

        public void Invoke(int achievementRatio, int npcId, string msg) { _achivementEvent.Invoke(achievementRatio, npcId, msg); }

        public void Attach(AchievementHandler handler)
        {
            if (null == handler)
                return;

            _achivementEvent += handler;
        }

        public void Detach(AchievementHandler handler)
        {
            if (null == handler)
                return;

            _achivementEvent -= handler;
        }
    }

    public class ScheduleEndEvent
    {
        public delegate void Handler();
        private event Handler _event = null;

        public ScheduleEndEvent()
        {
            _event = new Handler(onScheduleEnd);
        }

        // default handler
        private void onScheduleEnd()
        {
            Log.Debug(string.Format("onScheduleEnd;"));
        }

        public void Invoke() { _event.Invoke(); }

        public void Attach(Handler handler)
        {
            if (null == handler)
                return;

            _event += handler;
        }

        public void Detach(Handler handler)
        {
            if (null == handler)
                return;

            _event -= handler;
        }
    }

    public class CharacterStatEvent
    {
        public delegate void Handler(EStat statType, int value);
        private event Handler _event = null;

        public CharacterStatEvent()
        {
            _event = new Handler(onCharacterStatChanged);
        }

        // default handler
        private void onCharacterStatChanged(EStat statType, int value)
        {
            Log.Debug(string.Format("onCharacterStatChanged; {0}: {1}", statType, value));
        }

        public void Invoke(EStat statType, int value) { _event.Invoke(statType, value); }

        public void Attach(Handler handler)
        {
            if (null == handler)
                return;

            _event += handler;
        }

        public void Detach(Handler handler)
        {
            if (null == handler)
                return;

            _event -= handler;
        }
    }

}   // namespace