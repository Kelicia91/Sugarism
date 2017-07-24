using System.Collections;


namespace Nurture
{
    public partial class Schedule
    {
        private readonly Mode _mode = null;

        private readonly int _MAX_NUM_OF_ACTION = 0;

        private int[] _actionArray = null;
        private IdleAction _idleAction = null;

        #region Events

        private ScheduleInsertEvent _insertEvent = null;
        public ScheduleInsertEvent InsertEvent { get { return _insertEvent; } }

        private ScheduleStartEvent _startEvent = null;
        public ScheduleStartEvent StartEvent { get { return _startEvent; } }

        private ActionCancelEvent _actionCancelEvent = null;
        public ActionCancelEvent ActionCancelEvent { get { return _actionCancelEvent; } }

        private ActionStartEvent _actionStartEvent = null;
        public ActionStartEvent ActionStartEvent { get { return _actionStartEvent; } }

        private ActionFirstEvent _actionFirstEvent = null;
        public ActionFirstEvent ActionFirstEvent { get { return _actionFirstEvent; } }

        private ActionBeforeEndEvent _actionBeforeEndEvent = null;
        public ActionBeforeEndEvent ActionBeforeEndEvent { get { return _actionBeforeEndEvent; } }

        private ActionDoEvent _actionDoEvent = null;
        public ActionDoEvent ActionDoEvent { get { return _actionDoEvent; } }

        private ActionEndEvent _actionEndEvent = null;
        public ActionEndEvent ActionEndEvent { get { return _actionEndEvent; } }

        private ScheduleEndEvent _endEvent = null;
        public ScheduleEndEvent EndEvent { get { return _endEvent; } }

        #endregion


        // constructor
        public Schedule(Mode mode, int maxNumOfAction)
        {
            _mode = mode;

            _MAX_NUM_OF_ACTION = maxNumOfAction;

            _actionArray = new int[_MAX_NUM_OF_ACTION];
            _idleAction = new IdleAction(Def.ACTION_IDLE_ID, _mode);
            
            _insertEvent = new ScheduleInsertEvent();
            _startEvent = new ScheduleStartEvent();
            _actionCancelEvent = new ActionCancelEvent();
            _actionStartEvent = new ActionStartEvent();
            _actionFirstEvent = new ActionFirstEvent();
            _actionDoEvent = new ActionDoEvent();
            _actionBeforeEndEvent = new ActionBeforeEndEvent();
            _actionEndEvent = new ActionEndEvent();
            _endEvent = new ScheduleEndEvent();

            initialize();
        }

        private void initialize()
        {
            int actionArrayLength = _actionArray.Length;
            for (int i = 0; i < actionArrayLength; ++i)
            {
                insert(i, -1);
            }

            _iterator = null;
        }

        public int GetActionId(int index)
        {
            if (false == isValid(index))
                return -1;
            else
                return _actionArray[index];
        }

        public bool IsFull()
        {
            int actionArrayLength = _actionArray.Length;
            for (int i = 0; i < actionArrayLength; ++i)
            {
                if (false == ExtAction.isValid(_actionArray[i]))
                    return false;
            }

            return true;
        }

        public void Insert(int index, int actionId)
        {
            if (false == isValid(index))
                return;

            if (false == ExtAction.isValid(actionId))
                return;

            insert(index, actionId);
        }

        private void insert(int index, int actionId)
        {
            _actionArray[index] = actionId;

            InsertEvent.Invoke(index, _actionArray[index]);
        }

        private bool isValid(int index)
        {
            if (index < 0)
                return false;
            else if (index >= _actionArray.Length)
                return false;
            else
                return true;
        }
    }


    public partial class Schedule
    {
        private IEnumerator _iterator = null;

        public void Start()
        {
            _iterator = scheduling();
            StartEvent.Invoke();

            Iterate();
        }

        public void Iterate()
        {
            if (null == _iterator)
            {
                Log.Error("not found schedule iterator");
                return;
            }

            if (_iterator.MoveNext())
                return;

            end();
        }

        private void end()
        {
            _mode.Character.UpdateCondition();

            EndEvent.Invoke();
            initialize();
        }

        private IEnumerator scheduling()
        {
            int MONTH = _mode.Calendar.Month;

            int[] BEGINDAY_ACTION = getBeginDayOfActon(MONTH, _MAX_NUM_OF_ACTION);
            int[] ENDDAY_ACTION = getEndDayOfAction(MONTH, _MAX_NUM_OF_ACTION);

            ActionController[] actionCtrlArray = generate();

            int index = 0;
            int day = _mode.Calendar.Day;

            // LOOP : day by day
            while (day <= Calendar.LastDay[MONTH])
            {
                if (true == isLackMoney(actionCtrlArray[index].Id))
                {
                    ActionCancelEvent.Invoke();
                    yield return null;

                    if (day > BEGINDAY_ACTION[index])
                    {
                        _mode.Character.IncrementActionCount(actionCtrlArray[index].Id);

                        actionCtrlArray[index].End();
                        yield return null;
                    }

                    actionCtrlArray[index] = _idleAction;
                    BEGINDAY_ACTION[index] = day;
                }

                ActionController action = actionCtrlArray[index];

                if (day == BEGINDAY_ACTION[index])
                {
                    action.Start();
                    yield return null;

                    if (_mode.Character.GetActionCount(action.Id) <= 0)
                    {
                        action.First();
                        yield return null;
                    }
                }

                action.Do();
                yield return null;

                if (day == ENDDAY_ACTION[index])
                {
                    _mode.Character.IncrementActionCount(action.Id);

                    action.BeforeEnd();
                    yield return null;

                    action.End();
                    yield return null;
                                        
                    ++index;
                }

                ++day;
                _mode.Calendar.Day = day;

            }   // while
        }

        private bool isLackMoney(int actionId)
        {
            Action action = Manager.Instance.DTAction[actionId];

            // @note : action.money can be < 0.
            int sum = _mode.Character.Money + action.money;
            if (sum < 0)
                return true;
            else
                return false;
        }

        private ActionController[] generate()
        {
            ActionController[] actionCtrlArray = new ActionController[_MAX_NUM_OF_ACTION];

            for (int i = 0; i < _MAX_NUM_OF_ACTION; ++i)
            {
                int actionId = _actionArray[i];
                if (Def.ACTION_VACATION_ID == actionId)
                {
                    actionCtrlArray[i] = new VacationAction(actionId, _mode);
                    continue;
                }

                Action action = Manager.Instance.DTAction[actionId];
                switch (action.type)
                {
                    case EActionType.PARTTIME:
                        actionCtrlArray[i] = new PartTimeAction(actionId, _mode);
                        break;

                    case EActionType.LESSON:
                        actionCtrlArray[i] = new LessonAction(actionId, _mode);
                        break;

                    case EActionType.RELAX:
                        actionCtrlArray[i] = new RelaxAction(actionId, _mode);
                        break;

                    default:
                        Log.Error("invalid action type");
                        actionCtrlArray[i] = null;
                        break;
                }
            }

            return actionCtrlArray;
        }

        private int[] getBeginDayOfActon(int month, int numOfAction)
        {
            int actionPeriod = Calendar.LastDay[month] / numOfAction;

            int[] beginDayOfAction = new int[numOfAction];

            int numBeginDayOfAction = beginDayOfAction.Length;
            for (int i = 0; i < numBeginDayOfAction; ++i)
            {
                beginDayOfAction[i] = actionPeriod * i + 1;
            }

            return beginDayOfAction;
        }

        private int[] getEndDayOfAction(int month, int numOfAction)
        {
            int actionPeriod = Calendar.LastDay[month] / numOfAction;

            int[] endDayOfAction = new int[numOfAction];

            int numEndDayOfAction = endDayOfAction.Length;
            for (int i = 0; i < numEndDayOfAction; ++i)
            {
                endDayOfAction[i] = actionPeriod * (i + 1);
            }

            endDayOfAction[(numOfAction - 1)] = Calendar.LastDay[month];

            return endDayOfAction;
        }
    }   // class

}   // namespace