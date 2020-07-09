using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
	public float speedH = 2f;
	public float speedV = 2f;
	private float yaw = 0.0f;
	private float pitch = 0.0f;


	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		yaw += speedH * Input.GetAxis("Horizontal");
		pitch -= speedV * Input.GetAxis("Vertical");
		transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
	}
}
