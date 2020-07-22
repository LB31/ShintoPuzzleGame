using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskHover : MonoBehaviour
{
    [SerializeField]
    private float hoverSpeed = 10.0f;
    [SerializeField]
    private float maxHeight = 5;
    [SerializeField]
    private float minHeight;

    private float hoverHeight;
    private float hoverRange;
    // Start is called before the first frame update
    void Start()
    {
        hoverHeight = (maxHeight + minHeight) / 2.0f;
        hoverRange = maxHeight - minHeight;
    }

    // Update is called once per frame
    void Update()
    {
            this.transform.position = Vector3.up * (hoverHeight + Mathf.Cos(Time.time * hoverSpeed) * hoverRange);
    }
}
