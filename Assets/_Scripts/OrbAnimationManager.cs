using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbAnimationManager : MonoBehaviour
{
    public Animator animator;
    public GameObject player;

    public int yokaiDefeated;

    //public bool triggerOne = false;
    //public bool triggertwo = false;
    // Start is called before the first frame update

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            Debug.Log("AnimatorStart");
            animator.SetBool("startAnim1", true);

        }
        ChangePaths(yokaiDefeated);
        /*
        else if (other.gameObject == player && triggertwo)
        {
            animator.SetBool("startAnim2", true);
        }
        */
        //animator.SetBool("startAnim1", false);
        //animator.SetBool("startAnim2", false);

    }

    public void ChangePaths(int yokaiDefeated)
    {
        animator.SetInteger("yokaiDefeated", yokaiDefeated);
    }
}
