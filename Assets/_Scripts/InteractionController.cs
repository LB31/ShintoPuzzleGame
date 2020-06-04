using System;
using System.Collections;
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
                    StartCoroutine(WorldSwitcher.Instance.VisualizeSceneChange());
                    WorldSwitcher.Instance.sendingKami = (WorldSwitcher.SendingKami)Enum.Parse(typeof(WorldSwitcher.SendingKami), hitInfo.transform.name);
                }
            }
        }
    }

}
