using UnityEngine;

public class ExplosionSelfDamage : MovePointBase
{
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private PlayerHealth target;
    private float damage = 50;
    private DamageType damageType;
    
    private void Awake()
    {
        Resistences resistence = target.GetComponent<Resistences>();
        damageType = new FireDamage(resistence, damage);
    }
    public override void Apply()
    {
        GameObject explosion = Instantiate(explosionPrefab, target.transform.position + Vector3.up * 3, Quaternion.identity);
        OnDestroyEffect effect = explosion.GetComponent<OnDestroyEffect>();
        target.TakeDamage(damageType);
        effect.OnDestroyed += OpenDice;
    }
    private void OpenDice()
    {
        if (GameManager.Instance.GameCanContinue)
        {
            DiceController.Instace.OpenDice();
        }
    }
}
