using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Main : MonoBehaviour
{
    // prefabs
    public GameObject EventSystem;
    public GameObject Canvas;

	// Use this for initialization
	void Start ()
    {
        Instantiate(EventSystem);
        Instantiate(Canvas);
	}
}
