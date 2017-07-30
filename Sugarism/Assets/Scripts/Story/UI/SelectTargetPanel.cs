using UnityEngine;
using UnityEngine.UI;


public class SelectTargetPanel : Panel
{
    /********* Editor Interface *********/
    // prefabs
    [SerializeField]
    private GameObject PrefSelectTargetButton = null;
    // object
    [SerializeField]
    private Text TitleText = null;
    [SerializeField]
    private ScrollRect ScrollView = null;


    // Use this for initialization
    void Awake()
    {
        TitleText.text = Def.CMD_SELECT_TARGET;

        create(PrefSelectTargetButton);

        Manager.Instance.Object.StoryMode.SelectTargetEvent.Attach(onSelectTarget);
    }
    
    private void create(GameObject prefab)
    {        
        RectTransform parent = ScrollView.content;

        int numOfTarget = Manager.Instance.DT.Target.Count;
        for (int i = 0; i < numOfTarget; ++i)
        {
            GameObject o = Instantiate(PrefSelectTargetButton);
            o.transform.SetParent(parent, false);

            SelectTargetButton btn = o.GetComponent<SelectTargetButton>();
            btn.Set(i);
        }
    }

    private void onSelectTarget()
    {
        Show();
    }

}   // class
