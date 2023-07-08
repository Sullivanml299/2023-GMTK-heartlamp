using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearAttack : EnemyBehavior
{

    public Vector3 leftOffset, rightOffset;
    public Vector3 leftHitboxSize, rightHitboxSize;
    public LayerMask mask;
    public float force = 10.0f;
    public float attackRange = 2.0f;

    public Transform leftPaw, rightPaw;
    public GameObject shockwavePrefab;
    public Vector3 shockwaveOffset;

    int attackIndex;
    bool usedShockwave = false;
    HashSet<Transform> hitObjects = new HashSet<Transform>();
    List<string> attackList = new List<string> {
        "Attack1", //right horizontal swing
        "Attack2", //left horizontal swing
        "Attack5", // front slam
        };

    public override void behaviorEnter()
    {
        hitObjects.Clear();
        usedShockwave = false;
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

        if (stateInfo.normalizedTime >= 1.0f)
        {
            hitObjects.Clear();
            usedShockwave = false;
            attackIndex = Random.Range(0, attackList.Count);
            enemyData.animator.Play(attackList[attackIndex], 0, 0.0f);
            if (Vector3.Distance(enemyData.target.position, transform.position) > attackRange)
            {
                enemyData.controller.setState(EnemyState.engage);
            }
        }
        else
        {
            if (attackIndex <= 1) hitCheck();
            else if (!usedShockwave) GroundPound();
        }
    }

    void GroundPound()
    {
        if (enemyData.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f)
        {
            var offset = transform.forward * shockwaveOffset.x +
                     transform.up * shockwaveOffset.y +
                     transform.right * shockwaveOffset.z;
            var shockwave = Instantiate(shockwavePrefab, transform.position + offset, Quaternion.identity);
            if (shockwave.transform.position.y < 0.1f) shockwave.transform.position = new Vector3(shockwave.transform.position.x, 0.1f, shockwave.transform.position.z);
            usedShockwave = true;
        }

    }

    void hitCheck()
    {
        Transform paw = attackIndex == 0 ? rightPaw : leftPaw;
        Vector3 hitboxSize = (attackIndex == 0 ? rightHitboxSize : leftHitboxSize) / 2; //half extents
        hitboxSize.Scale(paw.lossyScale);
        var offset = paw.forward * (attackIndex == 0 ? rightOffset : leftOffset).x +
                     paw.up * (attackIndex == 0 ? rightOffset : leftOffset).y +
                     paw.right * (attackIndex == 0 ? rightOffset : leftOffset).z;
        Vector3 hitboxCenter = paw.position + offset;
        foreach (Collider c in Physics.OverlapBox(hitboxCenter, hitboxSize, paw.rotation, mask))
        {
            //TODO: may need to adjust if hitting self
            if (c.transform == paw
                || c.transform == transform
                || hitObjects.Contains(c.transform))
            {
                continue;
            }
            else
            {
                Rigidbody rb;
                if (c.TryGetComponent<Rigidbody>(out rb))
                {
                    hitObjects.Add(c.transform);
                    print("hit: " + c.name);
                    float finalForce = force * rb.mass;
                    rb.AddForce(((c.transform.position - transform.position).normalized + Vector3.up) * finalForce, ForceMode.Impulse);
                }
            }

        };
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.green;
        var offset = transform.forward * shockwaveOffset.x +
                     transform.up * shockwaveOffset.y +
                     transform.right * shockwaveOffset.z;
        Gizmos.DrawSphere(transform.position + offset, 0.1f);

        Transform paw = rightPaw;
        Vector3 hitboxSize = rightHitboxSize;
        hitboxSize.Scale(paw.lossyScale);
        offset = paw.forward * rightOffset.x +
                     paw.up * rightOffset.y +
                     paw.right * rightOffset.z;

        Gizmos.matrix = Matrix4x4.TRS(paw.position + offset, paw.rotation, Vector3.one);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(Vector3.zero, hitboxSize);


        paw = leftPaw;
        hitboxSize = leftHitboxSize;
        hitboxSize.Scale(paw.lossyScale);
        offset = paw.forward * leftOffset.x +
                     paw.up * leftOffset.y +
                     paw.right * leftOffset.z;

        Gizmos.matrix = Matrix4x4.TRS(paw.position + offset, paw.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, hitboxSize);

    }
}
