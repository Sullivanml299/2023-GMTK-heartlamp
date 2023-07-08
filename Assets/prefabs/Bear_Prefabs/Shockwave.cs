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

    HashSet<Collider> hitObjects = new HashSet<Collider>();

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
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
        if (other.tag == "Boss") return;

        if (!hitObjects.Contains(other))
        {
            hitObjects.Add(other);
            var rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(((other.transform.position - transform.position).normalized + Vector3.up) * force, ForceMode.Impulse);
            }
            EnemyController ec;
            if (other.TryGetComponent<EnemyController>(out ec))
            {
                ec.takeDamage(1);
            }
        }
    }
}
