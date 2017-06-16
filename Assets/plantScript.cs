using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plantScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


 void OnCollisionEnter(Collision col) {
      Debug.Log(gameObject.name + " has collided with " + col.gameObject.name);
   }}
