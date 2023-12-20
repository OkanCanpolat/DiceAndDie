using UnityEngine;

public class EnemyPoint : MovePointBase
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private CombatManager combatManager;
    public override void Apply()
    {
        PlayerMovement playerMovement = combatManager.GetPlayer().GetComponent<PlayerMovement>();
        GameObject go = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        playerMovement.RotateSmoothly(spawnPoint);
        go.transform.LookAt(combatManager.GetPlayer().transform);
        Enemy enemy = go.GetComponent<Enemy>();
        combatManager.SetEnemy(enemy);
        combatManager.StartCombat();
    }
}
