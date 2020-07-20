﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleManagerPancake : MonoBehaviour
{
    public List<Transform> Plates;

    public List<Transform> AllPancakes;
    public List<Vector3> OriginPancakePosition;

    private Vector3 lastPancakePos;

    private string collectableName = "Pancake";

    private int selectedPancake = -1;
    private float selectedPancakeZ;



    // Puzzle Information
    public GameObject TaskPanel;

    void Start()
    {
        AssignSelectablePancakes();
    }


    private void Update()
    {
        // select element
        if (Input.GetMouseButtonDown(0))
        {
            GrabElement();
        }

        // drag element
        if (Input.GetMouseButton(0))
        {
            DragElement();
        }

        // release element
        if (Input.GetMouseButtonUp(0))
        {
            ReleaseElement();
        }
    }

    private void GrabElement()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = Physics.RaycastAll(ray).FirstOrDefault(element => element.transform.name.Contains(collectableName));

        if (!hit.transform || hit.transform.tag != "selectable") return;
        //Debug.Log(hit.transform.name, hit.transform);

        selectedPancake = AllPancakes.IndexOf(hit.transform);
        selectedPancakeZ = AllPancakes[selectedPancake].position.z;

        lastPancakePos = AllPancakes[selectedPancake].position;

        AllPancakes[selectedPancake].GetComponent<Rigidbody>().useGravity = false;
    }

    private void DragElement()
    {
        if (selectedPancake == -1) return;
        Vector3 mp = Input.mousePosition;
        mp.z = 1.55f;
        // change the offset for fat fingers
        if (GameManager.Instance.Mobile)
            mp.y += 100;

        float offsetTable = 0.02f;
        Debug.DrawLine(Camera.main.transform.position, Camera.main.ScreenToWorldPoint(mp), Color.green);
        if (AllPancakes[selectedPancake].localPosition.z > offsetTable)
        {
            AllPancakes[selectedPancake].position = Camera.main.ScreenToWorldPoint(mp);
        }
        else
        {
            // Stop the pancake from being under the table
            Vector3 curPos = AllPancakes[selectedPancake].localPosition;
            AllPancakes[selectedPancake].localPosition = new Vector3(curPos.x, curPos.y, curPos.z + offsetTable);
        }
    }

    private void ReleaseElement()
    {
        if (selectedPancake == -1) return;
        if (SnapToPlate())
        {
            Rigidbody rg = AllPancakes[selectedPancake].GetComponent<Rigidbody>();
            rg.useGravity = true;
            rg.isKinematic = false;
        }
        else
        {
            AllPancakes[selectedPancake].position = lastPancakePos;
        }

        selectedPancake = -1;


        //// return to origin position (e.g. apple tree)
        //if (!SnapToBasket())
        //{
        //    if (selectedPancake != AllPancakes.Count - 1)
        //        AllPancakes[selectedPancake].parent = AppleTree;
        //    else
        //        AllPancakes[selectedPancake].parent = Plates[0].parent;
        //    AllPancakes[selectedPancake].localScale = OriginAppleScale[selectedPancake];
        //    AllPancakes[selectedPancake].position = OriginPancakePosition[selectedPancake];

        //}


        //if (CheckIfPuzzleSolved())
        //{
        //    PlayMakerFSM.BroadcastEvent("PuzzleSolved");
        //}
    }

    private bool SnapToPlate()
    {
        List<Collider> hitColliders = Physics.OverlapSphere(AllPancakes[selectedPancake].position, 0.05f).ToList();
        hitColliders.Remove(AllPancakes[selectedPancake].GetComponent<Collider>());
        if (hitColliders.Any(b => b.name.Contains("Plate"))) // if a basket was triggered / entered
        {
            Transform plate = hitColliders.First(item => item.name.Contains("Plate")).transform;
            if (!CheckIfPlaceable(plate)) return false;
            // set plate as parent
            AllPancakes[selectedPancake].parent = plate;
            AssignSelectablePancakes();
            return true;
        }
        return false;
    }

    private bool CheckIfPlaceable(Transform plate)
    {
        if (plate.childCount == 0) return true;
        Transform lastChild = plate.GetChild(plate.childCount - 1);
        int childNumber = int.Parse(Regex.Match(lastChild.name, @"\d+").Value);
        if (childNumber < selectedPancake) return true;
        return false;
    }

    private void AssignSelectablePancakes()
    {
        foreach (Transform plate in Plates)
        {
            foreach (Transform child in plate)
            {
                child.tag = "Untagged";
            }
            if (plate.childCount > 0)
                plate.GetChild(plate.childCount - 1).tag = "selectable";
        }
    }

    [ContextMenu("Fill Fields")]
    public void FillFields()
    {
        AllPancakes.Clear();
        OriginPancakePosition.Clear();
        foreach (Transform child in Plates[0])
        {
            AllPancakes.Add(child);
            OriginPancakePosition.Add(child.position);
        }
    }

    private bool CheckIfPuzzleSolved()
    {
        Transform[] basketChildren;

        // Big check
        basketChildren = Plates[0].GetComponentsInChildren<Transform>();
        bool checkBig1 = basketChildren.Where(child => child.name.Contains(collectableName)).Count() == 3 && basketChildren.Any(child => child.name.Contains("Small"));
        bool checkBig2 = basketChildren.Where(child => child.name.Contains(collectableName)).Count() == 3;
        // Middle check
        basketChildren = Plates[1].GetComponentsInChildren<Transform>();
        bool checkMiddle1 = basketChildren.Where(child => child.name.Contains(collectableName)).Count() == 2;
        bool checkMiddle2 = basketChildren.Where(child => child.name.Contains(collectableName)).Count() == 2 && basketChildren.Any(child => child.name.Contains("Small"));
        // Small check
        basketChildren = Plates[2].GetComponentsInChildren<Transform>();
        bool checkSmall = basketChildren.Where(child => child.name.Contains(collectableName)).Count() == 1;

        return ((checkBig1 && checkMiddle1) || (checkBig2 && checkMiddle2)) && checkSmall;
    }


    public void TriggerPuzzleInfo()
    {
        if (TaskPanel.activeSelf)
            TaskPanel.SetActive(false);
        else
            TaskPanel.SetActive(true);
    }
}
