﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSzmbol : MonoBehaviour
{
	public float speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		transform.Rotate(transform.rotation.y - 1 * speed * Time.deltaTime, 0, 0);
    }
}
