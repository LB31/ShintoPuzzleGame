using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamWorldController : MonoBehaviour
{
    public Animator anim;
    public GameObject AmaterasuTrigger;

    private bool firstTime = true;

    private void Awake()
    {
        enabled = false;
    }

    private void OnEnable()
    {
        if (firstTime)
        {
            PlayAnimation("fallingOrb");
            firstTime = false;
        }

    }

    public void PlayAnimation(string name)
    {
        anim.Play(name);
    }




}
