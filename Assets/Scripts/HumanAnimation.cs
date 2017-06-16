using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAnimation : MonoBehaviour {
   public Animator anim;
   public bool isWalking;
   public bool isUnderwater;
   public float waterLevel;
   public float parentYAxis;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
      isWalking = false;
      isUnderwater = false;
      waterLevel = 40.0f;
	}
	
	// Update is called once per frame
	void Update () {
      /*if 'movement' keys are held down, it'll trigger the 'walking' flag*/
		isWalking = (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D) || 
         Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A));
      
      /*if underwater, then trigger underwater flag*/
      parentYAxis = transform.parent.transform.position.y;
      if ((parentYAxis < waterLevel) != isUnderwater) {
         isUnderwater = parentYAxis < waterLevel;
      }

      /*activate flags*/
      if (isUnderwater) {
         isWalking = false;
         anim.SetBool("isSwimming", isUnderwater);
         anim.SetBool("isWalking", isWalking);         
      }
      else if (!isUnderwater && isWalking) {
         anim.SetBool("isWalking", isWalking);
         anim.SetBool("isSwimming", isUnderwater);
      }
      else {
         anim.SetBool("isWalking", false);
         anim.SetBool("isSwimming", false);
      }
	}
}
