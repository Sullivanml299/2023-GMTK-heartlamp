using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomDeath : EnemyBehavior
{
    public GameObject puffPrefab;
    public override void behaviorEnter()
    {
        enemyData.animator.Play("Death");
    }

    public override void behaviorExit()
    {
        throw new System.NotImplementedException();
    }

    public override void behaviorUpdate()
    {
        AnimatorStateInfo stateInfo = enemyData.animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.normalizedTime >= 1)
        {
            Destroy(gameObject);
            Instantiate(puffPrefab, transform.position, transform.rotation);
        }
    }


}
