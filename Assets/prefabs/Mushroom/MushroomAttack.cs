using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomAttack : EnemyBehavior
{

    public float yOffset = 0.5f;
    public Vector3 hitboxSize;
    public LayerMask mask;
    public float force = 10.0f;
    public float attackRange = 2.0f;

    bool hit = false;

    public override void behaviorEnter()
    {
        hit = false;
        enemyData.animator.Play("Attack", 0, 0.0f);
    }

    public override void behaviorExit()
    {
        enemyData.animator.ResetTrigger("attack");
    }

    public override void behaviorUpdate()
    {
        var stateInfo = enemyData.animator.GetCurrentAnimatorStateInfo(0);



        if (!hit && stateInfo.normalizedTime >= 0.4f)
        {
            hit = true;
            hitCheck();
        }

        if (stateInfo.normalizedTime >= 1.0f)
        {
            hit = false;
            enemyData.animator.Play("Attack", 0, 0.0f);
            if (Vector3.Distance(enemyData.target.position, transform.position) > attackRange)
            {
                enemyData.controller.setState(EnemyState.engage);
            }
        }
    }

    void hitCheck()
    {
        foreach (Collider c in Physics.OverlapBox(Vector3.up * yOffset + transform.position + transform.forward, hitboxSize, transform.rotation, mask))
        {
            if (c.transform == transform)
            {
                continue;
            }
            else
            {
                Rigidbody rb;
                if (c.TryGetComponent<Rigidbody>(out rb))
                {
                    float finalForce = force;
                    rb.AddForce(((c.transform.position - transform.position).normalized + Vector3.up) * force * rb.mass, ForceMode.Impulse);
                }
            }

        };
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(Vector3.up * yOffset + transform.position + transform.forward, 0.1f);
    }
}

