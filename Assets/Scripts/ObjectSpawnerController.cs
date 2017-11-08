using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnerController : MonoBehaviour {

	public float generationInterval;

	public float maxFishX;
	public float maxFishY;

	public float maxRockX;
	public float maxRockY;

	public float maxBushX;
	public float maxBushY;

	public float maxFishSpeed;
	public float minFishSpeed;

	public float specialFishProbability;
	public float obstacleProbability;
	public float fishProbability;

	public GameObject fishPrefab;
	public GameObject obstaclePrefab;
	private IEnumerator coroutine;
	private Boolean over = false;
	
	void Start () {
		coroutine = WaitAndGenerate(generationInterval);
	}
	

	void Update () {
		StartCoroutine(coroutine);
	}

	private IEnumerator WaitAndGenerate(float generationInterval) {
		while (!over) {
			yield return new WaitForSeconds(generationInterval);
			GenerateRandomObject();
			over = true;
		}
	}

	private GameObject GenerateRandomObject() {
		GameObject generated = GameObject.Instantiate(fishPrefab, new Vector3(0, 0, 0), Quaternion.identity);
		return generated;
	}
}
