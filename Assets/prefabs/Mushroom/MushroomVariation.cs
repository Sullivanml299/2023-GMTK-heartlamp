using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomVariation : MonoBehaviour
{
    public SkinnedMeshRenderer mesh;
    public List<Material> materials;
    // Start is called before the first frame update
    void Start()
    {
        mesh.material = materials[Random.Range(0, materials.Count)];

        transform.localScale *= Random.Range(0.7f, 1.3f);
    }

}
