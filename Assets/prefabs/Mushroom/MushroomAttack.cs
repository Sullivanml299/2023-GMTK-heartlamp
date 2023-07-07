using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomAttack : EnemyBehavior
{

    public override void behaviorEnter()
    {
        enemyData.animator.SetTrigger("attack");
    }

    public override void behaviorExit()
    {
        enemyData.animator.ResetTrigger("attack");
    }

    public override void behaviorUpdate()
    {
        print("attack");
        var stateInfo = enemyData.animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.normalizedTime >= 1.0f)
        {
            enemyData.controller.setState(EnemyState.engage);
        }
    }
}
