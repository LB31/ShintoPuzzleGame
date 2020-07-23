using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbAnimationManager : MonoBehaviour
{
    public Animator animator;
    public GameObject player;

    private bool isTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            //Debug.Log("AnimatorStart");
            //isTriggered = true;
            animator.SetBool("startAnim1", true);
        }
    }

    public void ChangePaths(int yokaiDefeated)
    {
        animator.SetInteger("yokaiDefeated", yokaiDefeated);
    }
}
