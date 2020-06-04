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
                    InteractionKami(hitInfo);
                }

                if(hitInfo.transform.tag == "AmaterasuPuzzle1" && hitInfo.distance < 1)
                {
                    hitInfo.transform.GetChild(0).gameObject.SetActive(true);
                    if (hitInfo.transform.childCount == 2)
                        Destroy(hitInfo.transform.GetChild(1).gameObject);
                    hitInfo.transform.GetComponent<HintController>().enabled = false;
                }

            }
        }
    }

    private void InteractionKami(RaycastHit hitInfo)
    {
        StartCoroutine(WorldSwitcher.Instance.VisualizeSceneChange());
        WorldSwitcher.Instance.sendingKami = (WorldSwitcher.SendingKami)Enum.Parse(typeof(WorldSwitcher.SendingKami), hitInfo.transform.name);
    }

    private void InteractionObject()
    {

    }

}
