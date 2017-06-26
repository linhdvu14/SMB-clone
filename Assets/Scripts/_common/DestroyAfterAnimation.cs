using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Automatically delete game object after animation finishes
 * Applicable to: Block Coin, Floating Text Effect
 */


public class DestroyAfterAnimation : MonoBehaviour {
	public float delay = 0f; // optional delay

	// Use this for initialization
	void Start () {
		if (gameObject.transform.parent.gameObject) {
			Destroy (gameObject.transform.parent.gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);
		}
		Destroy (gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay); 
	}

}