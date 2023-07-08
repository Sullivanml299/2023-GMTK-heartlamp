using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class HeroState : MonoBehaviour
{
    public Animator animator;
    public RigBuilder rigbuilder;
    public enum HeroCurrentState
    {
        Wave,
        Ride,
        Aim,
        Ragdoll,
        Stand,
    }

    public HeroCurrentState state = HeroCurrentState.Wave;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey(KeyCode.W)) { state = HeroCurrentState.Wave; print("W!"); }
        //if (Input.GetKey(KeyCode.A)) { state = HeroCurrentState.Aim; print("A!"); }

        switch (state)
        {
            case HeroCurrentState.Wave:
                Wave();
                break;
            case HeroCurrentState.Aim:
                Aim();
                break;
        }
    }

    void Wave()
    {
        animator.SetBool("Wave", true);
        rigbuilder.enabled = false;
    }

    void Aim()
    {
        animator.SetBool("Wave", false);
        rigbuilder.enabled = true;
    }
}
