using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour {

	public GameObject thePlatform;
	public Transform generationPoint;
	public float distanceBetween;

	private float platformWidth;

	public float distanceBetweenMin;
	public float distanceBetweenMax;

	//public GameObject[] thePlatforms; // array holding platforms of diff length

	private int platformSelector; // random index to access the array

	private float[] platformWidths;


	public ObjectPooler[] theObjectPools;

	// Use this for initialization
	void Start () {
		//platformWidth = thePlatform.GetComponent<BoxCollider2D>().size.x;
		//Initially setting a platform width variable to the actual width of the platform. To use in calculation below

		platformWidths = new float[theObjectPools.Length];
		for (int i = 0; i < theObjectPools.Length; i++) {
			platformWidths[i] = theObjectPools[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < generationPoint.position.x) {

			distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);
			//setting random distance between platforms

			platformSelector = Random.Range(0, theObjectPools.Length);

			transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector]/2) + distanceBetween, transform.position.y, transform.position.z);



			//Instantiate(/*thePlatform*/ theObjectPools[platformSelector], transform.position, transform.rotation);
			//To implement object pool we don't instantiate objects on the go. Instead we create a pool of 
			// objects which we toggle between active and inactive. In PlatformDestroyer we make them inactive
		
			GameObject newPlatform = theObjectPools[platformSelector].GetPooledObject();
			newPlatform.transform.position = transform.position;
			newPlatform.transform.rotation = transform.rotation;
			newPlatform.SetActive(true);

			transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector]/2) , transform.position.y, transform.position.z);
		}

	}
}
