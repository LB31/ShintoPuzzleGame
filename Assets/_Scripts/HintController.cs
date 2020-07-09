using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintController : MonoBehaviour
{
    public Transform Resizer;
    public GameObject ClickHintRotator;

    private void Start()
    {
        if (ClickHintRotator)
            ClickHintRotator.SetActive(false);
    }

    private void Update()
    {
        if (!ClickHintRotator.activeSelf) return;
        ClickHintRotator.transform.LookAt(Camera.main.transform);
        float newScale = Mathf.PingPong(Time.time, 0.5f) + 1;
        Resizer.localScale = new Vector3(newScale, newScale, 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        ClickHintRotator.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        ClickHintRotator.SetActive(false);
    }
}
