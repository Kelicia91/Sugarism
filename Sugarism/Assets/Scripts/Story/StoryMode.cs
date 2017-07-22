using UnityEngine;


namespace Story
{
    public class Mode
    {
        private TargetCharacter[] _targetCharacterArray = null;
        public TargetCharacter[] TargetCharacterArray { get { return _targetCharacterArray; } }

        private int _caseKey = -1;
        public int CaseKey
        {
            get { return _caseKey; }
            set { _caseKey = value; }
        }

        private int _targetId = -1;
        private Scenario _scenario = null;


        #region Events

        private CmdLinesEvent _cmdLinesEvent = null;
        public CmdLinesEvent CmdLinesEvent { get { return _cmdLinesEvent; } }

        private CmdTextEvent _cmdTextEvent = null;
        public CmdTextEvent CmdTextEvent { get { return _cmdTextEvent; } }

        private CmdAppearEvent _cmdAppearEvent = null;
        public CmdAppearEvent CmdAppearEvent { get { return _cmdAppearEvent; } }

        private CmdBackgroundEvent _cmdBackgroundEvent = null;
        public CmdBackgroundEvent CmdBackgroundEvent { get { return _cmdBackgroundEvent; } }

        private CmdMiniPictureEvent _cmdMiniPictureEvent = null;
        public CmdMiniPictureEvent CmdMiniPictureEvent { get { return _cmdMiniPictureEvent; } }

        private CmdPictureEvent _cmdPictureEvent = null;
        public CmdPictureEvent CmdPictureEvent { get { return _cmdPictureEvent; } }

        private CmdFilterEvent _cmdFilterEvent = null;
        public CmdFilterEvent CmdFilterEvent { get { return _cmdFilterEvent; } }

        private CmdSEEvent _cmdSEEvent = null;
        public CmdSEEvent CmdSEEvent { get { return _cmdSEEvent; } }

        private CmdFeelingEvent _cmdFeelingEvent = null;
        public CmdFeelingEvent CmdFeelingEvent { get { return _cmdFeelingEvent; } }

        private CmdSwitchEvent _cmdSwitchEvent = null;
        public CmdSwitchEvent CmdSwitchEvent { get { return _cmdSwitchEvent; } }

        private CmdTargetAppearEvent _cmdTargetAppearEvent = null;
        public CmdTargetAppearEvent CmdTargetAppearEvent { get { return _cmdTargetAppearEvent; } }

        private CmdDisappearEvent _cmdDisappearEvent = null;
        public CmdDisappearEvent CmdDisappearEvent { get { return _cmdDisappearEvent; } }

        private ScenarioStartEvent _scenarioStartEvent = null;
        public ScenarioStartEvent ScenarioStartEvent { get { return _scenarioStartEvent; } }

        private ScenarioEndEvent _scenarioEndEvent = null;
        public ScenarioEndEvent ScenarioEndEvent { get { return _scenarioEndEvent; } }

        #endregion


        // constructor
        public Mode()
        {
            // events
            _cmdLinesEvent = new CmdLinesEvent();
            _cmdTextEvent = new CmdTextEvent();
            _cmdAppearEvent = new CmdAppearEvent();
            _cmdBackgroundEvent = new CmdBackgroundEvent();
            _cmdMiniPictureEvent = new CmdMiniPictureEvent();
            _cmdPictureEvent = new CmdPictureEvent();
            _cmdFilterEvent = new CmdFilterEvent();
            _cmdSEEvent = new CmdSEEvent();
            _cmdFeelingEvent = new CmdFeelingEvent();
            _cmdSwitchEvent = new CmdSwitchEvent();
            _cmdTargetAppearEvent = new CmdTargetAppearEvent();
            _cmdDisappearEvent = new CmdDisappearEvent();

            _scenarioStartEvent = new ScenarioStartEvent();
            _scenarioEndEvent = new ScenarioEndEvent();

            // target
            _targetCharacterArray = new TargetCharacter[Manager.Instance.DTTarget.Count];

            int numTargetCharacterArray = _targetCharacterArray.Length;
            for (int i = 0; i < numTargetCharacterArray; ++i)
            {
                _targetCharacterArray[i] = new TargetCharacter(i, CmdFeelingEvent);
            }
        }


        public bool LoadScenario(int targetId)
        {
            if (false == ExtTarget.isValid(targetId))
            {
                Log.Error(string.Format("invalid target id({0})", targetId));
                return false;
            }

            string dirPath = string.Format("{0}{1}{2}", Configuration.SCENARIO_FOLDER_PATH,
                            RsrcLoader.DIR_SEPARATOR, Manager.Instance.DTTarget[targetId].scenarioDirName);

            TargetCharacter tc = TargetCharacterArray[targetId];
            string filename = (tc.LastOpenedScenarioNo + 1).ToString(); // without extension

            string fullPath = string.Format("{0}{1}{2}", dirPath,
                            RsrcLoader.DIR_SEPARATOR, filename);

            
            bool isLoaded = LoadScenario(fullPath);
            if (isLoaded)
            {
                _targetId = targetId;
            }
            else
            {
                _targetId = -1;
            }

            return isLoaded;
        }

        public bool LoadScenario(string path)
        {
            Sugarism.Scenario scenarioModel = null;
            bool isLoaded = RsrcLoader.Instance.Load(path, out scenarioModel);
            if (isLoaded)
            {
                _scenario = new Scenario(scenarioModel, this);
            }
            else
            {
                _scenario = null;
            }

            return isLoaded;
        }

        public bool LoadScenario(TextAsset textAsset)
        {
            Sugarism.Scenario scenarioModel = null;
            bool isLoaded = RsrcLoader.Instance.Load(textAsset, out scenarioModel);
            if (isLoaded)
            {
                _scenario = new Scenario(scenarioModel, this);
            }
            else
            {
                _scenario = null;
            }

            return isLoaded;
        }


        // temp : test (data <-> ui)
        public void NextCmd()
        {
            if (_isEndedScenario)
            {
                _isEndedScenario = false;
                _scenario = null;

                ScenarioEndEvent.Invoke();
                return;
            }

            if (null == _scenario)
            {
                Log.Error("not found scenario, load please");
                return;
            }

            play();
        }


        private bool _isEndedScenario = false;
        private void play()
        {
            bool canMorePlay = _scenario.Play();
            if (false == canMorePlay)
            {
                _isEndedScenario = true;

                if (ExtTarget.isValid(_targetId))
                {
                    TargetCharacter c = _targetCharacterArray[_targetId];
                    ++c.LastOpenedScenarioNo;

                    _targetId = -1;
                }

                Log.Debug("end. scenario");
            }
        }

    }   // class

}   // namespace