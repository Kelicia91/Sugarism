using System.Collections.Generic;


namespace Story
{
    interface IPlayable
    {
        // play 하고 이후 play할 것들이 존재하면 true. 그외에는 false.
        bool Play();
    }

    public class Scenario : IPlayable
    {
        private Mode _mode = null;
        private Sugarism.Scenario _model = null;

        private List<Scene> _sceneList = null;
        private IEnumerator<Scene> _sceneIter = null;

        public Scenario(Sugarism.Scenario model, Mode mode)
        {
            if (null == model)
            {
                Log.Error("Not Found Sugarism.Scenario");
                return;
            }

            _model = model;
            _mode = mode;

            Log.Debug("begin. Scenario");

            _sceneList = new List<Scene>();
            foreach (Sugarism.Scene mScene in _model.SceneList)
            {
                Scene scene = new Scene(mScene, _mode);
                _sceneList.Add(scene);
            }

            _sceneIter = _sceneList.GetEnumerator();
            if (_sceneIter.MoveNext())
            {
                string msg = string.Format("begin. Scene: {0}", _sceneIter.Current.Description);
                Log.Debug(msg);
            }
            else
            {
                Log.Error("fail to move next scene");
            }
        }

        public void Execute()
        {
            Log.Debug(ToString());

            foreach (Scene scene in _sceneList)
            {
                scene.Execute();
            }
        }


        public bool Play()
        {
            if (_sceneList.Count <= 0)
                return false;

            Scene s = _sceneIter.Current;

            if (s.Play())
            {
                return true;
            }
            else
            {
                string msg = string.Format("end. Scene: {0}", s.Description);
                Log.Debug(msg);

                if (_sceneIter.MoveNext())
                {
                    msg = string.Format("begin. Scene: {0}", _sceneIter.Current.Description);
                    Log.Debug(msg);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

    }   // class

}   // namespace
