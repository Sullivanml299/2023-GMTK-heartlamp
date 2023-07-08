using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomIdle : EnemyBehavior
{

    public float aggroRange = 10f;

    public override void behaviorEnter()
    {
        enemyData.animator.SetBool("idle", true);
    }

    public override void behaviorExit()
    {
        enemyData.animator.SetBool("idle", false);
    }

    public override void behaviorUpdate()
    {
        var distance = Vector3.Distance(transform.position, enemyData.target.position);
        if (distance <= aggroRange) enemyData.controller.setState(EnemyState.engage);
    }
}
