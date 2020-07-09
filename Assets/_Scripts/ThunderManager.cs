using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEngine;

public class ThunderManager : MonoBehaviour
{
	public List<GameObject> thunders = new List<GameObject>();
	private bool start = false;
	float timeDelay;

	// Start is called before the first frame update
	void Start()
	{

		for (int i = 0; i < transform.childCount; i++)
		{
			thunders.Add(transform.GetChild(i).gameObject);
		}

	}

	// Update is called once per frame
	void Update()
	{

		if (!start)
		{
			StartCoroutine(startLightning());
		}
	}

	IEnumerator startLightning()
	{
		start = true;
		int range = Random.Range(0, thunders.Count - 1);
		thunders[range].GetComponent<Light>().enabled = true;
		timeDelay = Random.Range(0.1f, 0.2f);
		yield return new WaitForSeconds(timeDelay);
		thunders[range].GetComponent<Light>().enabled = false;
		timeDelay = Random.Range(0.1f, 0.2f);
		yield return new WaitForSeconds(timeDelay);
		start = false;
		yield return new WaitForSeconds(1);

	}
}
