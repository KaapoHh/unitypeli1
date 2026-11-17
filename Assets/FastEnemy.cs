using UnityEngine;

public class FastEnemy : EnemyBase
{
    protected override void Start()
    {
        base.Start(); 

        moveSpeed = 4f;          
        damageToCampfire = 1;    
        coinReward = 5;         
    }
}