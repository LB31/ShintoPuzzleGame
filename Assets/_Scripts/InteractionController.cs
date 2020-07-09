using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [SerializeField]
    private GameObject canvasPuzzle;
    [SerializeField]
    private GameObject playerCamera;
    [SerializeField]
    private TMP_InputField inputField;
    [SerializeField]
    private GameObject canvasDialog;

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
        if (Input.GetKeyDown(KeyCode.E))
        {
            canvasPuzzle.SetActive(true);
            playerCamera.SetActive(false);
            inputField.ActivateInputField();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            canvasDialog.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            this.GetComponent<PuzzleManager>().StartDialog();
        }
    }

    private void InteractionKami(RaycastHit hitInfo)
    {
        StartCoroutine(WorldSwitcher.Instance.VisualizeSceneChange(true));
        WorldSwitcher.Instance.sendingKami = (WorldSwitcher.SendingKami)Enum.Parse(typeof(WorldSwitcher.SendingKami), hitInfo.transform.name);
    }

    private void InteractionObject()
    {

    }

}
