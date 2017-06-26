using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Applicable to: Goomba, Koopa, Koopa Shell, Koopa Winged, Piranha, Firebar, Bowser Fire, Bowser
 */

public class Enemy : MonoBehaviour {
	public Vector2 flippedVelocity = new Vector2(0, 3);
	public int starmanBonus;
	public int rollingShellBonus;
	public int hitByBlockBonus;
	public int fireballBonus;
	public int stompBonus;

	public bool isBeingStomped;
	
	protected virtual void FlipAndDie() {
		Animator m_Animator = GetComponent<Animator> ();
		Rigidbody2D m_Rigidbody2D = GetComponent<Rigidbody2D> ();
		m_Animator.SetTrigger ("flipped");
		m_Rigidbody2D.velocity += flippedVelocity;
		gameObject.layer = LayerMask.NameToLayer ("Falling to Kill Plane");
		gameObject.GetComponent<SpriteRenderer> ().sortingLayerName = "Foreground Effect";
	}

	protected void StopInteraction() {
		GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeAll;
		foreach (Collider2D c in GetComponents<Collider2D>()) {
			c.enabled = false;
		}
	}

	protected void ResumeInteraction() {
		GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeRotation;
		foreach (Collider2D c in GetComponents<Collider2D>()) {
			c.enabled = true;
		}
	}

	public virtual void TouchedByStarmanMario() {
		FlipAndDie ();
	}
		
	public virtual void TouchedByRollingShell() {
		FlipAndDie ();
	}
		
	public virtual void HitBelowByBlock() {
		FlipAndDie ();
	}

	public virtual void HitByMarioFireball() {
		FlipAndDie ();
	}

	public virtual void StompedByMario() {
	}


}
