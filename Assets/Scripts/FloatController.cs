using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatController : MonoBehaviour {

	//text field for showing countdown
	public Text countDownText;
	//value of countdown length
	public int waitTime;
	//variable needed to count, how much time passed in countdown
	private float counter;

	//movement of float
	public float speed;
	//rigidbody2d of float
	private Rigidbody2D rb2d;


	//texts fields for showing damage done to objects
	public Text hookDamageText;
	public Text fishDamageText;
	//values of objects life at beggining
	public float fishLife;
	public float hookLife;
	//values of actual objects life
	private float actualFishLife;
	private float actualHookLife;
	//healthbars of objects
	public GameObject fishHealthBar;
	public GameObject hookHealthBar;
	//values of damage done to objects
	private float fishDamage;
	private float hookDamage;
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
	public Text eventText;
	//variable bool for event of fish
	private bool fishPull;
	//time passed from last flag change
	private float fullTime;

	//variable bool of showing if float is in good position
	private bool goodPosition;

	//variable bool of checking if countDown has started
	private bool play;
	//variable bool of checking if game is ended
	private bool endOfGame;

	// Use this for initialization
	void Start()
	{
		endOfGame = false;
		play = false;
		countDownText.fontSize = 144;
		counter = 0f;

		actualFishLife = 100f;
		actualHookLife = 100f;
		fishPull = true;
		rb2d = GetComponent<Rigidbody2D>();
		fishDamage = 0f;
		hookDamage = 0f;
		fullTime = 0f;
		interval = minRange;
	}

	// Update is called once per frame
	void Update()
	{
		if (!endOfGame)
		{
			if (!play)
			{
				CountDown();
			}
			else
			{
				MoveFloat();
				ShowFishEvent();
				CalculateAndPrintDamage();
				ChangeFlag();
				ChangeHealthBarLengths();
			}

			CheckIfEndOfGame();
		}			
	}

	//function to showing countdown at the beggining of the game
	private void CountDown()
	{
		ResetTextFields();
		countDownText.text = waitTime.ToString();
		countDownText.fontSize -= 3;
		counter += Time.deltaTime;

		if (counter >= 1f)
		{
			waitTime--;
			counter = 0f;
			countDownText.fontSize = 144;
		}
		if (waitTime == 0)
		{
			play = true;
			countDownText.text = "";
		}	
	}

	//function to reset text fields at the beggining of the game
	private void ResetTextFields()
	{
		valueText.text = "";
		eventText.text = "";
		fishDamageText.text = "100 %";
		hookDamageText.text = "100 %";
	}

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
			fullTime = 0f;
			interval = Random.Range(minRange, maxRange);
		}
	}

	//Function showing if fish is pulling the hook or not
	private void ShowFishEvent()
	{
		if (fishPull)
		{
			eventText.text = "Fish is pulling";
		}
		else
		{
			eventText.text = "Fish is not moving";
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
			hookDamage += damageSize * Time.deltaTime;
		}

		actualFishLife = 100f - Mathf.Round((fishDamage / fishLife) * 100);
		fishDamageText.text = actualFishLife.ToString() + " %";

		actualHookLife = 100f - Mathf.Round((hookDamage / hookLife) * 100);
		hookDamageText.text = actualHookLife.ToString() + " %";
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

	//function to controll healthbars
	private void ChangeHealthBarLengths()
	{
		Vector2 fishScale = new Vector2(actualFishLife / 100, 1f);
		fishHealthBar.transform.localScale = fishScale;

		Vector2 hookScale = new Vector2(actualHookLife / 100, 1f);
		hookHealthBar.transform.localScale = hookScale;
	}

	//function checking end of minigame
	private void CheckIfEndOfGame()
	{
		if (actualHookLife == 0 || actualFishLife == 0)
		{
			endOfGame = true;
			rb2d.velocity = new Vector2(0, 0);
			if (actualFishLife == 0)
			{
				ShowEndTexts("win");
			}
			if (actualHookLife == 0)
			{
				ShowEndTexts("lost");
			}
		}
	}

	//function to show texts at the end of game
	private void ShowEndTexts (string text)
	{
		valueText.text = "";
		eventText.text = "";
		countDownText.text = "You " + text + "!";
		countDownText.fontSize = 144;
	}
}
