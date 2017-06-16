using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class underwater : MonoBehaviour {
   public float waterLevel = 40;
   public float neutralThreshold = 1.0f;
   public float floatScaler = 0.01f;
   public float floatDecay = 0.002f; 
   public float maxBouyancy = 0.03f;
   public float minBouyancy = -0.03f;
   public static bool isUnderwater;
   
   private Color normalColor;
   private Color underwaterColor;
   private float underwaterGravity;
   private float normalGravity;
   private float bouyancy; 

   protected CharacterController Controller = null;
	// Use this for initialization
	void Start () {
      Controller = gameObject.GetComponent<CharacterController>();

		normalColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
      underwaterColor = new Color(0.22f, 0.65f, 0.77f, 0.5f);

      isUnderwater = false;
      underwaterGravity = 0.0f;
      bouyancy = 0.0f; 
      normalGravity = Physics.gravity.y;
      Physics.gravity = new Vector3(0f, -9.81f, 0f);
      SetNormal();
	}
	
	// Update is called once per frame
	void Update () {
      Physics.gravity = new Vector3(0, normalGravity, 0);

   	/* Check and update underwater / normal state */
      if ((transform.position.y < waterLevel - 2) != isUnderwater) {
         isUnderwater = transform.position.y < waterLevel - 2;

         if (isUnderwater) {
            SetUnderwater();
         }
         else {
            SetNormal();
         }
      }

      if (isUnderwater) {
         /* Collect left and right trigger inputs, 0 - 1.0 input range. */
         float primTrig = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger); 
         float secTrig = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);

         if (secTrig > 0) { //bouyancy increase
            bouyancy += floatScaler; 
         }
         
         if (primTrig > 0) { //bouyancy decrease
            bouyancy -= floatScaler;
         }

         /* Lock bouyancy to min / max */
         bouyancy = (bouyancy > maxBouyancy) ? maxBouyancy : bouyancy;
         bouyancy = (bouyancy < minBouyancy) ? minBouyancy : bouyancy;

         /* decay the bouyancy */
         if (bouyancy > 0) {
            bouyancy -= floatDecay;
            bouyancy = (bouyancy > 0) ? bouyancy : 0;
         }
         else if (bouyancy < 0) {
            bouyancy += floatDecay;
            bouyancy = (bouyancy < 0) ? bouyancy : 0;
         }

         /* Move the amount that bouyancy dictates */
         Controller.Move(new Vector3(0, bouyancy, 0));
      }
	}

   void SetNormal() {
      RenderSettings.fogColor = normalColor;
      RenderSettings.fogDensity = 0.002f;
      Physics.gravity = new Vector3(0, normalGravity, 0);
   }

   void SetUnderwater() {
      RenderSettings.fogColor = underwaterColor;
      RenderSettings.fogDensity = 0.15f;
      Physics.gravity = new Vector3(0, underwaterGravity, 0);
   }
}