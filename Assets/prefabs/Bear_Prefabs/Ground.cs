using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{

    public float castOriginOffset = 0.5f;
    public float castDistance = 5f;
    public float distanceThreshold = 0.1f;
    public float targetDistance = 0.5f;
    public CharacterController controller;
    RaycastHit hit;
    Terrain terrain;

    // Start is called before the first frame update
    void Start()
    {
        if (terrain == null)
        {
            terrain = Terrain.activeTerrain;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float playerPositionCalculatedY = transform.position.y - Terrain.activeTerrain.SampleHeight(transform.position);
        if (playerPositionCalculatedY < 0)
        {
            float pushHeight = 1 - playerPositionCalculatedY;
            transform.position += new Vector3(0, pushHeight, 0);
        }

        if (Physics.Raycast(transform.position + transform.up * castOriginOffset, -transform.up, out hit, castDistance))
        {
            if (hit.distance > targetDistance)
            {
                controller.Move(new Vector3(0, -hit.distance + targetDistance, 0));
            }
            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + transform.up * castOriginOffset, -transform.up * castDistance);
    }

}
