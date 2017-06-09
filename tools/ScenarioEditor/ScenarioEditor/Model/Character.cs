using System;
using System.Collections.Generic;
using System.Linq;

namespace ScenarioEditor.Model
{
    // @note: maybe don't need to use Sugarism.Character...

    // for referencing Character DataTable(.xml)
    public class Character : ModelBase
    {
        // const
        public const int START_ID = 0;
        public const string STR_UNKNOWN = "unknown";

        public const string XML_COLUMN_NAME = "name";
        public const string XML_CHARACTER_NAME = "Character";


        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }


        public Character() : this(-1, string.Empty) { }

        public Character(int id, string name)
        {
            _id = id;
            _name = name;
        }
    }
}
