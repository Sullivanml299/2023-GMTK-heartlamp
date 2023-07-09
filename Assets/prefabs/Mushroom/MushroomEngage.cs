using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomEngage : EnemyBehavior
{
    public float deaggroRange = 20f;
    public float attackRange = 2.0f;
    public float runSpeed = 10.0f;

    public override void behaviorEnter()
    {
        enemyData.animator.SetBool("run", true);
    }

    public override void behaviorExit()
    {
        enemyData.animator.SetBool("run", false);
    }

    public override void behaviorUpdate()
    {

        // print("engage");
        var distance = Vector3.Distance(enemyData.target.position, transform.position);
        Vector3 direction = (enemyData.target.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        if (distance > deaggroRange) enemyData.controller.setState(EnemyState.idle);
        else if (distance < attackRange) enemyData.controller.setState(EnemyState.attack);
        else
        {
            enemyData.rigidbody.MovePosition(transform.position + direction * runSpeed * Time.deltaTime);
        }
    }

}
