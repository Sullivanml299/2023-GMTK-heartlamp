using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehavior : MonoBehaviour
{
    public EnemyData enemyData;

    public abstract void behaviorUpdate();
    public abstract void behaviorEnter();
    public abstract void behaviorExit();

    public void setEnemyData(EnemyData enemyData)
    {
        this.enemyData = enemyData;
    }

}
