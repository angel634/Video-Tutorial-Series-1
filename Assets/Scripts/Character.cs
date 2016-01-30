using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour {

	public float speed;
	public KeyCode jumpKey = KeyCode.Space;
	public Sprite[] walkSprites;
	public Sprite idleSprite;
	public float jumpSpeed;

	bool isWalking;
	bool isGrounded;

	RaycastHit2D hit;

	void Update () {
		Rigidbody2D r = GetComponent<Rigidbody2D> ();
		r.velocity = new Vector2 (Input.GetAxis ("Horizontal") * speed, r.velocity.y);
		if (r.velocity.magnitude > 0.1f) {
			if (!isWalking) {
				StartCoroutine (Walk ());
			}
		}

		hit = Physics2D.Raycast (new Vector2 (transform.position.x, transform.position.y), Vector2.down);
		Debug.Log (Vector2.Distance(new Vector2 (transform.position.x, transform.position.y), hit.collider.transform.position));
		if (hit.distance < 1.9f) {
			isGrounded = true;
			Debug.Log ("On ground.");
		} else {
			isGrounded = false;
			Debug.Log("Off ground.");
		}

		if (isGrounded) {
			if (Input.GetKeyDown (jumpKey)) {
				r.velocity = new Vector2 (r.velocity.x, jumpSpeed);
			}
		}

		if (!isWalking) {
			GetComponent<SpriteRenderer> ().sprite = idleSprite;
		}
	}

	IEnumerator Walk() {
		isWalking = true;
		GetComponent<SpriteRenderer> ().sprite = walkSprites [0];
		yield return new WaitForSeconds (0.25f);
		GetComponent<SpriteRenderer> ().sprite = walkSprites [1];
		yield return new WaitForSeconds (0.25f);
		isWalking = false;
	}
}
