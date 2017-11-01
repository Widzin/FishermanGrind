using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointerController : MonoBehaviour {

	public float speed;
	public Text statusText;

	private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();

	}
	
	// Update is called once per frame
	void Update () {
		float moveHorizontal = Input.GetAxis("Horizontal");

		Vector2 movement = new Vector2(moveHorizontal, transform.position.y);

		rb2d.AddForce(movement * speed);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("GoodBar"))
		{
			statusText.text = "Good";
		}
		if (other.gameObject.CompareTag("BadBar"))
		{
			statusText.text = "Bad";
		}
	}
}
