using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnRider : MonoBehaviour
{
    public GameObject rider, ragdoll;
    public Rigidbody rb;
    public float horseAngle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.R) && rider.active == false) { rider.active = true; }
        //else if (Input.GetKeyDown(KeyCode.R) && rider.active == true) { rider.active = false; }

        horseAngle = Vector3.Angle(Vector3.up, transform.up);

        if (horseAngle > 80 && rider.active == true) 
        { 
            
            Instantiate(ragdoll, rider.transform.position, rider.transform.rotation);
            //rb = ragdoll.GetComponent<Rigidbody>();
            //rb.AddForce(transform.up * 10000f);

            rider.active = false;
        }
    }
}
