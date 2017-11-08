using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectController : MonoBehaviour {

	private bool onPlace;
	private Vector3 target;

	private void Start()
	{
		onPlace = true;
	}

	void Update () {
		MoveCameraToThePoint();
	}

	private void OnMouseDown()
	{
		onPlace = false;
		target = transform.position;
	}


	//function checking where it should move
	void MoveCameraToThePoint()
	{
		if (!onPlace)
		{
			SetCameraPosition(new Vector3(target.x, target.y, Camera.main.transform.position.z));
			if ((target.x == Camera.main.transform.position.x) && (target.y == Camera.main.transform.position.y))
				onPlace = true;
		}
	}

	//function to set camera position
	void SetCameraPosition(Vector3 newPosition)
	{
		Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, newPosition, 0.1f);
	}
}
