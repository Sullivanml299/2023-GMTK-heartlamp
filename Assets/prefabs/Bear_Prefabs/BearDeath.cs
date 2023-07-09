using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearDeath : EnemyBehavior
{
    public GameObject puffPrefab;
    public override void behaviorEnter()
    {
        enemyData.animator.StopPlayback();
        enemyData.animator.Play("Death");
    }

    public override void behaviorExit()
    {
        throw new System.NotImplementedException();
    }

    public override void behaviorUpdate()
    {
        // AnimatorStateInfo stateInfo = enemyData.animator.GetCurrentAnimatorStateInfo(0);
        // print(stateInfo.normalizedTime);
        // if (stateInfo.normalizedTime >= 1)
        // {
        //     Destroy(gameObject);
        //     Instantiate(puffPrefab, transform.position, transform.rotation);
        // }
    }
}
