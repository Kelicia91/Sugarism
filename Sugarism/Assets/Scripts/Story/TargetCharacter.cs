﻿using System;


namespace Story
{
    public class TargetCharacter
    {
        private int _id = -1;
        public int Id { get { return _id; } }

        private int _feeling = -1;
        public int Feeling
        {
            get { return _feeling; }
            set
            {
                _feeling = value;

                if (_feeling < Def.MIN_FEELING)
                    _feeling = Def.MIN_FEELING;
                else if (_feeling > Def.MAX_FEELING)
                    _feeling = Def.MAX_FEELING;

                FeelingChangeEvent.Invoke(_feeling);
            }
        }

        private int _lastOpenedScenarioNo = -1;
        public int LastOpenedScenarioNo
        {
            get { return _lastOpenedScenarioNo; }
            private set
            {
                _lastOpenedScenarioNo = value;

                if (_lastOpenedScenarioNo < Def.MIN_SCENARIO)
                    _lastOpenedScenarioNo = Def.MIN_SCENARIO;
                else if (_lastOpenedScenarioNo > Def.MAX_SCENARIO)
                    _lastOpenedScenarioNo = Def.MAX_SCENARIO;

                LastOpenedScenarioNoChangeEvent.Invoke(_lastOpenedScenarioNo);
            }
        }

        // End with directory separator
        public readonly string ScenarioDirPath = null;

        public string NextScenarioPath
        {
            get
            {
                string filename = (LastOpenedScenarioNo + 1).ToString();
                string path = string.Format("{0}{1}", ScenarioDirPath, filename);
                return path;
            }
        }


        #region Events

        private FeelingChangeEvent _feelingChangeEvent = null;
        public FeelingChangeEvent FeelingChangeEvent { get { return _feelingChangeEvent; } }

        private LastOpenedScenarioNoChangeEvent _lastOpenedScenarioNoChangeEvent = null;
        public LastOpenedScenarioNoChangeEvent LastOpenedScenarioNoChangeEvent { get { return _lastOpenedScenarioNoChangeEvent; } }

        #endregion


        // constructor
        public TargetCharacter(int id, int feeling, int lastOpenedScenarioNo)
        {
            _id = id;
            _feeling = feeling;
            _lastOpenedScenarioNo = lastOpenedScenarioNo;

            Target target = Manager.Instance.DT.Target[_id];
            ScenarioDirPath = string.Format("{0}{1}{2}{3}", 
                            RsrcLoader.SCENARIO_FOLDER_PATH, RsrcLoader.DIR_SEPARATOR, 
                            target.scenarioDirName, RsrcLoader.DIR_SEPARATOR);
            
            // events
            _feelingChangeEvent = new FeelingChangeEvent();
            _lastOpenedScenarioNoChangeEvent = new LastOpenedScenarioNoChangeEvent();
        }

        public void AttachTo(CmdFeelingEvent cmdFeelingEvent)
        {
            cmdFeelingEvent.Attach(onCmdFeeling);
        }

        public void NextScenarioNo()
        {
            ++LastOpenedScenarioNo;
            Log.Debug(string.Format("TargetCharacter; LastOpenedScenarioNo({0})", LastOpenedScenarioNo));
        }


        private void onCmdFeeling(int targetId, Sugarism.EOperation op, int value)
        {
            if (targetId != Id)
                return;

            operateFeeling(op, value);
        }

        private void operateFeeling(Sugarism.EOperation op, int value)
        {
            switch (op)
            {
                case Sugarism.EOperation.Add:
                    Feeling += value;
                    break;

                case Sugarism.EOperation.Subtract:
                    Feeling -= value;
                    break;

                case Sugarism.EOperation.Assign:
                    Feeling = value;
                    break;

                default:
                    break;
            }
        }

    }   // class

}   // namespace