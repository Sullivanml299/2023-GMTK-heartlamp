using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hovertitle : MonoBehaviour
{
    Rigidbody hb;
    public float mult;
    public float moveForce;
    public float turnTorque;
    public float hoverRayDist;

    public bool game_start;

    public float runMult, runMultMax, jumpForce, fallForce, flipForce;

    public CameraFade cf;
    public GameObject fadeObj;

    public float timeRemaining = 4.0f;

    void Start()
    {
        hb = GetComponent<Rigidbody>();
        hoverRayDist = 1;

        game_start = false;
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
            if (game_start)
            { 
            hb.AddForce(1.0f * moveForce * transform.forward * runMult); }
        }
        else
        {
            hb.drag = 0.1f;
        }


        hb.AddTorque(Input.GetAxis("Horizontal") * turnTorque * transform.up);



    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.S))
         {
            game_start = true;
        }

        if (game_start)
        {
            runMult = runMultMax;
        }
        else
        {
            runMult = 1;
        }

        if (Physics.Raycast(transform.position, -Vector3.up, hoverRayDist * 1.5f))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //print("Jump!");

                hb.AddForce(Vector3.up * jumpForce);


            }
        }
        else
        {
            //print("fall!");
            hb.AddForce(-Vector3.up * fallForce);
        }


        if (Physics.Raycast(transform.position, transform.up, 2f))
        {
            //if (Input.GetKeyDown(KeyCode.Space))
            // {
            //print("Flip!");
            hb.AddForceAtPosition(Vector3.up * flipForce, transform.position + new Vector3(0.05f, 0.03f, 0f), ForceMode.Force);
            //}
        }






        if (timeRemaining > 0)
        {
            if (game_start)
            {
                timeRemaining -= Time.deltaTime;
            }
        }
        else
        {
            cf = fadeObj.GetComponent<CameraFade>();
            cf.exit_scene = true;

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
