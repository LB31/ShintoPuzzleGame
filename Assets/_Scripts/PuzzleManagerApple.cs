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

            RaycastHit hit = Physics.RaycastAll(ray).FirstOrDefault(element => element.transform.name.Contains("Apple"));
            if (!hit.transform) return;
            hit.transform.localScale = Vector3.one * 0.5f;
            selectedApple = AllApples.IndexOf(hit.transform);
        }

        // drag apple
        if (Input.GetMouseButton(0))
        {
            
            Vector3 mp = Input.mousePosition;
            mp.z = 2f;
            // change the offset for fat fingers
            if (GameManager.Instance.Mobile) 
                mp.y += 30; 

            Debug.DrawLine(Camera.main.transform.position, Camera.main.ScreenToWorldPoint(mp), Color.green);
            if (selectedApple != -1)
                AllApples[selectedApple].position = Camera.main.ScreenToWorldPoint(mp);
        }

        // release apple
        if (Input.GetMouseButtonUp(0))
        {
            if (selectedApple == -1) return;

            if (!SnapToBasket())
            {   
                AllApples[selectedApple].localScale = OriginAppleScale[selectedApple];
                AllApples[selectedApple].position = OriginApplePosition[selectedApple];
            }
            selectedApple = -1;
        }
    }

    private bool SnapToBasket()
    {
        Collider[] hitColliders = Physics.OverlapSphere(AllApples[selectedApple].position, 0.05f);
        if (hitColliders.Any(b => b.name.Contains("Straw")))
        {
            return true;
        }
        return false;
    }

    [ContextMenu("Fill Fields")]
    public void FillFields()
    {
        AllApples.Clear();
        OriginAppleScale.Clear();
        OriginApplePosition.Clear();
        foreach (Transform child in AppleTree)
        {
            AllApples.Add(child);
            OriginAppleScale.Add(child.localScale);
            OriginApplePosition.Add(child.position);
        }
    }
}
