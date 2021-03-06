﻿using System.Collections.Generic;


namespace Story
{
    // @note : differ from struct 'UnityEngine.Scene'
    public class Scene : IPlayable
    {
        private Mode _mode = null;
        private Sugarism.Scene _model = null;

        private List<Command> _cmdList = null;
        private IEnumerator<Command> _cmdIter = null;

        public Scene(Sugarism.Scene model, Mode mode)
        {
            if (null == model)
            {
                Log.Error("Not Found Sugarism.Scene");
                return;
            }

            _model = model;
            _mode = mode;

            _cmdList = new List<Command>();
            foreach (Sugarism.Command mCmd in _model.CmdList)
            {
                Command cmd = Command.Create(mCmd, _mode);
                _cmdList.Add(cmd);
            }

            _cmdIter = _cmdList.GetEnumerator();
            _cmdIter.MoveNext();
        }


        #region Property

        public string Description
        {
            get { return _model.Description; }
        }

        #endregion


        public void Execute()
        {
            Log.Debug(ToString());

            foreach (Command cmd in _cmdList)
            {
                cmd.Execute();
            }
        }

        public override string ToString()
        {
            string s = string.Format("[Scene] Description : {0}", Description);
            return s;
        }



        public bool Play()
        {
            if (_cmdList.Count <= 0)
                return false;

            Command cmd = _cmdIter.Current;

            if (cmd.Play())
                return true;
            else
                return _cmdIter.MoveNext();
        }

    }   // class

}   // namespace
