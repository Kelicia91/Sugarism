using UnityEngine;
using UnityEngine.UI;


public class SelectTargetButton : MonoBehaviour
{
    /********* Editor Interface *********/
    // objects
    [SerializeField]
    private Text NameText = null;
    [SerializeField]
    private Image Image = null;
    
    //
    private int _targetId = -1;

    
    void Awake()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(onClick);
    }
    
    public void Set(int targetId)
    {
        if (false == ExtTarget.isValid(targetId))
        {
            Log.Error(string.Format("invalid target id; {0}", targetId));
            return;
        }

        _targetId = targetId;

        //@todo: need target thumbnail..
        Target target = Manager.Instance.DT.Target[_targetId];
        set(target.baseShape);
        Character character = Manager.Instance.DT.Character[target.characterId];
        set(character.name);
    }

    private void set(string name)
    {
        if (null == NameText)
        {
            Log.Error("not found name text");
            return;
        }

        NameText.text = name;
    }

    private void set(Sprite sprite)
    {
        if (null == Image)
        {
            Log.Error("not found image");
            return;
        }

        Image.sprite = sprite;
    }


    private void onClick()
    {
        Manager.Instance.UI.SelectTargetPanel.Hide();
        Manager.Instance.Object.CreateTargetCharacter(_targetId);
    }

}   // class
