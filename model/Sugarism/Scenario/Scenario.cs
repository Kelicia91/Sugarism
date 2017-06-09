using System.Collections.Generic;


namespace Sugarism
{
    /// <summary>
    /// set of scene.
    /// </summary>
    public class Scenario : Base.Model
    {
        // const
        public const int MIN_COUNT_SCENE = 1;


        // property
        private List<Scene> _sceneList;
        public List<Scene> SceneList
        {
            get { return _sceneList; }
            set { _sceneList = value; OnPropertyChanged("SceneList"); }
        }


        // default constructor for JSON Deserializer
        public Scenario()
        {
            _sceneList = null;
        }

        /// <summary>
        /// Create Scenario instance.
        /// SceneList is initiated and has minimum number of scenes.
        /// </summary>
        /// <param name="sceneList">Scene List. If it is null, create a scene list that has minimum num of scene.</param>
        public Scenario(List<Scene> sceneList)
        {
            if (null != sceneList)
            {
                _sceneList = sceneList;
                return;
            }

            _sceneList = new List<Scene>();

            for(int i = 0; i < MIN_COUNT_SCENE; ++i)
            {
                Scene scene = new Scene(string.Empty);
                _sceneList.Add(scene);
            }
        }


        // method : validation
        public enum ValidationResult
        {
            Success,

            SceneListIsNull,
            UnderMinCountScene
        }

        public static ValidationResult IsValid(List<Scene> sceneList)
        {
            ValidationResult result;

            result = IsValidSceneCount(sceneList);
            if (ValidationResult.Success != result)
                return result;

            return ValidationResult.Success;
        }

        public static ValidationResult IsValidSceneCount(List<Scene> sceneList)
        {
            if (null == sceneList)
                return ValidationResult.SceneListIsNull;

            if (sceneList.Count < MIN_COUNT_SCENE)
                return ValidationResult.UnderMinCountScene;

            return ValidationResult.Success;
        }
    }
}
