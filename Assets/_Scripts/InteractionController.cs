using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                print("HIT");
                Rigidbody rg = hitInfo.collider.GetComponent<Rigidbody>();
                if (rg)
                {
                    rg.AddForce(new Vector3(1000f, 0F, 0f));
                    print(hitInfo.transform.name);
                }
            }
        }
    }

}
