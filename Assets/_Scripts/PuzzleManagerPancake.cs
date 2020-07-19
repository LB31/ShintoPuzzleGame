using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleManagerPancake : MonoBehaviour
{
    public List<Transform> Plates;

    public List<Transform> AllPancakes;
    public List<Vector3> OriginPancakePosition;

    private int selectedPancake = -1;

    // for moving of the small basket
    private float distanceToBigBasket;
    private Quaternion originSmallRotation;

    // Puzzle Information
    public GameObject TaskPanel;

    void Start()
    {
        distanceToBigBasket = Vector3.Distance(Plates[0].position, Plates[2].position);
        originSmallRotation = Plates[2].rotation;

        TaskPanel.transform.parent.gameObject.SetActive(false);
    }


    void Update()
    {
        DragObject();
    }

    private void DragObject()
    {
        // select apple
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = Physics.RaycastAll(ray). // TODO maybe select all baskets
                FirstOrDefault(element => element.transform.name.Contains("Apple") || element.transform.name.Contains("Small"));

            if (!hit.transform) return;
            //Debug.Log(hit.transform.name, hit.transform);


            selectedPancake = AllPancakes.IndexOf(hit.transform);
        }

        // drag apple
        if (Input.GetMouseButton(0))
        {
            Vector3 mp = Input.mousePosition;
            mp.z = 2f;
            // change the offset for fat fingers
            if (GameManager.Instance.Mobile)
                mp.y += 100;

            //Debug.DrawLine(Camera.main.transform.position, Camera.main.ScreenToWorldPoint(mp), Color.green);
            if (selectedPancake != -1)
            {
                AllPancakes[selectedPancake].position = Camera.main.ScreenToWorldPoint(mp);
                // if the basket is moved
                if (AllPancakes[selectedPancake].name.Contains("Small"))
                {
                    float distance = Vector3.Distance(AllPancakes[selectedPancake].position, Plates[0].position);
                    float f = distance / distanceToBigBasket;
                    float t = 1.0f - f;
                    AllPancakes[selectedPancake].rotation = Quaternion.Lerp(Plates[0].rotation, originSmallRotation, f);
                }
            }
                
        }

        // release apple
        if (Input.GetMouseButtonUp(0))
        {
            //if (selectedPancake == -1) return;

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
            //selectedPancake = -1;

            //if (CheckIfPuzzleSolved())
            //{
            //    PlayMakerFSM.BroadcastEvent("PuzzleSolved");
            //}

        }
    }

    private bool SnapToBasket()
    {
        List<Collider> hitColliders = Physics.OverlapSphere(AllPancakes[selectedPancake].position, 0.05f).ToList();
        hitColliders.Remove(AllPancakes[selectedPancake].GetComponent<Collider>());
        if (hitColliders.Any(b => b.name.Contains("Straw"))) // if a basket was triggered / entered
        {
            // set basket as parent
            AllPancakes[selectedPancake].parent = hitColliders.First(item => item.name.Contains("Straw")).transform;
            return true;

        }

        return false;
    }

    [ContextMenu("Fill Fields")]
    public void FillFields()
    {
        //// Handle small basket as an apple
        //Transform outsider = GameObject.Find("StrawBasketSmall").transform;
        //outsider.parent = AppleTree;

        //AllPancakes.Clear();
        //OriginAppleScale.Clear();
        //OriginPancakePosition.Clear();
        //foreach (Transform child in AppleTree)
        //{
        //    AllPancakes.Add(child);
        //    OriginAppleScale.Add(child.localScale);
        //    OriginPancakePosition.Add(child.position);
        //}

        //outsider.parent = GameObject.Find("BasketParent").transform;
    }

    private bool CheckIfPuzzleSolved()
    {
        Transform[] basketChildren;

        // Big check
        basketChildren = Plates[0].GetComponentsInChildren<Transform>();
        bool checkBig1 = basketChildren.Where(child => child.name.Contains("Apple")).Count() == 3 && basketChildren.Any(child => child.name.Contains("Small"));
        bool checkBig2 = basketChildren.Where(child => child.name.Contains("Apple")).Count() == 3;
        // Middle check
        basketChildren = Plates[1].GetComponentsInChildren<Transform>();
        bool checkMiddle1 = basketChildren.Where(child => child.name.Contains("Apple")).Count() == 2;
        bool checkMiddle2 = basketChildren.Where(child => child.name.Contains("Apple")).Count() == 2 && basketChildren.Any(child => child.name.Contains("Small"));
        // Small check
        basketChildren = Plates[2].GetComponentsInChildren<Transform>();
        bool checkSmall = basketChildren.Where(child => child.name.Contains("Apple")).Count() == 1;

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
