using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public float speed = 2f;
    public Transform start;
    public Transform end;
    public bool plattformDoesMove = true;
    private bool hinfahrt = false;
    private float t = 2;
    private Vector3 plattformRichtung;

    private void Start()
    {
        plattformRichtung = end.position - start.position;
        plattformRichtung.Normalize();
    }

    private void FixedUpdate()
    {
        if (plattformDoesMove)
        {
            if (getDistance(this.transform, start) < 0.2 && hinfahrt == false) // Fahrt von Start zu Ziel
            {
                hinfahrt = true;
                plattformRichtung = end.position - start.position;
                plattformRichtung.Normalize();
                t = 2;
            }
            if (getDistance(this.transform, end) < 0.2 && hinfahrt == true) // Fahrt von Ziel zu Start
            {
                hinfahrt = false;
                plattformRichtung = start.position - end.position;
                plattformRichtung.Normalize();
                t = 2;
            }
            t -= Time.deltaTime;
            if (t < 0)
            {
                Vector3 translation = Time.deltaTime * plattformRichtung * speed;
                transform.Translate(translation);
            }
        }
    }

    private float getDistance(Transform plattformPos, Transform anchorPointPos)
    {
        return Mathf.Sqrt(
            Mathf.Pow(plattformPos.position.x - anchorPointPos.position.x, 2) +
            Mathf.Pow(plattformPos.position.y - anchorPointPos.position.y, 2) +
            Mathf.Pow(plattformPos.position.z - anchorPointPos.position.z, 2));
    }
}