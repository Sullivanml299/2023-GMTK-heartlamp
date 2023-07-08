using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearIdle : EnemyBehavior
{
    public float aggroRange = 20f;

    public override void behaviorEnter()
    {
        enemyData.animator.SetBool("Idle", true);
    }

    public override void behaviorExit()
    {
        enemyData.animator.SetBool("Idle", false);
    }

    public override void behaviorUpdate()
    {
        var distance = Vector3.Distance(transform.position, enemyData.target.position);
        if (distance <= aggroRange) enemyData.controller.setState(EnemyState.engage);
    }
}

