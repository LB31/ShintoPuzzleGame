using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PuzzleManagerApple : MonoBehaviour
{
    public List<Transform> Baskets;

    private Transform selectedApple;
    private Vector3 previousScale;
    private Vector3 previousPosition;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
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
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.transform.name.Contains("Apple"))
                {
                    previousScale = hit.transform.localScale;
                    previousPosition = hit.transform.position;
                    hit.transform.localScale = Vector3.one * 0.5f;
                    selectedApple = hit.transform;
                }
            }
        }

        // drag apple
        if (Input.GetMouseButton(0))
        {
            Vector3 mp = Input.mousePosition;
            mp.z = 2f;
            mp.y += 50; // change the offset for fat fingers
            if (selectedApple)
                selectedApple.position = Camera.main.ScreenToWorldPoint(mp);
        }

        // release apple
        if (Input.GetMouseButtonUp(0))
        {
            if (!selectedApple) return;

            if (!SnapToBasket())
            {
                selectedApple.localScale = previousScale;
                selectedApple.position = previousPosition;
            }
            
            selectedApple = null;
        }
    }

    private bool SnapToBasket()
    {
        Collider[] hitColliders = Physics.OverlapSphere(selectedApple.position, 0.3f);
        if (hitColliders.Any(b => b.name.Contains("Straw")))
        {
            selectedApple.position = hitColliders[0].transform.position;
            foreach (var item in hitColliders)
            {
                print(item.name);
            }
            
            return true;
        }
        return false;
    }
}
