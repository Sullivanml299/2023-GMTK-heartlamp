using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnRider : MonoBehaviour
{
    public GameObject rider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
        if (Input.GetKeyDown(KeyCode.R) && rider.active == false) { rider.active = true; }
        else if (Input.GetKeyDown(KeyCode.R) && rider.active == true) { rider.active = false; }
    }
}
