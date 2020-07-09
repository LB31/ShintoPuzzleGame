using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEngine;

public class LightningManager : MonoBehaviour
{

<<<<<<< HEAD
	private List<GameObject> lightnings = new List<GameObject>();
=======
	public List<GameObject> lightnings = new List<GameObject>();
>>>>>>> 6bb5fa3ddf81083a75dabc873a3c5158330b7384
	private bool start = false;

    // Start is called before the first frame update
    void Start()
    {
<<<<<<< HEAD
        for (int i = 0; i < transform.childCount; i++)
        {
			lightnings.Add(transform.GetChild(i).gameObject);
		}
=======
        
>>>>>>> 6bb5fa3ddf81083a75dabc873a3c5158330b7384
    }

    // Update is called once per frame
    void Update()
    {
		if (start == false)
		{
			StartCoroutine(startLightning());
		}
<<<<<<< HEAD
 
=======
>>>>>>> 6bb5fa3ddf81083a75dabc873a3c5158330b7384
    }

	IEnumerator startLightning()
	{
		start = true;
		int range = Random.Range(0, lightnings.Count - 1);
<<<<<<< HEAD
		int time1 = Random.Range(1, 2);
		lightnings[range].SetActive(true);
		yield return new WaitForSeconds(1);
=======
		float time1 = Random.Range(1.5f, 2);
		lightnings[range].SetActive(true);
		yield return new WaitForSeconds(time1);
>>>>>>> 6bb5fa3ddf81083a75dabc873a3c5158330b7384
		lightnings[range].SetActive(false);
		start = false;
	}
}
