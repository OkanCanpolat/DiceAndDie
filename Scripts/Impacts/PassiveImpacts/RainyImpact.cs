using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainyImpact : Impact
{
    private GameObject rainEffect;
    private GameObject healthEffect;
    private PlayerHealth playerHealth;
    private EnemyHealth enemyHealth;
    private PlayerSkillManager player;
    private Enemy enemy;
    private float healthAmount;
    private int enemyHealthChance;

    public RainyImpact(int turnCount, ImpactManager manager, Sprite impactSprite) : base(turnCount, manager, impactSprite)
    {

    }

    public void Init(PlayerSkillManager player, Enemy enemy, float healthAmount, GameObject healthEffect, GameObject rainEffect, int enemyHealthChance)
    {
        this.player = player;
        this.healthAmount = healthAmount;
        this.healthEffect = healthEffect;
        this.rainEffect = rainEffect;
        this.enemyHealthChance = enemyHealthChance;
        this.enemy = enemy;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = enemy.GetComponent<EnemyHealth>();
    }
    public override void OnTurnCome()
    {
        RestorePlayer();
        RestoreEnemy();

        turnCount--;

        if (turnCount == 0)
        {
            impactManager.RemovePassiveImpact(this);
            Object.Destroy(rainEffect);
            return;
        }

        turnCountText.TurnText.text = turnCount.ToString();
    }

    public override void RemoveListeners()
    {
        throw new System.NotImplementedException();
    }
    public override void OnDelete()
    {
        Object.Destroy(rainEffect);
    }
    private void RestorePlayer()
    {
        playerHealth.RestoreHealth(healthAmount);
        Object.Instantiate(healthEffect, player.transform.position, Quaternion.identity);
    }
    private void RestoreEnemy()
    {
        int number = Random.Range(0, 100);
        if(number < enemyHealthChance)
        {
            enemyHealth.RestoreHealth(healthAmount);
            Object.Instantiate(healthEffect, enemy.transform.position, Quaternion.identity);
        }
    }
}
