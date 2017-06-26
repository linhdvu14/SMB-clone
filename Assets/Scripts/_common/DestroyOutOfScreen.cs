using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Destroy if object goes out of screen
 * Applicable to: Vertical Moving Platform for Spawner, Mario Fireball
 */

public class DestroyOutOfScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnBecameInvisible() {
		Destroy (gameObject);
	}
}
