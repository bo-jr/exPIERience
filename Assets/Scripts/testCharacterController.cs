using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testCharacterController : MonoBehaviour {
   public float speed = 10.0f;
   private float translation;
   private float strafe;
   private bool isGrounded; 
   RaycastHit hit;
   Vector3 physicsCentre;

   void Start()
   {
      Cursor.lockState = CursorLockMode.Locked;
   }

   void Update()
   {
      /*gives character vertical and horizontal movement*/
      translation = Input.GetAxis("Vertical") * speed;
      strafe = Input.GetAxis("Horizontal") * speed;
      translation *= Time.deltaTime;
      strafe *= Time.deltaTime;
      transform.Translate(strafe, 0, translation);

      /*gives the character a "center" so it can jump properly*/
      physicsCentre = this.transform.position + this.GetComponent<CapsuleCollider>().center;
      /*Debug.DrawRay(physicsCentre, Vector3.down * 1.5f, Color.red, 1);*/

      if (Physics.Raycast(physicsCentre, Vector3.down, out hit, 1.5f)) {
         if (hit.transform.gameObject.tag != "Player") {
            isGrounded = true;
         }
      }
      else {
         isGrounded = false;
      }

      /*enables space key bar for jumps*/
      if (Input.GetKeyDown("space") && isGrounded) {
         this.GetComponent<Rigidbody>().AddForce(Vector3.up * 200);
      }

      /*unlocks cursor so that you can press the "unplay" button to quit*/
      if (Input.GetKeyDown("escape"))
      {
         Cursor.lockState = CursorLockMode.None;
      }
   }
}
