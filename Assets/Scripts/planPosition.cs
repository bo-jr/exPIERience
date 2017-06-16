using System.Collections;
using UnityEngine;

public class plants: MonoBehaviour {

 void OnCollisionEnter(Collision col) {
      Debug.Log(gameObject.name + " has collided with " + col.gameObject.name);
      
   }
}