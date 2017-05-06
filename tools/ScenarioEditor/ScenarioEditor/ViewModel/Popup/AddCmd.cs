using System;
using System.Collections.Generic;


namespace ScenarioEditor.ViewModel.Popup
{
    public class AddCmd : ModelBase
    {
        #region Singleton

        private static AddCmd _instance = null;
        public static AddCmd Instance
        {
            get
            {
                if (null == _instance)
                    _instance = new AddCmd();

                return _instance;
            }
        }

        private AddCmd()
        {
            _cmdTypeList = new List<Sugarism.Command.Type>();

            Type t = typeof(Sugarism.Command.Type);
            foreach (Sugarism.Command.Type cmdType in Enum.GetValues(t))
            {
                switch (cmdType)
                {
                    case Sugarism.Command.Type.Case:
                    case Sugarism.Command.Type.MAX:
                        continue;

                    default:
                        break;
                }

                _cmdTypeList.Add(cmdType);
            }
        }

        #endregion //Singleton



        #region Property

        private List<Sugarism.Command.Type> _cmdTypeList;
        public List<Sugarism.Command.Type> CmdTypeList
        {
            get { return _cmdTypeList; }
        }

        private int _selectedIndex;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set { _selectedIndex = value; OnPropertyChanged(); }
        }

        #endregion //Property
        

        #region ICommand
        #endregion //ICommand



        #region Public Method

        /// <summary>
        /// Show View.Popup.AddCmd.
        /// </summary>
        /// <returns>Sugarism.Command.Type to add. If window is cancled, return Sugarism.Command.Type.MAX.</returns>
        public Command Show()
        {
            reset();

            View.Popup.AddCmd view = new View.Popup.AddCmd(this);

            bool? result = view.ShowDialog();
            switch (result)
            {
                case true:
                    return create();

                default:
                    return null;
            }
        }

        #endregion //Public Method



        #region Private Method

        private void reset()
        {
            if (CmdTypeList.Count > 0)
                SelectedIndex = 0;
            else
                SelectedIndex = -1;
        }

        private Command create()
        {
            if (SelectedIndex < 0)
                return null;

            if (SelectedIndex >= CmdTypeList.Count)
                return null;

            Sugarism.Command.Type selectedCmdType = CmdTypeList[SelectedIndex];
            switch(selectedCmdType)
            {
                case Sugarism.Command.Type.Lines:
                    {
                        Sugarism.CmdLines linesModel = new Sugarism.CmdLines(Sugarism.Character.START_ID);
                        return new CmdLines(linesModel);
                    }

                case Sugarism.Command.Type.Switch:
                    {
                        Sugarism.CmdSwitch switchModel = new Sugarism.CmdSwitch(Sugarism.Character.START_ID);
                        return new CmdSwitch(switchModel);
                    }

                default:
                    return null;
            }
        }

        #endregion //Private Method
    }
}
