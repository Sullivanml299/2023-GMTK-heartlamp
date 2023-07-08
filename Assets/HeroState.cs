using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class HeroState : MonoBehaviour
{
    public Animator animator;
    public RigBuilder rigbuilder;
    public GameObject HeroAimObject, ridePosObj;

    public enum HeroCurrentState
    {
        Idle,
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
        animator.SetBool("Wave", false);
        animator.SetBool("Aim", false);
        animator.SetBool("Stand", false);
        animator.SetBool("Ride", false);

        HeroAimObject.TryGetComponent<heroAim>(out heroAim heroAimScript);
        heroAimScript.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey(KeyCode.W)) { state = HeroCurrentState.Wave; print("W!"); }
        //if (Input.GetKey(KeyCode.A)) { state = HeroCurrentState.Aim; print("A!"); }

        switch (state)
        {
            case HeroCurrentState.Idle:
                Idle();
                break;
            case HeroCurrentState.Wave:
                Wave();
                break;
            case HeroCurrentState.Aim:
                Aim();
                break;
            case HeroCurrentState.Stand:
                Stand();
                break;
            case HeroCurrentState.Ride:
                Ride();
                break;
        }
    }

    void Wave()
    {
        animator.SetBool("Wave", true);
        animator.SetBool("Aim", false);
        animator.SetBool("Stand", false);
        animator.SetBool("Ride", false);
        rigbuilder.enabled = false;

        print(Vector3.Distance(transform.position, ridePosObj.transform.position));
        if (Vector3.Distance(transform.position, ridePosObj.transform.position) < 3.0f)
        {
            state = HeroCurrentState.Ride;
        }
    }

    void Aim()
    {
        animator.SetBool("Wave", false);
        animator.SetBool("Aim", true);
        animator.SetBool("Stand", false);
        animator.SetBool("Ride", false);
        rigbuilder.enabled = true;
    }

    void Stand()
    {
        animator.SetBool("Wave", false);
        animator.SetBool("Aim", false);
        animator.SetBool("Stand", true);
        animator.SetBool("Ride", false);
        rigbuilder.enabled = false;
    }

    void Idle()
    {
        animator.SetBool("Wave", false);
        animator.SetBool("Aim", false);
        animator.SetBool("Stand", false);
        animator.SetBool("Ride", false);
        rigbuilder.enabled = false;
    }

    void Ride()
    {
        animator.SetBool("Wave", false);
        animator.SetBool("Aim", false);
        animator.SetBool("Stand", false);
        animator.SetBool("Ride", true);
        rigbuilder.enabled = false;

        transform.position = ridePosObj.transform.position;
        transform.rotation = ridePosObj.transform.rotation;
    }


}
