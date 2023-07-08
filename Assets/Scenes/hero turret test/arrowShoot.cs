using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ArrowProjectile : MonoBehaviour
{
    private Rigidbody arrowRigidbody;
    [SerializeField] private Transform vfxHitEnemy;
    [SerializeField] private Transform vfxHitEnvironment;
    
    [SerializeField] private float maxSpeed = 50f;
    private Collider myCollider;
   // public float bulletHitScan = hitScanLength;


private void Awake() 
{
        arrowRigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {

        float speed = maxSpeed;
        arrowRigidbody.velocity = transform.forward * speed;
    }
    void FixedUpdate()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.forward, out hit, 10.0f))
        {
            print("I am getting destroyed by: " + hit.collider.gameObject.name);
            myCollider = GetComponent<Collider>();

            if (hit.collider != myCollider)
            {
            Instantiate(vfxHitEnemy, hit.point, Quaternion.identity);
            Instantiate(vfxHitEnvironment, hit.point, Quaternion.identity);
            Destroy(gameObject); 
            }

        }
        Debug.DrawRay(transform.position, transform.forward * 10.0f, Color.red);
    }

    private void OnTriggerEnter(Collider other) 
    {
        // if (other.GetComponent<BulletTarget>() != null)
        // {
        //     //hit enemy
        //     //arrowRigidbody.velocity = transform.forward * 0f;
        //     Instantiate(vfxHitEnemy, transform.position, Quaternion.identity);
        // }
        // else
        // {
        //     //hit environment
        //     //arrowRigidbody.velocity = transform.forward * 0f;
        //     Instantiate(vfxHitEnvironment, transform.position, Quaternion.identity);
        // }
        
       // Destroy(gameObject); 
    }
}
