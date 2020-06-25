using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEngine;

public class LightningManager : MonoBehaviour
{

	private List<GameObject> lightnings = new List<GameObject>();
	private bool start = false;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
			lightnings.Add(transform.GetChild(i).gameObject);
		}
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
		yield return new WaitForSeconds(1);
		lightnings[range].SetActive(false);
		start = false;
	}
}
