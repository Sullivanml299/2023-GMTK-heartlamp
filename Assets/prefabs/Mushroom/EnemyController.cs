using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Animator animator;
    public EnemyBehavior idleBehavior;
    public EnemyBehavior engageBehavior;
    public EnemyBehavior attackBehavior;
    public EnemyBehavior damageBehavior;
    public EnemyBehavior deathBehavior;
    public float hp = 1f;

    private EnemyData enemyData;
    private EnemyBehavior currentBehavior;




    // Start is called before the first frame update
    void Start()
    {
        enemyData = new EnemyData();
        enemyData.animator = animator;
        enemyData.target = GameObject.FindGameObjectWithTag("Player").transform;
        enemyData.rigidbody = GetComponent<Rigidbody>();
        enemyData.controller = this;

        if (idleBehavior != null) idleBehavior.setEnemyData(enemyData);
        if (engageBehavior != null) engageBehavior.setEnemyData(enemyData);
        if (attackBehavior != null) attackBehavior.setEnemyData(enemyData);
        if (damageBehavior != null) damageBehavior.setEnemyData(enemyData);
        if (deathBehavior != null) deathBehavior.setEnemyData(enemyData);

        currentBehavior = idleBehavior;
    }

    // Update is called once per frame
    void Update()
    {
        currentBehavior.behaviorUpdate();
        if (hp <= 0 && currentBehavior != deathBehavior)
        {
            print("death");
            setState(EnemyState.death);
        }
    }

    public void setState(EnemyState newState)
    {
        currentBehavior.behaviorExit();
        switch (newState)
        {
            case EnemyState.idle:
                currentBehavior = idleBehavior;
                break;

            case EnemyState.engage:
                currentBehavior = engageBehavior;
                break;

            case EnemyState.attack:
                currentBehavior = attackBehavior;
                break;

            case EnemyState.damage:
                currentBehavior = damageBehavior;
                break;

            case EnemyState.death:
                currentBehavior = deathBehavior;
                break;
        }

        currentBehavior.behaviorEnter();
    }
    public void takeDamage(float damage)
    {
        hp -= damage;
        // setState(EnemyState.damage);
    }

    public void applyForce(Vector3 force)
    {

        if (enemyData.rigidbody != null && !enemyData.rigidbody.isKinematic)
            enemyData.rigidbody.AddForce(force);
    }

}




public enum EnemyState
{
    idle,
    engage,
    attack,
    damage,
    death,
}

public struct EnemyData
{
    public Rigidbody rigidbody;
    public Animator animator;
    public Transform target;
    public EnemyController controller;
}