﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                Rigidbody rg = hitInfo.collider.GetComponent<Rigidbody>();
                if (rg && hitInfo.distance < 2)
                {
                    rg.AddForce(new Vector3(1000f, 0F, 0f));
                    WorldSwitcher.Instance.SwitchWorld();
                    print(hitInfo.transform.name);
                }
            }
        }
    }

}
