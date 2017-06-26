using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformObjectDetector : MonoBehaviour {
	private Dictionary<GameObject, Transform> objectsOnTop; // store objects and their original parents


	// Use this for initialization
	void Start () {
		objectsOnTop = new Dictionary<GameObject, Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnTriggerEnter2D(Collider2D other) {
		if (!objectsOnTop.ContainsKey (other.gameObject)) {
			objectsOnTop.Add (other.gameObject, other.transform.parent);
			other.transform.parent = transform; // make object stick to platform
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		Transform oldParent;
		if (objectsOnTop.TryGetValue(other.gameObject, out oldParent)) {
			other.transform.parent = oldParent; // unstick object
			objectsOnTop.Remove(other.gameObject);
		}
	}
}
