using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour {

	public float velocity;
	public Side facingSide;

	public enum Side {
		left = -1,
		right = 1
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position += new Vector3(velocity*(float)facingSide*Time.deltaTime, 0,0);
		if (this.transform.position.x > Screen.width + 10 || this.transform.position.x < -10) {
			this.gameObject.SetActive(false);
		}
	}
}
