using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowEffectGhosts : MonoBehaviour
{
	public Material mat;
	public Color color;
	public float timeMultiplyer;
	public float maxIntence;
	public float minIntence;
    // Start is called before the first frame update
    void Start()
    {
		
	}

    // Update is called once per frame
    void Update()
    {
		mat.SetColor("Color_D53729AC", color * (Mathf.PingPong(Time.time * timeMultiplyer, maxIntence) + minIntence));
	}
}
