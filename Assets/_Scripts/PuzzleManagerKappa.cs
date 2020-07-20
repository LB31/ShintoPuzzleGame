using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PuzzleManagerKappa : MonoBehaviour
{
    private GameObject selectedBucket = null;
    private bool bucketIsSelected = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SelectBucket();
    }

    private void SelectBucket()
    {
        // select bucket
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = Physics.RaycastAll(ray).
                FirstOrDefault(element => element.transform.name.Contains("Bucket"));

            if (!hit.transform) return;
           
            if (hit.transform.name.Contains("Bucket"))
            {
                if (selectedBucket != hit.transform.gameObject && bucketIsSelected)
                {
                    ChooseSecondBucket(hit.transform.gameObject);
                    //Debug.Log("Do Puzzle Stuff");
                    selectedBucket = null;
                    bucketIsSelected = false;
                }
                //select the bucket if it wasnt already selected
                else if(selectedBucket != hit.transform.gameObject && !bucketIsSelected)
                {
                    selectedBucket = hit.transform.gameObject;
                    bucketIsSelected = true;
                }
                //checks if bucket has already been selected
                else if (selectedBucket == hit.transform.gameObject && bucketIsSelected)
                {
                    //deselect the bucket
                    selectedBucket = null;
                    bucketIsSelected = false;
                }
            }
            Debug.Log(bucketIsSelected);
            if (selectedBucket != null)
                Debug.Log(selectedBucket.name);
        }
    }

    private void ChooseSecondBucket(GameObject secondBucket)
    {
        Bucket bucketFirst = selectedBucket.GetComponent<Bucket>();
        Bucket bucketSecond = secondBucket.GetComponent<Bucket>();

        int bucketFirstAmount = bucketFirst.GetWaterAmount();
        int bucketSecondAmount = bucketSecond.GetWaterAmount();

        while(!bucketFirst.IsEmpty() && !bucketSecond.IsFull())
        {
            bucketFirst.PopWater();
            bucketSecond.PushWater();
        }
    }
}
