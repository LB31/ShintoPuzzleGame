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

    // Start is called before the first frame update
    void Start()
    {
        
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
        if (CheckWinCondition())
        {
            Debug.Log("WON!!");
            PlayMakerFSM.BroadcastEvent("PuzzleSolved");
        }
    }

    private void UpdateWaterAmount()
    {
        var bigPanel = this.transform.Find("PuzzleUi/WaterAmountPanel/BigPanel/Text");
        var mediumPanel = this.transform.Find("PuzzleUi/WaterAmountPanel/MediumPanel/Text");
        var smallPanel = this.transform.Find("PuzzleUi/WaterAmountPanel/SmallPanel/Text");

        bigPanel.GetComponent<TMP_Text>().text = bigBucket.GetWaterAmount().ToString();
        mediumPanel.GetComponent<TMP_Text>().text = mediumBucket.GetWaterAmount().ToString();
        smallPanel.GetComponent<TMP_Text>().text = smallBucket.GetWaterAmount().ToString();
    }

    private bool CheckWinCondition()
    {
        //ONLY FOR DEBUG
        if (mediumBucket.GetWaterAmount() == 9)
        {
            return true;
        }
        return false;

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
}
