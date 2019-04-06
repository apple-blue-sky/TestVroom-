using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testIO : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if ( Input.GetButtonDown("KeyCode.Space"))
        {
            Debug.Log("SPACEBAR DETECTED");
        }
	}
}
