using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatController : MonoBehaviour {

	public float speed;
	public Text lineDamageText;
	public Text fishDamageText;
	public float damageSize;

	public float randomSeed;
	public float interval;

	public Text valueText;
	public Text tempText;
	private bool yes;
	private float time;

	private Rigidbody2D rb2d;
	private float fishDamage;
	private float lineDamage;
	private bool goodPosition;

	private float fullTime;

	// Use this for initialization
	void Start()
	{
		yes = true;
		rb2d = GetComponent<Rigidbody2D>();
		fishDamage = 0f;
		lineDamage = 0f;
		fullTime = 0f;
		ChangeTime();
		//InvokeRepeating("ChangeYes", 0f, time);
		//Invoke("ChangeFlag", time);
	}

	// Update is called once per frame
	void Update()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");

		Vector2 movement = new Vector2(moveHorizontal, transform.position.y);

		rb2d.AddForce(movement * speed);

		if (yes)
		{
			tempText.text = "True";
		}
		else
		{
			tempText.text = "False";
		}

		CalculateAndPrintDamage();

		//Invoke("ChangeYes", time);
		//randomSeed = Random.Range(0f, 4f);
		ChangeFlag();
	}

	private void ChangeFlag()
	{

		fullTime += Time.deltaTime;
		valueText.text = fullTime.ToString();


		if (interval < fullTime)
		{
			yes = !yes;
			fullTime = 0f;
			interval = Random.Range(5f, 10f);
		}

		//ChangeTime(interval, random);
	}

	private void ChangeTime()
	{
		time = Random.Range(10f, 40f);
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
