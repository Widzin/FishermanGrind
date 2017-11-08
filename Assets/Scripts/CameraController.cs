using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject[] levels;

	private bool onPlace;
	private Vector3 target;

	void Start () {
		onPlace = true;
		Vector3 firstPosition = new Vector3(levels[0].transform.position.x, levels[0].transform.position.y, transform.position.z);
		transform.position = firstPosition;
	}
}
