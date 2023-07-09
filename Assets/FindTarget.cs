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
        hero = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        GetTarget();

        if (target != null) transform.position = target.transform.position;


    }




    void GetTarget()
    {

        float lowestDist = Mathf.Infinity;


        for (int i = 0; i < enemies.Length; i++)
        {
            var enemy = enemies[i];
            if (enemy == null)
                continue;

            float dist = Vector3.Distance(enemy.transform.position, hero.transform.position);

            if (dist < lowestDist)
            {
                lowestDist = dist;
                target = enemy;
            }

        }

    }


    void OnDrawGizmos()
    {
        if (target == null)
            return;
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(target.transform.position, 1.0f);
    }

}
