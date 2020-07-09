using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    private FPSController player;
    private Vector3 pos = Vector3.zero;
    public float speed = 0.25f;
    public Transform start;
    public Transform end;
    private bool playerEntered = false;

    private Vector3 offset;
    private bool platformMove;

    private async void Start()
    {
        player = FindObjectOfType<FPSController>();
        await Task.Delay(3000);
        platformMove = true;

    }

    public float t = 1;
    bool wait;
    public float pingPong = 0.5f;

    private void Update()
    {
        if (!platformMove) return;

        if ((pingPong > 0.9f || pingPong < 0.1f) && !wait)
        {
            wait = true;
            t = 0;
        }

        if (t >= 1)
        {
            wait = false;
            pingPong = Mathf.PingPong(Time.time * speed, 1);
            transform.position = Vector3.Lerp(start.position, end.position, pingPong);
        }

        if (wait)
            t += Time.deltaTime;





    }
}
