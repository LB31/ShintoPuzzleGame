using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbAnimationManager : MonoBehaviour
{

    public Animator animator;
    public GameObject player;
    public GameObject blockadeWall;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            animator.SetBool("startAnim1", true);
            blockadeWall.SetActive(false);
        }
    }


}
