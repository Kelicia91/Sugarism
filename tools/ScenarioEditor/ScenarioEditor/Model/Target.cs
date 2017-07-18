
namespace ScenarioEditor.Model
{
    // for referencing Target DataTable(.xml)
    public class Target : ModelBase
    {
        // const
        public const int START_ID = 0;

        public const string XML_COLUMN_NAME = "characterId";
        public const string XML_TARGET_NAME = "Target";


        private int _id = -1;
        public int Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(); }
        }

        private int _characterId = -1;
        public int CharacterId
        {
            get { return _characterId; }
            set { _characterId = value; OnPropertyChanged(); }
        }


        public Target() : this(-1, -1) { }

        public Target(int id, int characterId)
        {
            _id = id;
            _characterId = characterId;
        }
    }
}
