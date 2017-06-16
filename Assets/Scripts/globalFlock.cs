using System.Collections;
using UnityEngine;

public class globalFlock : MonoBehaviour {

	public GameObject fishPrefab;
	//public GameObject goalPrefab;
	public static int tankSize = 70;

	public static int tankSizeX = 145;
	public static int tankSizeY = 25;
	public static int tankSizeZ = 55;

	static int numFish = 1;
	public static GameObject[] allFish = new GameObject[numFish];

	public static Vector3 goalPos = Vector3.zero;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < numFish; i++) {
			Vector3 pos = new Vector3 (Random.Range(-tankSize, tankSize),
									   Random.Range(5, 35),
									   Random.Range(-tankSize, tankSize));
			allFish [i] = (GameObject)Instantiate (fishPrefab, pos, Quaternion.identity);
		}
	}

	// Update is called once per frame
	void Update () {
		if (Random.Range (0, 10000) < 50) {
			goalPos = new Vector3(Random.Range(-tankSize, tankSize),
								  Random.Range(10, tankSize),
								  Random.Range(-tankSize, tankSize));
		}

		//goalPrefab.transform.position = goalPos;
	}
}
