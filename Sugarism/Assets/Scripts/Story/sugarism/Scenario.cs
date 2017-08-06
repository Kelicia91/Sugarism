using System.Collections.Generic;


namespace Sugarism
{
    /// <summary>
    /// set of scene.
    /// </summary>
    public class Scenario
    {
        // const
        public const int MIN_COUNT_SCENE = 1;


        // property
        private List<Scene> _sceneList = null;
        public List<Scene> SceneList
        {
            get { return _sceneList; }
            set { _sceneList = value; }
        }


        // default constructor for JSON Deserializer
        public Scenario() { }
    }
}
