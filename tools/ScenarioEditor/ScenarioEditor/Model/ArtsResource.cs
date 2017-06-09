
namespace ScenarioEditor.Model
{
    // for referencing Art(image, sound, ..) DataTable(.xml)
    public class ArtsResource : ModelBase
    {
        public const string XML_COLUMN_NAME = "description";

        public const string XML_BACKGROUND_NAME = "Background";
        public const string XML_MINIPICTURE_NAME = "MiniPicture";
        public const string XML_PICTURE_NAME = "Picture";
        public const string XML_SE_NAME = "SE";


        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(); }
        }


        public ArtsResource() : this(-1, string.Empty) { }

        public ArtsResource(int id, string description)
        {
            _id = id;
            _description = description;
        }
    }
}
