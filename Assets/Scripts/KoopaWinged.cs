using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoopaWinged : Enemy {
	public GameObject Koopa;

	void Start() { // ???
		starmanBonus = 100;
		rollingShellBonus = 500;
		hitByBlockBonus = 100;
		fireballBonus = 100;
		stompBonus = 100;
	}

	protected override void FlipAndDie() {
		base.FlipAndDie ();
		GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Dynamic;
	}

	void LoseWings() {
		StartCoroutine (SpawnKoopaCo ());
	}

	IEnumerator SpawnKoopaCo() {
		StopInteraction ();
		gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		yield return new WaitForSecondsRealtime(.05f); // prevent immediate damage by spawned koopa
		GameObject newKoopa = Instantiate (Koopa, transform.position, Quaternion.identity);
		newKoopa.GetComponent<MoveAndFlip> ().directionX = transform.localScale.x;
		Destroy (gameObject.transform.parent.gameObject); // destroy whole Koopa Struct
		isBeingStomped = false;
	}

	public override void TouchedByRollingShell() {
		FlipAndDie ();
	}

	public override void HitBelowByBlock() {
		LoseWings ();
	}

	public override void HitByMarioFireball() {
		FlipAndDie ();
	}

	public override void StompedByMario() {
		isBeingStomped = true;
		LoseWings ();
	}
}
