using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEngine;

public class LightningManager : MonoBehaviour
{

	public List<GameObject> lightnings = new List<GameObject>();
	private bool start = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (start == false)
		{
			StartCoroutine(startLightning());
		}
    }

	IEnumerator startLightning()
	{
		start = true;
		int range = Random.Range(0, lightnings.Count - 1);
		int time1 = Random.Range(1, 2);
		lightnings[range].SetActive(true);
		yield return new WaitForSeconds(2);
		lightnings[range].SetActive(false);
		start = false;
	}
}
