using System.Windows.Input; // for ICommand
using System.Collections.ObjectModel;


namespace ScenarioEditor.ViewModel
{
    public class Scenario : ModelBase, IOwner<Scene>, ITreeViewItem
    {
        public Scenario(Sugarism.Scenario model) : this(Properties.Resources.Scenario, model) { }

        // use after deserialize SceneList
        public Scenario(string filePath, Sugarism.Scenario model)
        {
            _model = model;

            _owner = null;
            _fileFullPath = filePath;

            _sceneList = new ObservableCollection<Scene>();
            foreach (Sugarism.Scene scene in _model.SceneList)
            {
                Scene s = new Scene(scene);
                SceneList.Add(s);
                s.Owner = this;
            }

            _isExpanded = true;
            _isSelected = true;

            _inputBindings = new InputBindingCollection();

            _inputBindings.Add(new KeyBinding(CmdExpand,    Key.Enter,  ModifierKeys.None));
            _inputBindings.Add(new KeyBinding(CmdAddChild,  Key.A,      ModifierKeys.Control));
        }


        #region Field

        private Sugarism.Scenario _model;

        private MainViewModel _owner;
        private string _fileFullPath;
        private ObservableCollection<Scene> _sceneList;

        private bool _isExpanded;
        private bool _isSelected;
        private InputBindingCollection _inputBindings;

        private ICommand _cmdExpand;
        private ICommand _cmdAddChild;

        #endregion //Field


        #region Property

        public Sugarism.Scenario Model { get { return _model; } }

        public MainViewModel Owner
        {
            get { return _owner; }
            set { _owner = value; OnPropertyChanged(); }
        }        
        
        public string FileFullPath
        {
            get { return _fileFullPath; }
            set
            {
                _fileFullPath = value;
                OnPropertyChanged();
                OnPropertyChanged("FileName");
            }
        }
        
        public string FileName  // with Extension
        {
            get { return FileUtils.GetFileName(FileFullPath); }
        }
        
        public ObservableCollection<Scene> SceneList
        {
            get { return _sceneList; }
        }

        public bool IsExpanded
        {
            get { return _isExpanded; }
            set { _isExpanded = value; OnPropertyChanged(); }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; OnPropertyChanged(); }
        }

        public InputBindingCollection InputBindings
        {
            get { return _inputBindings; }
        }

        #endregion //Property


        #region ICommand

        public ICommand CmdExpand
        {
            get
            {
                if (null == _cmdExpand)
                {
                    _cmdExpand = new RelayCommand(param => { IsExpanded = !IsExpanded; }, param => IsSelected);
                }
                return _cmdExpand;
            }
        }

        public ICommand CmdAddChild // IOwner
        {
            get
            {
                if (null == _cmdAddChild)
                {
                    _cmdAddChild = new RelayCommand(param => AddSampleScene(), param => IsSelected);
                }
                return _cmdAddChild;
            }
        }

        #endregion //ICommand



        #region Public Method

        public void AddSampleScene()
        {
            Sugarism.Scene model = new Sugarism.Scene(string.Empty);

            Scene newScene = new Scene(model);

            Insert(0, newScene);
        }

        public void Delete()
        {
            if (null == Owner)
                return;

            Owner.Delete(this);
        }

        #endregion //Public Method
        
        #region Public Method : IOwner

        public void Insert(int index, Scene scene)
        {
            if (index < 0)
            {
                string errMsg = string.Format(Properties.Resources.ErrInsertSceneUnderMin, index, 0);
                Log.Error(errMsg);
                return;
            }

            if (index > SceneList.Count)
            {
                string errMsg = string.Format(Properties.Resources.ErrInsertSceneOverMax, index, SceneList.Count);
                Log.Error(errMsg);
                return;
            }

            if (null == scene)
            {
                Log.Error(Properties.Resources.ErrInsertSceneNullScene);
                return;
            }
            
            SceneList.Insert(index, scene); // Collection<T> accepts null as valid value.
            scene.Owner = this;

            insert(index, scene.Model);
        }

        public void Delete(Scene scene)
        {
            if (SceneList.Count <= Sugarism.Scenario.MIN_COUNT_SCENE)
            {
                string msg = string.Format(Properties.Resources.ErrDeleteSceneUnderMin, Sugarism.Scenario.MIN_COUNT_SCENE);
                Log.Error(msg);
                return;
            }

            if (null != scene)
                scene.Owner = null;

            SceneList.Remove(scene);

            delete(scene.Model);
        }

        public void Up(Scene scene)
        {
            int sceneIndex;
            if (false == canUp(scene, out sceneIndex))
                return;

            int prevIndex = sceneIndex - 1;

            SceneList.Move(sceneIndex, prevIndex);

            up(sceneIndex, prevIndex);
        }

        public void Down(Scene scene)
        {
            int sceneIndex;
            if (false == canDown(scene, out sceneIndex))
                return;

            int nextIndex = sceneIndex + 1;

            SceneList.Move(sceneIndex, nextIndex);

            down(sceneIndex, nextIndex);
        }

        public bool CanUp(Scene scene)
        {
            int sceneIndex;
            return canUp(scene, out sceneIndex);
        }

        public bool CanDown(Scene scene)
        {
            int sceneIndex;
            return canDown(scene, out sceneIndex);
        }

        public int GetIndexOf(Scene scene)
        {
            return SceneList.IndexOf(scene);    // if not found, return -1.
        }

        #endregion //Public Method : IOwner



        #region Private Method

        private void insert(int index, Sugarism.Scene scene)
        {
            _model.SceneList.Insert(index, scene);
        }

        private void delete(Sugarism.Scene scene)
        {
            _model.SceneList.Remove(scene);
        }

        private void up(int oldIndex, int newIndex)
        {
            Sugarism.Scene scene = _model.SceneList[oldIndex];
            insert(newIndex, scene);
            _model.SceneList.RemoveAt(oldIndex + 1);
        }

        private void down(int oldIndex, int newIndex)
        {
            Sugarism.Scene scene = _model.SceneList[oldIndex];
            _model.SceneList.RemoveAt(oldIndex);
            insert(newIndex, scene);
        }

        private bool canUp(Scene scene, out int sceneIndex)
        {
            sceneIndex = GetIndexOf(scene);
            if (sceneIndex <= 0)
                return false;
            else
                return true;
        }

        private bool canDown(Scene scene, out int sceneIndex)
        {
            sceneIndex = GetIndexOf(scene);
            if (sceneIndex < 0)
                return false;

            int lastIndex = SceneList.Count - 1;
            if (sceneIndex.Equals(lastIndex))
                return false;

            return true;
        }

        #endregion //Private Method
    }
}
