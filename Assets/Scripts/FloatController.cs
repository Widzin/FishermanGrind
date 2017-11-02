using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatController : MonoBehaviour {

	//movement of float
	public float speed;
	//rigidbody2d of float
	private Rigidbody2D rb2d;
	//step
	private float step;

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
		interval = minRange;
		step = 0.1f;
	}

	// Update is called once per frame
	void Update()
	{
		MoveFloat();
		ShowFishEvent();
		CalculateAndPrintDamage();
		ChangeFlag();
	}

	private Vector2 temp;

	//Function controlling moving the hook
	private void MoveFloat()
	{
		CheckInputKeys();
		rb2d.velocity = new Vector2(speed, 0);
	}

	//Function to check hold keys and setting from them speed and direction 
	private void CheckInputKeys()
	{
		if (Input.GetKey("left"))
		{
			if (fishPull)
				speed = -1.7f;
			else
				speed = -0.5f;
		}
		else if (Input.GetKey("right"))
		{
			if (fishPull)
				speed = 0.5f;
			else
				speed = 1.7f;
		}
		else
		{
			if (fishPull)
				speed = -1f;
			else
				speed = 1f;
		}
	}

	//Function calculating and changing the time, that fish is making sth for specific period
	private void ChangeFlag()
	{

		fullTime += Time.deltaTime;
		valueText.text = "Time to change:\n" + (interval - fullTime).ToString();


		if (interval < fullTime)
		{
			fishPull = !fishPull;
			speed = (-1) * speed;
			//step = (-1) * step;
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
