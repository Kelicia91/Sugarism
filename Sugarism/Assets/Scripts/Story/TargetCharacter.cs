
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
            }
        }

        private int _lastOpenedScenarioNo = -1;
        public int LastOpenedScenarioNo
        {
            get { return _lastOpenedScenarioNo; }
            set
            {
                _lastOpenedScenarioNo = value;

                if (_lastOpenedScenarioNo < Def.MIN_SCENARIO)
                    _lastOpenedScenarioNo = Def.MIN_SCENARIO;
                else if (_lastOpenedScenarioNo > Def.MAX_SCENARIO)
                    _lastOpenedScenarioNo = Def.MAX_SCENARIO;
            }
        }

        // constructor
        public TargetCharacter(int id, CmdFeelingEvent cmdFeelingEvent)
        {
            _id = id;
            _feeling = Def.MIN_FEELING;
            _lastOpenedScenarioNo = Def.MIN_SCENARIO - 1;

            cmdFeelingEvent.Attach(onCmdFeeling);
        }


        private void onCmdFeeling(int characterId, Sugarism.EOperation op, int value)
        {
            Target t = Manager.Instance.DTTarget[Id];
            if (characterId != t.characterId)
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