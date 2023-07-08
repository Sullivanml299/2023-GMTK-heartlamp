using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootPlacement : MonoBehaviour
{
    public Vector3 currentFootPlace;
    public Vector3 footTarget;
    public RaycastHit hit;

    public float stepSpd, stepDistance;

    public bool moveTwd;

    public GameObject transformObjectIK;
    // Start is called before the first frame update
    void Start()
    {
        if (Physics.Raycast(transform.position, -transform.up, out hit, 3.0f))
        {
            footTarget = hit.point;
            currentFootPlace = footTarget;
        }

        moveTwd = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, -transform.up + (transform.forward/Random.RandomRange(1,2)), out hit, 3.0f))
        {
            footTarget = hit.point;
        }

        if (Vector3.Distance(currentFootPlace, footTarget) > stepDistance)
        {
            
            moveTwd = true;
        }

        if (moveTwd == true && Vector3.Distance(currentFootPlace, footTarget) > 0.01)
        {
            var step = stepSpd * Time.deltaTime; // calculate distance to move
            currentFootPlace =  Vector3.MoveTowards(currentFootPlace, footTarget, step);
            
        }
        else
        {
            moveTwd = false;
        }

        transformObjectIK.transform.position = currentFootPlace;
    }


    void OnDrawGizmosSelected()
    {
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(currentFootPlace, 0.1f);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(footTarget, 0.1f);
    }
}
