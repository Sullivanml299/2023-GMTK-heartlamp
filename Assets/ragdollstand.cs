using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ragdollstand : MonoBehaviour
{
    public float timeRemaining = 5f;
    public GameObject standman;

    private RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {

            if (Physics.Raycast(transform.position, -Vector3.up, out hit, Mathf.Infinity))
            {
                Instantiate(standman, hit.point, Quaternion.identity);
                Destroy(gameObject);
            }
            else
            {
                Instantiate(standman, transform.position + new Vector3(0, -3f, 0), Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
