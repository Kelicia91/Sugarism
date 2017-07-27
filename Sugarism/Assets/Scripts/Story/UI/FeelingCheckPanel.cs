using UnityEngine;
using UnityEngine.EventSystems;


public class FeelingCheckPanel : Panel, IPointerClickHandler
{
    /********* Editor Interface *********/
    // objects
    public StoryCharacterPanel TargetPanel;

    public SliderPanel FeelingPanel;
    public SliderPanel ProgressPanel;
    
    //
    private const string VALUE_FORMAT = "{0} %";


    //
    void Start()
    {
        const int PERCENT_MIN = 0;
        const int PERCENT_MAX = 100;

        FeelingPanel.SetMinMax(PERCENT_MIN, PERCENT_MAX);
        FeelingPanel.SetNameText(Def.CMD_FEELING_CHECK);

        ProgressPanel.SetMinMax(PERCENT_MIN, PERCENT_MAX);
        ProgressPanel.SetNameText(Def.PROGRESS);
        
        Story.TargetCharacter tc = Manager.Instance.Object.StoryMode.TargetCharacter;

        Target t = Manager.Instance.DT.Target[tc.Id];
        TargetPanel.Set(tc.Id, false, Sugarism.EFace.Default, Sugarism.ECostume.Default, Sugarism.EPosition.Middle);

        setFeeling(tc.Feeling);
        setProgress(tc.LastOpenedScenarioNo);

        tc.FeelingChangeEvent.Attach(onFeelingChanged);
        tc.LastOpenedScenarioNoChangeEvent.Attach(onLastOpenedScenarioNoChanged);
    }


    private void setFeeling(int feeling)
    {
        float ratio = feeling / (float)Def.MAX_FEELING;
        int percent = Mathf.FloorToInt(ratio * 100);

        FeelingPanel.SetValue(percent, VALUE_FORMAT);
    }

    private void setProgress(int lastOpenedScenarioNo)
    {
        float ratio = lastOpenedScenarioNo / (float)Def.MAX_SCENARIO;
        int percent = Mathf.FloorToInt(ratio * 100);

        ProgressPanel.SetValue(percent, VALUE_FORMAT);
    }


    private void onFeelingChanged(int feeling)
    {
        setFeeling(feeling);
    }

    private void onLastOpenedScenarioNoChanged(int no)
    {
        setProgress(no);
    }


    // 오브젝트에서 포인터를 누르고 동일한 오브젝트에서 뗄 때 호출됩니다.
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        Log.Debug("click FeelingCheckPanel");

        Hide();
    }
}
