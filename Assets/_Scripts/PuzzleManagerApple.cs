using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PuzzleManagerApple : MonoBehaviour
{
    public List<Transform> Baskets;
    public Transform AppleTree;

    public List<Transform> AllApples;
    public List<Vector3> OriginAppleScale;
    public List<Vector3> OriginApplePosition;

    private int selectedApple = -1;

    void Start()
    {

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

            RaycastHit hit = Physics.RaycastAll(ray).
                FirstOrDefault(element => element.transform.name.Contains("Apple") || element.transform.name.Contains("Small"));
            if (!hit.transform) return;
            Debug.Log(hit.transform.name, hit.transform);
            if (hit.transform.name.Contains("Apple"))
            {
                if (hit.transform.parent == AppleTree)
                    hit.transform.localScale = Vector3.one * 0.5f;
            }
            else
            {
                // TODO move basket properly
                hit.transform.rotation = Baskets[0].rotation;
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
                mp.y += 50;

            Debug.DrawLine(Camera.main.transform.position, Camera.main.ScreenToWorldPoint(mp), Color.green);
            if (selectedApple != -1)
                AllApples[selectedApple].position = Camera.main.ScreenToWorldPoint(mp);
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
        }
    }

    private bool SnapToBasket()
    {
        List<Collider> hitColliders = Physics.OverlapSphere(AllApples[selectedApple].position, 0.05f).ToList();
        hitColliders.Remove(AllApples[selectedApple].GetComponent<Collider>());
        if (hitColliders.Any(b => b.name.Contains("Straw"))) // if basket was triggered
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
        // Handle small basket
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
}
