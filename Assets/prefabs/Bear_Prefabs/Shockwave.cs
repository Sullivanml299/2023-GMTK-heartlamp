using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    public float speed = 1.0f;
    [Range(0, 1)]
    public float t = 0;
    public float force = 10.0f;
    Material mat;
    int bossLayer;

    HashSet<Collider> hitObjects = new HashSet<Collider>();

    // Start is called before the first frame update
    void Start()
    {
        bossLayer = LayerMask.NameToLayer("Boss");
        mat = GetComponent<MeshRenderer>().material;
        AlignWithGround();
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime * speed;
        mat.SetFloat("_WaveDistance", t);
        if (t >= 1.0f)
        {
            Destroy(gameObject);
        }

        foreach (var obj in hitObjects)
        {
            if (obj == null) continue;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == bossLayer) return;

        if (!hitObjects.Contains(other))
        {
            print("shockwave hit: " + other.name);
            hitObjects.Add(other);
            var rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(((other.transform.position - transform.position).normalized + Vector3.up) * force * rb.mass, ForceMode.Impulse);
            }
            EnemyController ec;
            if (other.TryGetComponent<EnemyController>(out ec))
            {
                ec.takeDamage(1);
            }
        }
    }

    void AlignWithGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1000))
        {
            transform.position = hit.point;
            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }
        float positionCalculatedY = transform.position.y - Terrain.activeTerrain.SampleHeight(transform.position);
        if (positionCalculatedY < 0.1f)
        {
            float pushHeight = 1 - positionCalculatedY;
            transform.position += new Vector3(0, pushHeight, 0);
        }

    }
}
