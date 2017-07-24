using UnityEngine;


namespace Story
{
    public class Mode
    {
        private readonly Newtonsoft.Json.JsonSerializerSettings JSON_SETTINGS = null;

        // @legacy .begin
        private TargetCharacter[] _targetCharacterArray = null;
        public TargetCharacter[] TargetCharacterArray { get { return _targetCharacterArray; } }
        // @legacy .end

        private TargetCharacter _targetCharacter = null;
        public TargetCharacter TargetCharacter { get { return _targetCharacter; } }

        private int _caseKey = -1;
        public int CaseKey
        {
            get { return _caseKey; }
            set { _caseKey = value; }
        }

        private int _targetId = -1; // @legacy
        private Scenario _scenario = null;

        private Nurture.Character _mainCharacter = null;


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
        public Mode(Nurture.Character mainCharacter, int targetId)
        {
            _mainCharacter = mainCharacter;

            // json settings
            JSON_SETTINGS = new Newtonsoft.Json.JsonSerializerSettings();
            JSON_SETTINGS.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto;

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

            // test : target
            _targetCharacter = new TargetCharacter(targetId, CmdFeelingEvent);
        }


        // legacy method
        public bool LoadScenario(int targetId)
        {
            TargetCharacter tc = TargetCharacterArray[targetId];
            
            bool isLoaded = LoadScenario(tc.NextScenarioPath);
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
            if (null == path)
                return false;

            TextAsset textAsset = getTextAsset(path);
            return LoadScenario(textAsset);
        }

        public bool LoadScenario(TextAsset textAsset)
        {
            if (null == textAsset)
            {
                Log.Error("not found text asset");
                return false;
            }
            
            object result = null;
            bool isDeserialized = JsonUtils.Deserialize<Sugarism.Scenario>(textAsset.text, 
                                out result, JSON_SETTINGS);
            if (false == isDeserialized)
            {
                string msg = string.Format("Failed to Load Scenario from the text asset: {0}", textAsset.name);
                Log.Error(msg);
                return false;
            }

            Sugarism.Scenario scenarioModel = result as Sugarism.Scenario;
            start(scenarioModel);

            return true;
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

        private void start(Sugarism.Scenario model)
        {
            _scenario = new Scenario(model, this);

            ScenarioStartEvent.Invoke();
            NextCmd();
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
                    c.NextScenarioNo();

                    _targetId = -1;
                }

                Log.Debug("end. scenario");
            }
        }

        //
        private TextAsset getTextAsset(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                Log.Error("not fount path");
                return null;
            }

            TextAsset textAsset = Resources.Load<TextAsset>(path);
            return textAsset;
        }

        //
        public string GetEndingScenarioPath()
        {
            int endingMinFeeling = Mathf.RoundToInt(Def.MAX_FEELING * Def.ENDING_MIN_FEELING_PERCENT * 0.01f);
            if (_targetCharacter.Feeling < endingMinFeeling)
            {
                Log.Debug(string.Format("lack feeling; min({0}), current({1})", endingMinFeeling, _targetCharacter.Feeling));
                return null;
            }

            if (isHappyEnding())
            {
                Log.Debug("story; happy ending");
                return getEndingScenarioPath(RsrcLoader.TARGET_HAPPY_ENDING_FILENAME);
            }
            else
            {
                Log.Debug("story; normal ending");
                return getEndingScenarioPath(RsrcLoader.TARGET_NORMAL_ENDING_FILENAME);
            }
        }

        private string getEndingScenarioPath(string filename)
        {
            if (null == filename)
                return null;

            if (false == ExtTarget.isValid(_targetCharacter.Id))
            {
                Log.Error(string.Format("invalid target id; {0}", _targetCharacter.Id));
                return null;
            }

            Target t = Manager.Instance.DTTarget[_targetCharacter.Id];

            string path = string.Format("{0}{1}{2}{3}{4}",
                            RsrcLoader.SCENARIO_FOLDER_PATH,
                            RsrcLoader.DIR_SEPARATOR, t.scenarioDirName,
                            RsrcLoader.DIR_SEPARATOR, filename);

            return path;
        }

        private bool isHappyEnding()
        {
            if (false == ExtTarget.isValid(_targetCharacter.Id))
            {
                Log.Error(string.Format("invalid target id; {0}", _targetCharacter.Id));
                return false;
            }

            Target t = Manager.Instance.DTTarget[_targetCharacter.Id];
            
            int fighterMin = Mathf.RoundToInt(Def.MAX_STAT * t.happyEndingFighterAvgMinPercent * 0.01f);
            int trickerMin = Mathf.RoundToInt(Def.MAX_STAT * t.happyEndingTrickerAvgMinPercent * 0.01f);
            int politicianMin = Mathf.RoundToInt(Def.MAX_STAT * t.happyEndingPoliticianAvgMinPercent * 0.01f);
            int attracterMin = Mathf.RoundToInt(Def.MAX_STAT * t.happyEndingAttracterAvgMinPercent * 0.01f);
            
            int fighterAvg = _mainCharacter.GetAverage(EStatLine.FIGHTER);
            int trickerAvg = _mainCharacter.GetAverage(EStatLine.TRICKER);
            int politicianAvg = _mainCharacter.GetAverage(EStatLine.POLITICIAN);
            int attracterAvg = _mainCharacter.GetAverage(EStatLine.ATTACTER);

            if ((fighterAvg >= fighterMin) && (trickerAvg >= trickerMin)
                && (politicianAvg >= politicianMin) && (attracterAvg >= attracterMin))
                return true;
            else
                return false;
        }

    }   // class

}   // namespace