using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatController : MonoBehaviour {

	//movement of float
	public float speed;
	//rigidbody2d of float
	private Rigidbody2D rb2d;


	//texts fields for showing damage done to objects
	public Text lineDamageText;
	public Text fishDamageText;
	//value of damage done to objects
	private float fishDamage;
	private float lineDamage;
	//damage size by every frame
	public float damageSize;


	//time to change fish event
	private float interval;
	//text field for showing time passed from last change of flag
	public Text valueText;
	//range values for random
	public float minRange;
	public float maxRange;


	//text field for showing value of flag
	public Text tempText;
	//variable bool for event of fish
	private bool fishPull;
	//time passed from last flag change
	private float fullTime;


	//variable bool of showing if float is in good position
	private bool goodPosition;

	// Use this for initialization
	void Start()
	{
		fishPull = true;
		rb2d = GetComponent<Rigidbody2D>();
		fishDamage = 0f;
		lineDamage = 0f;
		fullTime = 0f;
		interval = 2f;
	}

	// Update is called once per frame
	void Update()
	{
		MoveFloat();
		ShowFishEvent();
		CalculateAndPrintDamage();
		ChangeFlag();
	}

	//Function controlling moving the hook
	private void MoveFloat()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		Vector2 movement = new Vector2(moveHorizontal, transform.position.y);
		rb2d.AddForce(movement * speed);
	}

	//Function calculating and changing the time, that fish is making sth for specific period
	private void ChangeFlag()
	{

		fullTime += Time.deltaTime;
		valueText.text = "Time to change:\n" + (interval - fullTime).ToString();


		if (interval < fullTime)
		{
			fishPull = !fishPull;
			fullTime = 0f;
			interval = Random.Range(minRange, maxRange);
		}
	}

	//Function showing if fish is pulling the hook or not
	private void ShowFishEvent()
	{
		if (fishPull)
		{
			tempText.text = "Fish is pulling";
		}
		else
		{
			tempText.text = "Fish is not moving";
		}
	}

	//Function calculating damage done to hook or fish
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

		fishDamageText.text = "Damage done to fish:\n" + fishDamage.ToString();
		lineDamageText.text = "Damage done to hook:\n" + lineDamage.ToString();
	}

	//Functions checking if float is in good position
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
