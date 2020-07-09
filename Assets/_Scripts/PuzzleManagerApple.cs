using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleManagerApple : MonoBehaviour
{
    public List<Transform> Baskets;
    public Transform AppleTree;

    public List<Transform> AllApples;
    public List<Vector3> OriginAppleScale;
    public List<Vector3> OriginApplePosition;

    private int selectedApple = -1;

    // for moving of the small basket
    private float distanceToBigBasket;
    private Quaternion originSmallRotation;

    // Puzzle Information
    public GameObject TaskPanel;

    void Start()
    {
        distanceToBigBasket = Vector3.Distance(Baskets[0].position, Baskets[2].position);
        originSmallRotation = Baskets[2].rotation;

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

            if (hit.transform.name.Contains("Apple"))
            {
                if (hit.transform.parent == AppleTree)
                    hit.transform.localScale = Vector3.one * 0.5f;
            }

            selectedApple = AllApples.IndexOf(hit.transform);
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
            if (selectedApple != -1)
            {
                AllApples[selectedApple].position = Camera.main.ScreenToWorldPoint(mp);
                // if the basket is moved
                if (AllApples[selectedApple].name.Contains("Small"))
                {
                    float distance = Vector3.Distance(AllApples[selectedApple].position, Baskets[0].position);
                    float f = distance / distanceToBigBasket;
                    float t = 1.0f - f;
                    AllApples[selectedApple].rotation = Quaternion.Lerp(Baskets[0].rotation, originSmallRotation, f);
                }
            }
                
        }

        // release apple
        if (Input.GetMouseButtonUp(0))
        {
            if (selectedApple == -1) return;

            // return to origin position (e.g. apple tree)
            if (!SnapToBasket())
            {
                if (selectedApple != AllApples.Count - 1)
                    AllApples[selectedApple].parent = AppleTree;
                else
                    AllApples[selectedApple].parent = Baskets[0].parent;
                AllApples[selectedApple].localScale = OriginAppleScale[selectedApple];
                AllApples[selectedApple].position = OriginApplePosition[selectedApple];

            }
            selectedApple = -1;

            if (CheckIfPuzzleSolved())
            {
                PlayMakerFSM.BroadcastEvent("PuzzleSolved");
            }

        }
    }

    private bool SnapToBasket()
    {
        List<Collider> hitColliders = Physics.OverlapSphere(AllApples[selectedApple].position, 0.05f).ToList();
        hitColliders.Remove(AllApples[selectedApple].GetComponent<Collider>());
        if (hitColliders.Any(b => b.name.Contains("Straw"))) // if a basket was triggered / entered
        {
            // set basket as parent
            AllApples[selectedApple].parent = hitColliders.First(item => item.name.Contains("Straw")).transform;
            return true;

        }

        return false;
    }

    [ContextMenu("Fill Fields")]
    public void FillFields()
    {
        // Handle small basket as an apple
        Transform outsider = GameObject.Find("StrawBasketSmall").transform;
        outsider.parent = AppleTree;

        AllApples.Clear();
        OriginAppleScale.Clear();
        OriginApplePosition.Clear();
        foreach (Transform child in AppleTree)
        {
            AllApples.Add(child);
            OriginAppleScale.Add(child.localScale);
            OriginApplePosition.Add(child.position);
        }

        outsider.parent = GameObject.Find("BasketParent").transform;
    }

    private bool CheckIfPuzzleSolved()
    {
        Transform[] basketChildren;
        // Big check
        basketChildren = Baskets[0].GetComponentsInChildren<Transform>();
        bool checkBig = basketChildren.Where(child => child.name.Contains("Apple")).Count() == 3 && basketChildren.Any(child => child.name.Contains("Small"));
        // Middle check
        basketChildren = Baskets[1].GetComponentsInChildren<Transform>();
        bool checkMiddle = basketChildren.Where(child => child.name.Contains("Apple")).Count() == 2;
        // Small check
        basketChildren = Baskets[2].GetComponentsInChildren<Transform>();
        bool checkSmall = basketChildren.Where(child => child.name.Contains("Apple")).Count() == 1;

        return checkBig && checkMiddle && checkSmall;
    }


    public void TriggerPuzzleInfo()
    {
        if (TaskPanel.activeSelf)
            TaskPanel.SetActive(false);
        else
            TaskPanel.SetActive(true);
    }
}
