using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject[] levels;

	private bool onPlace;
	private bool picked;
	private Vector3 target;

	void Start () {
		onPlace = true;
		picked = false;
		Vector3 firstPosition = new Vector3(levels[0].transform.position.x, levels[0].transform.position.y, transform.position.z);
		transform.position = firstPosition;
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			onPlace = false;
			picked = true;
			target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}

		MoveCameraToThePoint();
	}

	void MoveCameraToThePoint()
	{
		if (!onPlace)
		{
			SetCameraPosition(new Vector3(target.x, target.y, transform.position.z));
		}
		if ((target.x == transform.position.x) && (target.y == transform.position.y))
			onPlace = true;
	}

	void SetCameraPosition(Vector3 newPosition)
	{
		transform.position = Vector3.Lerp(transform.position, newPosition, 0.1f);
	}

	float ChooseLevel(int level)
	{
		switch (level)
		{
			case 0:
				return -10f;
			case 1:
				return -20f;
			case 2:
				return -30f;
			case 3:
				return -40f;
			case 4:
				return -50f;
			case 5:
				return -60f;
			case 6:
				return -70f;
			default:
				return -100f;
		}
	}
}
