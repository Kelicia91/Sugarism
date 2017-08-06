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
            set { _characterId = value; OnPropertyChanged(); }
        }

        private List<CmdCase> _caseList = null;
        public List<CmdCase> CaseList
        {
            get { return _caseList; }
            set { _caseList = value; OnPropertyChanged(); }
        }


        // default constructor for JSON Deserializer
        public CmdSwitch() : base(Command.Type.Switch)
        {
            _characterId = -1;
            _caseList = null;
        }

        /// <summary>
        /// Create CmdSwitch instance.
        /// CaseList is initiated and has minumum number of cases.
        /// </summary>
        /// <param name="characterId"></param>
        public CmdSwitch(int characterId) : base(Command.Type.Switch)
        {
            _characterId = characterId;
            _caseList = new List<CmdCase>();
            
            for (int i = 0, key = CmdCase.START_KEY; i < MIN_COUNT_CASE; ++i, ++key)
            {
                CmdCase cmdCase = new CmdCase(key);
                _caseList.Add(cmdCase);
            }
        }


        // method : validation
        public enum ValidationResult
        {
            Success,

            CaseListIsNull,
            UnderMinCountCase,
            OverMaxCountCase
        }

        public static ValidationResult IsValid(List<CmdCase> caseList)
        {
            ValidationResult result;

            result = IsValidCaseCount(caseList);
            if (ValidationResult.Success != result)
                return result;

            return ValidationResult.Success;
        }

        public static ValidationResult IsValidCaseCount(List<CmdCase> caseList)
        {
            if (null == caseList)
                return ValidationResult.CaseListIsNull;

            if (caseList.Count < MIN_COUNT_CASE)
                return ValidationResult.UnderMinCountCase;

            if (caseList.Count > MAX_COUNT_CASE)
                return ValidationResult.OverMaxCountCase;

            return ValidationResult.Success;
        }
    }
}
