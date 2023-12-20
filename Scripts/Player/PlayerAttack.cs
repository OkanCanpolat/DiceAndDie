using System;
using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour, IStunable
{
    public event Action OnAttack;
    public event Action OnAttackEnd;
    public GameObject WeaponMesh;
    public Weapon CurrentWeapon;
    public Transform WeaponAttachPosition;
    public Transform WeapondDeattachPosition;
    [SerializeField] private float weaponDamage;
    private Enemy target;
    private Animator playerAnimator;
    private PlayerSkillManager skillManager;
    private bool canAttack = true;
    private DamageType damageType;
    private Resistences targetResistences;
    private Health targetHealth;
    private float attackAnimationTime = 3;
    private int manaConsume = 2;

    public bool IsStunned { get ; set; }

    private void Awake()
    {
        skillManager = GetComponent<PlayerSkillManager>();
        playerAnimator = GetComponent<Animator>();
        CombatManager.Instance.OnCombatStart += SetTarget;
        CombatManager.Instance.OnPlayerTurn += ActivateCanAttack;
        CombatManager.Instance.OnCombatEnding += OnCombatEnd;
    }
    public float GetWeaponDamage()
    {
        return weaponDamage;
    }
    public void IncreaseWeaponDamage(float value)
    {
        weaponDamage += value;
    }
    private void SetTarget()
    {
        target = CombatManager.Instance.GetEnemy();
        targetResistences = target.GetComponent<Resistences>();
        targetHealth = target.GetComponent<Health>();
        damageType = new BasicAttack(targetResistences, weaponDamage);
    }
    private void ActivateCanAttack()
    {
        if (IsStunned) CombatManager.Instance.StartEnemyTurn();
        canAttack = true;
    }
    private void DeactivateCanAttack()
    {
        canAttack = false;
    }
    public void Attack()
    {
        if (!canAttack || !skillManager.CanUseSpell()) return;
        if (skillManager.GetCurrentMana() < manaConsume) return;

        playerAnimator.SetTrigger("Attack");
        StartCoroutine(BlockSpellCasting());
        DeactivateCanAttack();
        skillManager.UseMana(manaConsume);
        OnAttack?.Invoke();
    }
    public void EquipWeapon()
    {
        CurrentWeapon.transform.parent = WeaponAttachPosition;
        CurrentWeapon.transform.localPosition = Vector3.zero;
    }
    public void UnequipWeapon()
    {
        CurrentWeapon.transform.parent = WeapondDeattachPosition;
        CurrentWeapon.transform.localPosition = Vector3.zero;
        CurrentWeapon.transform.localRotation = Quaternion.Euler(CurrentWeapon.DeattachRotation);
    }
    public void DealDamage()
    {
        targetHealth.TakeDamage(damageType);
    }
    private IEnumerator BlockSpellCasting()
    {
        skillManager.SetCanUseSpell(false);
        yield return new WaitForSeconds(attackAnimationTime);
        skillManager.SetCanUseSpell(true);
        OnAttackEnd?.Invoke();
    }
    public void ResetDamageType()
    {
        damageType = new BasicAttack(targetResistences, weaponDamage);
    }
    public void SetDamageType(DamageType type)
    {
        damageType = type;
    }
    private void OnCombatEnd()
    {
        canAttack = true;
    }
}
