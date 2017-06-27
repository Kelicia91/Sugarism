using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nurture
{
    public abstract class ActionController
    {
        private int _id = -1;
        public int Id { get { return _id; } }
        
        protected readonly Action _action;

        protected readonly Mode _mode = null;


        // constructor
        protected ActionController(int id, Mode mode)
        {
            _id = id;
            _action = Manager.Instance.DTAction[_id];

            _mode = mode;
        }

        // START
        public void Start()
        {
            start();
        }

        protected virtual void start()
        {
            _mode.Schedule.ActionStartEvent.Invoke(Id);
        }

        // FIRST
        public void First()
        {
            first();
        }

        protected virtual void first()
        {
            _mode.Schedule.ActionFirstEvent.Invoke();
        }

        // DO
        public void Do()
        {
            doing();

            updateStats();
        }

        protected virtual void doing()
        {
            _mode.Schedule.ActionDoEvent.Invoke();
        }

        private void updateStats()
        {
            Character c = _mode.Character;

            c.Stress += _action.stress;

            c.Stamina += _action.stamina;
            c.Intellect += _action.intellect;
            c.Grace += _action.grace;
            c.Charm += _action.charm;

            c.Attack += _action.attack;
            c.Defense += _action.defense;

            c.Leadership += _action.leadership;
            c.Tactic += _action.tactic;

            c.Morality += _action.morality;
            c.Goodness += _action.goodness;

            c.Sensibility += _action.sensibility;
            c.Arts += _action.arts;
        }

        // BEFORE END
        public void BeforeEnd()
        {
            beforeEnd();
        }

        protected virtual void beforeEnd()
        {
            _mode.Schedule.ActionBeforeEndEvent.Invoke();
        }
    
        // END
        public void End()
        {
            end();
        }

        protected virtual void end()
        {
            _mode.Schedule.ActionEndEvent.Invoke();
        }

    }   // class

}   // namespace
