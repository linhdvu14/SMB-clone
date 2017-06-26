using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Automatically destroy game object after delay
 * Applicable to: Mario Fireball, Brick Block Temp Collider
 */ 

public class DestroyAfterDelay : MonoBehaviour {
	public float delay;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, delay);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
