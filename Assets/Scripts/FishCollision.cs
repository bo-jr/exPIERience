using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCollision : MonoBehaviour {

      void OnCollisionEnter(Collision col) {
         //Debug.Log(gameObject.name + "has collided with" + col.gameObject.name);
         Debug.Log(gameObject.name + " has collided with " + col.gameObject.name);
      }
}
