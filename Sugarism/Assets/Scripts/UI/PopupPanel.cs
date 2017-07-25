using UnityEngine;


public class PopupPanel : Panel
{
    /********* Editor Interface *********/
    // prefabs
    public GameObject PrefConfirmMessagePanel;


    //
    private ConfirmMessagePanel _confirmMsg = null;
    public ConfirmMessagePanel ConfirmMessage { get { return _confirmMsg; } }


    //
    void Awake()
    {
        create();

        Manager.Instance.Object.EndNurtureEvent.Attach(onEndNurture);
    }

    //
    void Start()
    {
        ConfirmMessage.Hide();
    }

    //
    private void create()
    {
        GameObject o = null;

        o = Instantiate(PrefConfirmMessagePanel);
        o.transform.SetParent(transform, false);
        _confirmMsg = o.GetComponent<ConfirmMessagePanel>();
    }

    //
    private void onEndNurture()
    {
        ConfirmMessage.Show(Def.STORY_ENDING_START_MSG, onConfirmEndNurture);
    }

    private void onConfirmEndNurture()
    {
        Manager.Instance.Object.EndStory();
    }

}   // class
