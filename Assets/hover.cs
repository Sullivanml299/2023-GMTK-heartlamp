using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hover : MonoBehaviour
{
    Rigidbody hb;
    public float mult;
    public float moveForce;
    public float turnTorque;
    public float hoverRayDist;


    public float runMult, runMultMax, jumpForce, fallForce,flipForce;

    void Start()
    {
        hb = GetComponent<Rigidbody>();
        hoverRayDist = 1;
    }

    public Transform[] anchors = new Transform[4];
    public RaycastHit[] hits = new RaycastHit[4];

    void FixedUpdate()
    {
        for (int i = 0; i < 4; i++)
            ApplyF(anchors[i], hits[i]);

        if (Physics.Raycast(transform.position, -Vector3.up, hoverRayDist))
        {
            hb.drag = 2.0f;
            hb.AddForce(Input.GetAxis("Vertical") * moveForce * transform.forward * runMult);
        }
        else
        {
            hb.drag = 0.1f;
        }
        

        hb.AddTorque(Input.GetAxis("Horizontal") * turnTorque * transform.up);


            
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            runMult = runMultMax;
        }
        else
        {
            runMult = 1;
        }

        if (Physics.Raycast(transform.position, -Vector3.up, hoverRayDist))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                print("Jump!");

                    hb.AddForceAtPosition(Vector3.up * jumpForce, transform.position, ForceMode.Force);
                
           
            }
        }
        else
        {
            print("fall!");
            hb.AddForce(-Vector3.up * fallForce);
        }


        if (Physics.Raycast(transform.position, transform.up, 0.5f))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                print("Flip!");
                hb.AddForceAtPosition(Vector3.up * flipForce, transform.position + new Vector3(0, 0, 0.2f), ForceMode.Force);
            }
        }
    }

    void ApplyF(Transform anchor, RaycastHit hit)
    {
        if (Physics.Raycast(anchor.position, -anchor.up, out hit, hoverRayDist))
        {
            float force = 0;
            force = Mathf.Abs(1 / (hit.point.y - anchor.position.y));
            hb.AddForceAtPosition(transform.up * force * mult, anchor.position, ForceMode.Acceleration);

            Debug.DrawRay(anchor.position, -anchor.up * hoverRayDist, Color.red);
        }
    }

}
