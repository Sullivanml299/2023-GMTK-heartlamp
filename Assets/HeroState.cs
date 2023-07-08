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
        Wave,
        Stand,
    }

    public HeroCurrentState state = HeroCurrentState.Wave;
    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("Wave", false);
        animator.SetBool("Stand", false);

        HeroAimObject.TryGetComponent<heroAim>(out heroAim heroAimScript);
        heroAimScript.enabled = false;

        state = HeroCurrentState.Stand;
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

            case HeroCurrentState.Stand:
                Stand();
                break;

        }
    }

    void Wave()
    {
        animator.SetBool("Wave", true);
        animator.SetBool("Stand", false);
        rigbuilder.enabled = false;

       // print(Vector3.Distance(transform.position, ridePosObj.transform.position));
        if (Vector3.Distance(transform.position, ridePosObj.transform.position) < 3.0f)
        {
            //state = HeroCurrentState.Ride;
        }
    }

  

    void Stand()
    {
        animator.SetBool("Wave", false);
        animator.SetBool("Stand", true);
        rigbuilder.enabled = false;
    }



}
