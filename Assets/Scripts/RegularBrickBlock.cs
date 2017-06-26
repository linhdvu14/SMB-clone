using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularBrickBlock : MonoBehaviour {
	public GameObject BrickPiece;
	public GameObject TempCollider;
	public GameObject BlockCoin;
	private float WaitBetweenBounce = .25f;

	private LevelManager t_LevelManager;
	private Animator m_Animator;
	private RegularBrickBlockCoinDetector m_CoinDetector;

	private float time1, time2;
	private List<GameObject> enemiesOnTop = new List<GameObject> ();


	// Use this for initialization
	void Start () {
		t_LevelManager = FindObjectOfType<LevelManager> ();
		m_Animator = GetComponent<Animator> ();
		m_CoinDetector = transform.parent.FindChild ("Coin Detector").GetComponent<RegularBrickBlockCoinDetector> ();
		time1 = 0;
	}


	void OnTriggerEnter2D(Collider2D other) {
		time2 = Time.time;
		if (other.tag == "Player" && time2-time1 >= WaitBetweenBounce) {
			// Hit any enemy on top
			foreach (GameObject enemyObj in enemiesOnTop) {
				t_LevelManager.BlockHitEnemy (enemyObj.GetComponent<Enemy> ());
			}

			// check and collect coins on top
			if (m_CoinDetector.coinOnTop) {
				Instantiate (BlockCoin, transform.position + new Vector3(0, 2, 0), Quaternion.identity);
				Destroy (m_CoinDetector.coinOnTop);
			}

			// Bounce or break depending on Mario's size
			if (t_LevelManager.marioSize == 0) {
				m_Animator.SetTrigger ("bounce");
				t_LevelManager.soundSource.PlayOneShot (t_LevelManager.bumpSound);
			} else {
				BreakIntoPieces ();
				t_LevelManager.AddScore(t_LevelManager.breakBlockBonus);
				t_LevelManager.soundSource.PlayOneShot (t_LevelManager.breakBlockSound);
			}
			time1 = Time.time;
		}
	}

		
	void BreakIntoPieces() {
		GameObject brickPiece;
		brickPiece = Instantiate (BrickPiece, transform.position, Quaternion.Euler(new Vector3(45, 0, 0))); // up right
		brickPiece.GetComponent<Rigidbody2D> ().velocity = new Vector2 (3f, 12f);
		brickPiece = Instantiate (BrickPiece, transform.position, Quaternion.Euler(new Vector3(45, 0, 0))); // up left
		brickPiece.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-3f, 12f);
		brickPiece = Instantiate (BrickPiece, transform.position, Quaternion.Euler(new Vector3(45, 0, 0))); // down right
		brickPiece.GetComponent<Rigidbody2D> ().velocity = new Vector2 (3f, 8f);
		brickPiece = Instantiate (BrickPiece, transform.position, Quaternion.Euler(new Vector3(45, 0, 0))); // down left
		brickPiece.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-3f, 8f);

		// prevent Player from breaking bricks above this
		Instantiate (TempCollider, transform.position, Quaternion.identity);
		Destroy (transform.gameObject);
	}

	// check for enemy on top
	void OnCollisionStay2D(Collision2D other) {
		Vector2 normal = other.contacts[0].normal;
		Vector2 topSide = new Vector2 (0f, -1f);
		bool topHit = normal == topSide;
		if (other.gameObject.tag.Contains("Enemy") && topHit && !enemiesOnTop.Contains (other.gameObject)) {
			enemiesOnTop.Add (other.gameObject);
		}
	}

	void OnCollisionExit2D(Collision2D other) {
		if (other.gameObject.tag.Contains("Enemy")) {
			enemiesOnTop.Remove (other.gameObject);
		}
	}
}
