using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearAttack : EnemyBehavior
{

    public Vector3 offset;
    public Vector3 leftHitboxSize, rightHitboxSize;
    public LayerMask mask;
    public float force = 10.0f;
    public float attackRange = 2.0f;

    public Transform leftPaw, rightPaw;

    bool hit = false;
    int attackIndex;

    List<string> attackList = new List<string> {
        "Attack1", //right horizontal swing
        "Attack2", //left horizontal swing
        "Attack5", // front slam
        };

    public override void behaviorEnter()
    {
        // hit = false;
        attackIndex = Random.Range(0, attackList.Count);
        enemyData.animator.Play(attackList[attackIndex], 0, 0.0f);
    }

    public override void behaviorExit()
    {
        // enemyData.animator.ResetTrigger("attack");
    }

    public override void behaviorUpdate()
    {
        var stateInfo = enemyData.animator.GetCurrentAnimatorStateInfo(0);



        if (!hit && stateInfo.normalizedTime >= 0.4f)
        {
            hitCheck();
        }

        if (stateInfo.normalizedTime >= 1.0f)
        {
            hit = false;
            attackIndex = Random.Range(0, attackList.Count);
            enemyData.animator.Play(attackList[attackIndex], 0, 0.0f);
            if (Vector3.Distance(enemyData.target.position, transform.position) > attackRange)
            {
                enemyData.controller.setState(EnemyState.engage);
            }
        }
    }

    void hitCheck()
    {
        Transform paw = attackIndex == 0 ? rightPaw : leftPaw;
        Vector3 hitboxSize = attackIndex == 0 ? rightHitboxSize : leftHitboxSize;
        hitboxSize = Vector3.Scale(hitboxSize, transform.localScale);
        foreach (Collider c in Physics.OverlapBox(paw.position, hitboxSize, paw.rotation, mask))
        {
            //TODO: may need to adjust if hitting self
            if (c.transform == paw || c.transform == transform)
            {
                continue;
            }
            else
            {
                Rigidbody rb;
                if (c.TryGetComponent<Rigidbody>(out rb))
                {
                    print("hit: " + c.name);
                    float finalForce = force * rb.mass;
                    rb.AddForce(((c.transform.position - paw.position).normalized + Vector3.up) * finalForce, ForceMode.Impulse);
                }
            }

        };
    }

    void OnDrawGizmosSelected()
    {
        Transform paw = attackIndex == 0 ? rightPaw : leftPaw;
        Vector3 hitboxSize = attackIndex == 0 ? rightHitboxSize : leftHitboxSize;
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(paw.position, 1f);
        Gizmos.matrix = Matrix4x4.TRS(paw.position, paw.rotation, transform.localScale);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(Vector3.zero, hitboxSize);
    }
}
