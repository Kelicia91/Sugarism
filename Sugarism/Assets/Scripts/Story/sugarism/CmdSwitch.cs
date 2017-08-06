using System.Collections.Generic;

namespace Sugarism
{
    public class CmdSwitch : Command
    {
        // const
        public const int MIN_COUNT_CASE = 2;
        public const int MAX_COUNT_CASE = 3;


        // property
        private int _characterId = -1;
        public int CharacterId
        {
            get { return _characterId; }
            set { _characterId = value; }
        }

        private List<CmdCase> _caseList = null;
        public List<CmdCase> CaseList
        {
            get { return _caseList; }
            set { _caseList = value; }
        }


        // default constructor for JSON Deserializer
        public CmdSwitch() : base(Command.Type.Switch) { }
    }
}
