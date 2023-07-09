using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearEngage : EnemyBehavior
{
    public float deaggroRange = 20f;
    public float attackRange = 2.0f;
    public float runSpeed = 10.0f;
    public float maxEngageAngle = 25.0f;
    public float turnSpeed = 10.0f;
    public CharacterController controller;
    public float gravity = -9.8f;
    public override void behaviorEnter()
    {
        enemyData.animator.SetBool("Run Forward", true);
    }

    public override void behaviorExit()
    {
        enemyData.animator.SetBool("Run Forward", false);
    }

    public override void behaviorUpdate()
    {
        // print("engage");
        var distance = Vector3.Distance(enemyData.target.position, transform.position);
        if (distance > deaggroRange) enemyData.controller.setState(EnemyState.idle);
        else if (distance < attackRange) enemyData.controller.setState(EnemyState.attack);
        else
        {
            Vector3 direction = (enemyData.target.position - transform.position).normalized;
            if (isFacingTarget())
            {
                transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                // enemyData.rigidbody.MovePosition(transform.position + direction * runSpeed * Time.deltaTime);
                Vector3 vel = direction * runSpeed;// * Time.deltaTime
                controller.Move(vel * Time.deltaTime);
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z)), Time.deltaTime * turnSpeed);
            }
        }
    }

    bool isFacingTarget()
    {
        Vector3 direction = (enemyData.target.position - transform.position).normalized;
        float angle = Vector3.Angle(direction, transform.forward);
        return angle < maxEngageAngle;
    }
}
