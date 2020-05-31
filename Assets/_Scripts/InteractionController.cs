using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [SerializeField]
    private GameObject canvas;
    [SerializeField]
    private GameObject playerCamera;
    [SerializeField]
    private TMP_InputField inputField;

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
                    print(hitInfo.transform.name);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            canvas.SetActive(true);
            playerCamera.SetActive(false);
            inputField.ActivateInputField();
        }
    }

}
