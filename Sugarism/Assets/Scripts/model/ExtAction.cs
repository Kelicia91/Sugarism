using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Extended Action = Action Controller
public abstract class ExtAction
{
    private int _id = -1;
    public int Id { get { return _id; } }
    
    protected readonly Action _Action;
    protected readonly MainCharacter _MainCharacter;

    protected ExtAction(int id, MainCharacter mainCharacter)
    {
        _id = id;
        _Action = Manager.Instance.DTAction[_id];

        _MainCharacter = mainCharacter;
    }

    public void Begin()
    {
        Manager.Instance.ScheduleBeginEvent.Invoke(_id);
    }

    public void First()
    {
        first();
    }

    protected virtual void first()
    {
        Manager.Instance.Object.Schedule.Do();
    }

    public void Do()
    {
        updateStat();
        _MainCharacter.Increment(Id);

        bool isSuccessed = doing();
        Manager.Instance.ScheduleDoEvent.Invoke(isSuccessed);
    }

    protected abstract bool doing();

    protected bool isSuccess()
    {
        int min = 0;    // inclusive
        int max = 100;  // exclusive

        int randomValue = UnityEngine.Random.Range(min, max);

        string msg = string.Format("Random Value : {0}", randomValue);
        Log.Debug(msg);

        int successProbability = getSuccessProbability();

        if (randomValue < successProbability)
            return true;
        else
            return false;
    }

    private int getSuccessProbability()
    {
        // 성공율 = 각 element 합 * 100
        //
        // 1 element = 가중치1 * (criticalStat.current / criticalStat.base)
        //                       (단, current >= base 이면 1)
        // 2 element = 가중치2 * (1 - stress / stamina)
        //                      (단, stress >= stamina 이면 1)
        //
        // 조건. SUM(각 element 에 부여된 가중치) = 1
        // 조건. stat (min,max) = (0, 999)

        const int NUM_OF_ELEMENT = 2;
        float[] elemArray = new float[NUM_OF_ELEMENT];

        float quotient = 0;

        // set element[0]
        int currentVal = _MainCharacter.Get(_Action.criticalStat);
        int baseVal = _Action.criticalStatBaseValue;
        if (currentVal >= baseVal)
        {
            elemArray[0] = Def.CRITICAL_WEIGHT; // * 1
        }
        else
        {
            quotient = ((float)currentVal) / baseVal;  // float=(float/int)
            elemArray[0] = Def.CRITICAL_WEIGHT * quotient;
        }

        // set element[1]
        const float REMAIN_WEIGHT = 1.0f - Def.CRITICAL_WEIGHT;
        if (_MainCharacter.Stress >= _MainCharacter.Stamina)
        {
            elemArray[1] = 0.0f;
        }
        else
        {
            quotient = ((float)_MainCharacter.Stress) / _MainCharacter.Stamina;
            elemArray[1] = REMAIN_WEIGHT * (1.0f - quotient);
        }

        //
        float sum = 0;
        for (int i = 0; i < NUM_OF_ELEMENT; ++i)
            sum += elemArray[i];
        
        //
        int successProbability = Mathf.RoundToInt(sum * 100);

        string msg = string.Format("Success Probability : {0}", successProbability);
        Log.Debug(msg);

        return successProbability;
    }
    
    private void updateStat()
    {
        _MainCharacter.Stress += _Action.stress;

        _MainCharacter.Stamina += _Action.stamina;
        _MainCharacter.Intellect += _Action.intellect;
        _MainCharacter.Grace += _Action.grace;
        _MainCharacter.Charm += _Action.charm;

        _MainCharacter.Attack += _Action.attack;
        _MainCharacter.Defence += _Action.defense;

        _MainCharacter.Leadership += _Action.leadership;
        _MainCharacter.Tactic += _Action.tactic;

        _MainCharacter.Morality += _Action.morality;
        _MainCharacter.Goodness += _Action.goodness;

        _MainCharacter.Sensibility += _Action.sensibility;
        _MainCharacter.Arts += _Action.arts;
    }

    public void Done()
    {
        done();
    }

    protected virtual void done()
    {
        int achievementRatio = 0;
        int npcId = _Action.npcId;
        string msg = null;

        Manager.Instance.ScheduleFinishEvent.Invoke(achievementRatio, npcId, msg);
    }
}


public class PartTimeAction : ExtAction
{
    private int _successCount = 0;
    private int _actionPeriod = 0;

    public PartTimeAction(int id, MainCharacter mainCharacter) : base(id, mainCharacter)
    {

    }

    protected override void first()
    {
        Manager.Instance.ScheduleFirstEvent.Invoke(_Action.npcId);
    }

    protected override bool doing()
    {
        bool isSuccessed = isSuccess();
        if (isSuccessed)
        {
            ++_successCount;
            _MainCharacter.Money += _Action.money;   
        }

        ++_actionPeriod;
        return isSuccessed;
    }

    protected override void done()
    {
        float quotient = ((float)_successCount) / _actionPeriod;

        int achievementRatio = Mathf.RoundToInt(quotient * 100);

        string s = string.Format(Def.ACHIVEMENT_RATIO_FORMAT, achievementRatio);
        Log.Debug(s);

        string msg = null;
        if (achievementRatio <= 0)
        {
            _MainCharacter.Stress += _Action.failStress;
            msg = string.Format(Def.STRESS_FORMAT, _Action.failStress);
        }
        else if (achievementRatio < 100)
        {
            msg = string.Empty;
        }
        else
        {
            _MainCharacter.Money += _Action.bonus;
            msg = string.Format(Def.MONEY_FORMAT, _Action.bonus);
        }

        Manager.Instance.ScheduleFinishEvent.Invoke(achievementRatio, _Action.npcId, msg);
    }
}

public class LessonAction : ExtAction
{
    private int _successCount = 0;
    private int _actionPeriod = 0;

    public LessonAction(int id, MainCharacter mainCharacter) : base(id, mainCharacter)
    {

    }

    protected override void first()
    {
        Manager.Instance.ScheduleFirstEvent.Invoke(_Action.npcId);
    }

    protected override bool doing()
    {
        _MainCharacter.Money += _Action.money;

        bool isSuccessed = isSuccess();
        if (isSuccessed)
        {
            ++_successCount;
        }

        ++_actionPeriod;
        return isSuccessed;
    }

    protected override void done()
    {
        float quotient = ((float)_successCount) / _actionPeriod;

        int achievementRatio = Mathf.RoundToInt(quotient * 100);

        string s = string.Format(Def.ACHIVEMENT_RATIO_FORMAT, achievementRatio);
        Log.Debug(s);

        string msg = null;
        if (achievementRatio <= 0)
        {
            _MainCharacter.Stress += _Action.failStress;
            msg = string.Format(Def.STRESS_FORMAT, _Action.failStress);
        }
        else if (achievementRatio < 100)
        {
            msg = string.Empty;
        }
        else
        {
            _MainCharacter.Stress += _Action.bonus;
            msg = string.Format(Def.STRESS_FORMAT, _Action.bonus);
        }

        Manager.Instance.ScheduleFinishEvent.Invoke(achievementRatio, _Action.npcId, msg);
    }
}

public class RelaxAction : ExtAction
{
    public RelaxAction(int id, MainCharacter mainCharacter) : base(id, mainCharacter)
    {
    }

    protected override bool doing()
    {
        _MainCharacter.Money += _Action.money;

        return true;
    }
}

public class VacationAction : ExtAction
{
    ESeason _season = ESeason.MAX;
    Vacation _vacation;

    public VacationAction(int id, MainCharacter mainCharacter) : base(id, mainCharacter)
    {
        _season = Manager.Instance.Object.Calendar.Get();
        if (ESeason.MAX == _season)
        {
            Log.Error("invalid season");
            return;
        }

        int seasonId = (int)_season;
        _vacation = Manager.Instance.DTVacation[seasonId];
    }

    protected override bool doing()
    {
        updateStat(_vacation);
        _MainCharacter.Money += _Action.money;

        return true;
    }

    private void updateStat(Vacation vacation)
    {
        _MainCharacter.Stamina += vacation.stamina;
        _MainCharacter.Intellect += vacation.intellect;
        _MainCharacter.Grace += vacation.grace;
        _MainCharacter.Charm += vacation.charm;

        _MainCharacter.Attack += vacation.attack;
        _MainCharacter.Defence += vacation.defense;

        _MainCharacter.Leadership += vacation.leadership;
        _MainCharacter.Tactic += vacation.tactic;

        _MainCharacter.Morality += vacation.morality;
        _MainCharacter.Goodness += vacation.goodness;

        _MainCharacter.Sensibility += vacation.sensibility;
        _MainCharacter.Arts += vacation.arts;
    }
}

// 돈을 소비하는 action을 스케줄에 넣었는데 은전이 부족한 경우 강제로 셋팅.
public class IdleAction : ExtAction
{
    public IdleAction(int id, MainCharacter mainCharacter) : base(id, mainCharacter)
    {
        // do nothing
    }

    protected override bool doing()
    {
        return true;
    }
}
