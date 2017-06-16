using System.Collections;
using UnityEngine;

public class flock : MonoBehaviour {

	public float speed = 0.1f;
	float rotationSpeed = 4.0f;
	Vector3 averageHeading;
	Vector3 averagePosistion;
	float neighbourDistance = 3.0f;
	public bool collided = false;

	public static bool turning = false;
	Vector3 middle = new Vector3(10, 15, 55);
	// Use this for initialization
	void Start () {
		speed = Random.Range (0.5f, 3);
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (transform.position, middle) >= globalFlock.tankSize || collided) {
			turning = true;
		} 
		
      if (transform.position.y >= 40) {
         turning = true;
         //Debug.Log("Hit the top of water");
      }

		if (transform.position.x >= globalFlock.tankSizeX || 
			 transform.position.y >= globalFlock.tankSizeY ||
			 transform.position.z >= globalFlock.tankSizeZ ||
			  collided) {
			if (Vector3.Distance (transform.position, middle) >= globalFlock.tankSize)
				turning = true;
		} 
		else {
			turning = false;
		}

		if (collided) {
			Vector3 direction = middle - transform.position;
			transform.rotation = Quaternion.Slerp (transform.rotation,
				Quaternion.LookRotation (direction), rotationSpeed * Time.deltaTime);

			speed = Random.Range (0.5f, 3);			
			collided = false;
		}
		else if (turning) {
			Vector3 direction = middle - transform.position;
			transform.rotation = Quaternion.Slerp (transform.rotation,
				Quaternion.LookRotation (direction), rotationSpeed * Time.deltaTime);

			speed = Random.Range (0.5f, 3);
		} 
		else {
			if (Random.Range (0, 5) < 1)
				ApplyRules ();
		}
		
		transform.Translate (0, 0, Time.deltaTime * speed);
	}

	void ApplyRules() {
		GameObject[] gos;
		gos = globalFlock.allFish;

		Vector3 vcentre = middle;
		Vector3 vavoid = middle;
		float gSpeed = 0.1f;

		Vector3 goalPos = globalFlock.goalPos;

		float dist;
      

		int groupSize = 0;
		foreach(GameObject go in gos) {
			if (go != this.gameObject) {
				dist = Vector3.Distance (go.transform.position, this.transform.position);
				if (dist <= neighbourDistance) {
					vcentre += go.transform.position;
					groupSize++;

					if (dist < 1.0f) {
						vavoid = vavoid + (this.transform.position - go.transform.position);
					}

					flock anotherFlock = go.GetComponent<flock>();
					gSpeed = gSpeed + anotherFlock.speed;
				}
			}
		}

		if (groupSize > 0) {
			vcentre = vcentre / groupSize + (goalPos - this.transform.position);
			speed = gSpeed / groupSize;

			Vector3 direction = (vcentre + vavoid) - transform.position;
			if (direction != middle)
				transform.rotation = Quaternion.Slerp (transform.rotation,
					Quaternion.LookRotation (direction), rotationSpeed + Time.deltaTime);
		}
	}

   void OnCollisionEnter(Collision col) {
      //Debug.Log(gameObject.name + " has collided with " + col.gameObject.name);
      collided = true;
   }
}
