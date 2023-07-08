using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorDisable : MonoBehaviour
{
    public Animator animator;
    
    void Start()
    {
        animator.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
