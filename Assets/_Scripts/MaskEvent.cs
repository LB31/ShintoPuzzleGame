using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;
using UnityEngine.VFX;

public class MaskEvent : MonoBehaviour
{
	public Material maskShader;
	public GameObject particleMask;
	public GameObject explosion;
	public GameObject dissapearMask;
	private float dissolveFloat;
	private float time = 0;
	private float time1 = 0;
	private float time2 = 0;
	private float time3 = 0;

	float parameter2 = 1;

	// Start is called before the first frame update
	void Start()
	{
		dissolveFloat = -1;
		maskShader.SetFloat("Vector1_5F648E00", dissolveFloat);

	}

	// Update is called once per frame
	void Update()
	{

		if (Input.GetKey("up"))
		{
			dissolveFloat = Mathf.Lerp(-1, 1, time);
			time += 0.3f * Time.deltaTime;
			maskShader.SetFloat("Vector1_5F648E00", dissolveFloat);
		}
		if (dissolveFloat >= 1)
		{
			particleMask.SetActive(true);
		}

		if (particleMask.activeSelf == true)
		{
			float parameter = 1;
			parameter = Mathf.Lerp(1, 10, time1);
			time1 += 0.1f * Time.deltaTime;
			particleMask.GetComponent<VisualEffect>().SetFloat("particleSpeed", parameter);
		}
		if (Input.GetKey("down"))
		{
			parameter2 = Mathf.Lerp(1, 25, time2);
			time2 += 0.2f * Time.deltaTime;
			particleMask.GetComponent<VisualEffect>().SetFloat("y-Position", parameter2);
		}
		if (parameter2 >= 25)
		{
			time3 +=  1 * Time.deltaTime;
			dissapearMask.SetActive(true);
			particleMask.SetActive(false);
			if (time3 >= 4)
			{
				explosion.SetActive(true);
			}
		}
	}
}
