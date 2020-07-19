using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PuzzleManagerKappa : MonoBehaviour
{
    [SerializeField]
    public GameObject bigBucket;
    [SerializeField]
    public GameObject mediumBucket;
    [SerializeField]
    public GameObject smallBucket;

    private GameObject selectedBucket = null;
    private bool bucketIsSelected = false;
    private List<GameObject> waterInBigBucket = new List<GameObject>();
    private List<GameObject> waterInMediumBucket = new List<GameObject>();
    private List<GameObject> waterInSmallBucket = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        /* Initialize water in bucket
        foreach(Transform child in bigBucket.transform)
        {
            waterInBigBucket.Add(child.gameObject);
        }

        foreach (Transform child in mediumBucket.transform)
        {
            waterInMediumBucket.Add(child.gameObject);
        }

        foreach (Transform child in smallBucket.transform)
        {
            waterInSmallBucket.Add(child.gameObject);
        }
        */
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
                    Debug.Log("Do Puzzle Stuff");
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
        if(GetWaterAmount(selectedBucket).Count == 0)
        {
            return;
        }

    }

    private List<GameObject> GetWaterAmount(GameObject bucket)
    {
        if(bucket == bigBucket)
        {
            foreach (Transform water in bucket.transform)
            {
                waterInBigBucket.Add(water.gameObject);
            }
            return waterInBigBucket;
        }

        if (bucket == mediumBucket)
        {
            foreach (Transform water in bucket.transform)
            {
                waterInMediumBucket.Add(water.gameObject);
            }
            return waterInMediumBucket;
        }

        if (bucket == smallBucket)
        {
            foreach (Transform water in bucket.transform)
            {
                waterInSmallBucket.Add(water.gameObject);
            }
            return waterInSmallBucket;
        }
        return null;
    }
}
