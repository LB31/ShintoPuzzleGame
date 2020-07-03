using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamworlTrigger : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        PlayMakerFSM.BroadcastEvent("ENTER DREAMWORLD");
    }
}
