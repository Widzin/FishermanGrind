using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatController : MonoBehaviour {

	public float speed;
	public Text lineDamageText;
	public Text fishDamageText;
	public float damageSize;

	private Rigidbody2D rb2d;
	private float fishDamage;
	private float lineDamage;
	private bool goodPosition;

	// Use this for initialization
	void Start()
	{
		rb2d = GetComponent<Rigidbody2D>();
		fishDamage = 0f;
		lineDamage = 0f;
	}

	// Update is called once per frame
	void Update()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");

		Vector2 movement = new Vector2(moveHorizontal, transform.position.y);

		rb2d.AddForce(movement * speed);

		CalculateAndPrintDamage();
	}

	private void CalculateAndPrintDamage()
	{
		if (goodPosition)
		{
			fishDamage += damageSize * Time.deltaTime;
		}
		else
		{
			lineDamage += damageSize * Time.deltaTime;
		}

		fishDamageText.text = fishDamage.ToString();
		lineDamageText.text = lineDamage.ToString();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("GoodBar"))
		{
			goodPosition = true;
		}
		if (other.gameObject.CompareTag("BadBar"))
		{
			goodPosition = false;
		}
	}

	private void OnTriggerExit2d(Collider2D other)
	{
		if (other.gameObject.CompareTag("GoodBar"))
		{
			goodPosition = false;
		}
		if (other.gameObject.CompareTag("BadBar"))
		{
			goodPosition = true;
		}
	}
}
