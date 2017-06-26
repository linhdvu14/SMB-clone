using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : Enemy {
	private Animator m_Animator;
	private float stompedDuration = 0.5f;

	// Use this for initialization
	void Start () {
		m_Animator = GetComponent<Animator> ();

		starmanBonus = 100;
		rollingShellBonus = 500;
		hitByBlockBonus = 100;
		fireballBonus = 100;
		stompBonus = 100;
	}

	public override void StompedByMario() {
		isBeingStomped = true;
		StopInteraction ();
		Debug.Log (this.name + " StompedByMario: stopped interaction");
		m_Animator.SetTrigger ("stomped");
		Destroy (gameObject, stompedDuration);
		isBeingStomped = false;
	}
}
