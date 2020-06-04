using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{

    private Transform player;

    private Transform tutorialSphere;

    private bool startEvent;

    private Animator animator;

    private void Start()
    {
        player = FindObjectOfType<FPSController>().transform;
        tutorialSphere = GameObject.FindWithTag("DreamOrb").transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
 
            if (WorldSwitcher.Instance.sendingKami == WorldSwitcher.SendingKami.Amaterasu)
            {
                player.GetComponent<FPSController>().enabled = false;
                animator = FindObjectOfType<DreamWorldController>().anim;
                StartCoroutine(PlayAndWaitForAnim(animator, "amaterasuOrb"));
            }
                
        }
    }




    public IEnumerator PlayAndWaitForAnim(Animator targetAnim, string stateName)
    {
        targetAnim.Play(stateName);

        //Wait until we enter the current state
        while (!targetAnim.GetCurrentAnimatorStateInfo(0).IsName(stateName))
        {
            yield return null;
        }

        //Now, Wait until the current state is done playing
        while ((targetAnim.GetCurrentAnimatorStateInfo(0).normalizedTime) % 1 < 0.99f)
        {
            Quaternion lookOnLook = Quaternion.LookRotation(tutorialSphere.position - player.position);
            player.rotation = Quaternion.Slerp(player.rotation, lookOnLook, Time.deltaTime * 2);
            yield return null;
        }

        //Done playing. Do something below!
        player.GetComponent<FPSController>().enabled = true;
    }
}
