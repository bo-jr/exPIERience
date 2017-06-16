using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRocks : MonoBehaviour {
   static int numRocks = 1000;
   public GameObject rockPrefab;
   public static GameObject[] allRocks = new GameObject[numRocks];

	// Use this for initialization
	void Start () {
		for (int i = 0; i < numRocks; i++) {
         Vector3 pos = new Vector3(Random.Range(-65, 85), Random.Range(-.5f, .5f), Random.Range(-35, 80));
         rockPrefab.transform.localScale = transform.localScale * Random.Range(0, 2.5f);//new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), Random.Range(-5, 5));
         allRocks[i] = (GameObject)Instantiate (rockPrefab, pos, Random.rotation);
         /*allRocks[i] = (GameObject)Instantiate (rockPrefab, pos, Quaternion.identity);*/
      }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
