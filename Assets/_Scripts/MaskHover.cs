using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskHover : MonoBehaviour
{
    [SerializeField]
    private float hoverSpeed;
    [SerializeField]
    private float maxHeight;
    [SerializeField]
    private float minHeight;

    private float hoverHeight;
    private float hoverRange;
    private Vector3 tempPos;
    // Start is called before the first frame update
    void Start()
    {
        hoverHeight = (maxHeight + minHeight) / 2.0f;
        hoverRange = maxHeight - minHeight;
        tempPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        tempPos.y *= hoverHeight + Mathf.Cos(Time.time * hoverSpeed) * hoverRange;
        this.transform.position = tempPos;
    }
}
