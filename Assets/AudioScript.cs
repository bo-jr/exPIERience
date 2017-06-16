using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour {
   private AudioSource source;
   private int flag;
	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
        flag = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (underwater.isUnderwater && flag == 0) {
         source.Play();
         flag = 1;
      }
	}
}