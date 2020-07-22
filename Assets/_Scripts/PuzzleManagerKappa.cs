using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PuzzleManagerKappa : MonoBehaviour
{
    private GameObject selectedBucket = null;
    private bool bucketIsSelected = false;
    [SerializeField]
    private Bucket bigBucket;
    [SerializeField]
    private Bucket mediumBucket;
    [SerializeField]
    private Bucket smallBucket;

    private GameObject bigPanel;
    private GameObject mediumPanel;
    private GameObject smallPanel;
  

    // Start is called before the first frame update
    void Start()
    {
        bigPanel = this.transform.Find("PuzzleUi/WaterAmountPanel/BigPanel/Text").gameObject;
        mediumPanel = this.transform.Find("PuzzleUi/WaterAmountPanel/MediumPanel/Text").gameObject;
        smallPanel = this.transform.Find("PuzzleUi/WaterAmountPanel/SmallPanel/Text").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        SelectBucket();
        UpdateWaterAmount();
    }

    private void SelectBucket()
    {
       
        // select bucket
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = Physics.RaycastAll(ray).
                FirstOrDefault(element => element.transform.name.Contains("sara"));

            if (!hit.transform) return;
           
            if (hit.transform.name.Contains("sara"))
            {
                //select second bucket
                if (selectedBucket != hit.transform.gameObject && bucketIsSelected)
                {
                    ChooseSecondBucket(hit.transform.gameObject);
                    //Debug.Log("Do Puzzle Stuff");
                    selectedBucket.GetComponent<Bucket>().Activated(false);
                    selectedBucket = null;
                    bucketIsSelected = false;
                }
                //select the bucket if it wasnt already selected
                else if(selectedBucket != hit.transform.gameObject && !bucketIsSelected)
                {
                    selectedBucket = hit.transform.gameObject;
                    selectedBucket.GetComponent<Bucket>().Activated(true);
                    bucketIsSelected = true;
                }
                //checks if bucket has already been selected
                else if (selectedBucket == hit.transform.gameObject && bucketIsSelected)
                {
                    selectedBucket.GetComponent<Bucket>().Activated(false);
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

        float temp = 1;

        while(!bucketFirst.IsEmpty() && !bucketSecond.IsFull())
        {
            bucketFirst.PopWater();
            bucketSecond.PushWater(temp);

            temp += 0.05f;
        }
        if (CheckWinCondition())
        {
            Debug.Log("WON!!");
            PlayMakerFSM.BroadcastEvent("PuzzleSolved");
        }
    }

    private void UpdateWaterAmount()
    {
        //TODO: add maxAmount
        string bigBucketText = bigBucket.GetWaterAmount().ToString() + "/" + bigBucket.maxWaterAmount;
        string mediumBucketText = mediumBucket.GetWaterAmount().ToString() + "/" + mediumBucket.maxWaterAmount;
        string smallBucketText = smallBucket.GetWaterAmount().ToString() + "/" + smallBucket.maxWaterAmount;

        bigPanel.GetComponent<TMP_Text>().text = bigBucketText;
        mediumPanel.GetComponent<TMP_Text>().text = mediumBucketText;
        smallPanel.GetComponent<TMP_Text>().text = smallBucketText;
    }

    private bool CheckWinCondition()
    {
        //ONLY FOR DEBUG
        
        if (mediumBucket.GetWaterAmount() == 9)
        {
            return true;
        }
        return false;
        
        
        //The real thing
        /*
        if (bigBucket.GetWaterAmount() == 8 && mediumBucket.GetWaterAmount() == 8)
        {
            return true;
        }
        return false;
        */
    }
    public void PuzzleDescription(GameManager.KamiType selectedKami)
    {
        var taskPanel = this.transform.Find("PuzzleUi/TaskPanel/Text");
        var textComponent = taskPanel.GetComponent<TMP_Text>();
        //Debug.Log(selectedKami);
        textComponent.text = GameManager.Instance.kamis[(int)selectedKami].puzzleText;
    }

    public void TriggerPuzzleInfo()
    {
        var taskPanel = this.transform.Find("PuzzleUi/TaskPanel").gameObject;
        var bucketPanel = this.transform.Find("PuzzleUi/WaterAmountPanel").gameObject;
        if (taskPanel.activeSelf)
        {
            taskPanel.SetActive(false);
            bucketPanel.SetActive(true);
        }
        else
        {
            taskPanel.SetActive(true);
            bucketPanel.SetActive(false);
        }
    }

    public void TriggerPuzzleScene(bool isActive)
    {
        var kappaBigHead = this.transform.Find("kappaBig/Kappa/Head/sara").gameObject;
        var kappaMediumHead = this.transform.Find("kappaMiddle/Kappa/Head/sara").gameObject;
        var kappaSmallHead = this.transform.Find("kappaSmall/Kappa/Head/sara").gameObject;

        var kappaBigHeadPuzzle = this.transform.Find("kappaBig/Kappa/Head/saraBig").gameObject;
        var kappaMediumHeadPuzzle = this.transform.Find("kappaMiddle/Kappa/Head/saraMedium").gameObject;
        var kappaSmallHeadPuzzle = this.transform.Find("kappaSmall/Kappa/Head/saraSmall").gameObject;

        kappaBigHead.SetActive(!isActive);
        kappaMediumHead.SetActive(!isActive);
        kappaSmallHead.SetActive(!isActive);

        kappaBigHeadPuzzle.SetActive(isActive);
        kappaMediumHeadPuzzle.SetActive(isActive);
        kappaSmallHeadPuzzle.SetActive(isActive);
    }
}
