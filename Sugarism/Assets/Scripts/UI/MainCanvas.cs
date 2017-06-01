using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainCanvas : MonoBehaviour
{
    // prefabs
    public GameObject DialoguePanel;
    public GameObject ClearPanel;
    
    // Use this for initialization
    void Start ()
    {
        // @warn : call order of instantiate()
        //Instantiate(DialoguePanel, gameObject.transform);
        //Instantiate(ClearPanel, gameObject.transform);
    }
}
