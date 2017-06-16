using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

      if (OVRInput.Get(OVRInput.Button.Four)) {
         Debug.Log("Pressed Exit");
         Application.LoadLevel("MenuScreen");
      }
      if (OVRInput.Get(OVRInput.Button.One)) {
         Debug.Log("Pressed Exit");
         Application.LoadLevel("pier");
      }
	}
}
