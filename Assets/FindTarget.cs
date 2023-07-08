using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class FindTarget : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject target, hero;

    // Start is called before the first frame update

    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        GetTarget();

        transform.position = target.transform.position;

    
    }




    void GetTarget()
    {

        float lowestDist = Mathf.Infinity;


        for (int i = 0; i < enemies.Length; i++)
        {

            float dist = Vector3.Distance(enemies[i].transform.position, hero.transform.position);

            if (dist < lowestDist)
            {
                lowestDist = dist;
                target = enemies[i];
            }

        }

    }

}
