using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    public float speed = 1.0f;
    [Range(0, 1)]
    public float t = 0;
    Material mat;

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
    }
}
