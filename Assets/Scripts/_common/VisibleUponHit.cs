using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Activate sprite renderer if bumped by Player's head
 * Applicable to: Hidden collectible blocks
 */

public class VisibleUponHit : MonoBehaviour {
	private SpriteRenderer m_SpriteRenderer;

	// Use this for initialization
	void Start () {
		m_SpriteRenderer = GetComponent<SpriteRenderer> ();
		m_SpriteRenderer.enabled = false;
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			m_SpriteRenderer.enabled = true;
		}
	}
}
