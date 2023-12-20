using System.Collections;
using UnityEngine;

public class Lich : Enemy
{
    private PlayerSkillManager player;
    private Animator animator;
    private EnemyHealth health;

    [SerializeField] private EssenceHarvest harvestSkill;
    private int harvestTurnDelay = 4;
    private int harvestTurnCount;
    [SerializeField] private FusionCore fusionCore;
    private int fusionCoreDelay = 2;
    private int fusionTurnCount = 0;
    [SerializeField] private MagicMissile magicMissile;
    private bool magicMissileActive = true;
    private bool magicMissileUsedLastTurn;
    [SerializeField] private SpiritBomb spiritBomb;
    private bool spiritBombActive = false;
    private bool spiritBombUsedLastTurn;
    [SerializeField] private StormPillar stormPillar;
    private int stormPillarDelay = 5;
    private int stormPillarTurnCount = 0;
     

    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = CombatManager.Instance.GetPlayer();
        playerHealth = player.GetComponent<PlayerHealth>();
        health = GetComponent<EnemyHealth>();

        CombatManager.Instance.OnEnemyTurn += ReduceTurnCounts;
        CombatManager.Instance.OnEnemyTurn += Attack;
        CombatManager.Instance.OnPlayerTurn += IncreaseMana;
        health.OnDie += OnDie;

        currnetMana = lastTurnMana;
        basicAttackDamage = defaultBasicAttackDamage;
    }

    private void Start()
    {
        ManaChanged();
    }

    public override void OnDie()
    {
        CombatManager.Instance.EnemyDied();
        animator.SetTrigger("Die");
    }
    public void Die()
    {
        Destroy(gameObject, 1f);
        GameManager.Instance.OnLevelCompleted();
    }
    private void Attack()
    {
        if (health.isDead) return;

        StartCoroutine(TemplateMethod());
    }
    private IEnumerator TemplateMethod()
    {
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(ControlHarvest());
        yield return StartCoroutine(ControlFusionCore());
        yield return StartCoroutine(ControlStormPillar());
        yield return StartCoroutine(ControlMagicMissile());
        yield return StartCoroutine(ControlSpiritBomb());
        yield return StartCoroutine(AttackC());
    }

    private IEnumerator ControlHarvest()
    {
        if(harvestTurnCount == 0 && currnetMana >= harvestSkill.RequiredMana)
        {
            harvestSkill.Use();
            harvestTurnCount = harvestTurnDelay;
            currnetMana -= harvestSkill.RequiredMana;
            ManaChanged();
            animator.SetTrigger(harvestSkill.animationParameterName);
            yield return new WaitForSeconds(harvestSkill.skillCompleteTime);
        }
    }
    private IEnumerator ControlFusionCore()
    {
        if(fusionTurnCount == 0 && currnetMana >= fusionCore.RequiredMana)
        {
            fusionCore.Use();
            fusionTurnCount = fusionCoreDelay;
            currnetMana -= fusionCore.RequiredMana;
            ManaChanged();
            animator.SetTrigger(fusionCore.animationParameterName);
            yield return new WaitForSeconds(fusionCore.skillCompleteTime);
        }
    }
    private IEnumerator ControlMagicMissile()
    {
        if(magicMissileActive && currnetMana >= magicMissile.RequiredMana)
        {
            magicMissileUsedLastTurn = true;
            magicMissile.Use();
            currnetMana -= magicMissile.RequiredMana;
            ManaChanged();
            animator.SetTrigger(magicMissile.animationParameterName);
            yield return new WaitForSeconds(magicMissile.skillCompleteTime);
        }
    }
    private IEnumerator ControlSpiritBomb()
    {
        if (spiritBombActive && currnetMana >= spiritBomb.RequiredMana)
        {
            spiritBombUsedLastTurn = true;
            spiritBomb.Use();
            currnetMana -= spiritBomb.RequiredMana;
            ManaChanged();
            animator.SetTrigger(spiritBomb.animationParameterName);
            yield return new WaitForSeconds(spiritBomb.skillCompleteTime);
        }
    }
    private IEnumerator ControlStormPillar()
    {
        if(stormPillarTurnCount == 0 && currnetMana >= stormPillar.RequiredMana)
        {
            stormPillar.Use();
            stormPillarTurnCount = stormPillarDelay;
            currnetMana -= stormPillar.RequiredMana;
            ManaChanged();
            animator.SetTrigger(stormPillar.animationParameterName);
            yield return new WaitForSeconds(stormPillar.skillCompleteTime);
        }
    }
    private IEnumerator AttackC()
    {
        if (currnetMana >= basicAttackManaConsume)
        {
            animator.SetTrigger("Attack");
            currnetMana -= basicAttackManaConsume;
            ManaChanged();
            yield return new WaitForSeconds(1);
            playerHealth.TakeDamage(new BasicAttack(playerHealth.GetComponent<Resistences>(), basicAttackDamage));
            yield return new WaitForSeconds(1);
            StartPlayerTurn();
        }

        else
        {
            StartPlayerTurn();
        }
    }
    private void IncreaseMana()
    {
        lastTurnMana++;
        currnetMana = lastTurnMana;
        currnetMana = Mathf.Clamp(currnetMana, 0, maxMana);
        ManaChanged();
    }

    private void ReduceTurnCounts()
    {
        if(harvestTurnCount > 0)
        {
            harvestTurnCount--;
        }
        if(fusionTurnCount > 0)
        {
            fusionTurnCount--;
        }
        if(stormPillarTurnCount > 0)
        {
            stormPillarTurnCount--;
        }
        if(magicMissileUsedLastTurn)
        {
            magicMissileUsedLastTurn = false;
            magicMissileActive = false;
            spiritBombActive = true;
        }
        if(spiritBombUsedLastTurn)
        {
            spiritBombUsedLastTurn = false;
            magicMissileActive = true;
            spiritBombActive = false;
        }
    }
    private void StartPlayerTurn()
    {
        CombatManager.Instance.StartPlayerTurn();
    }

    private void OnDestroy()
    {
        CombatManager.Instance.OnEnemyTurn -= Attack;
        CombatManager.Instance.OnPlayerTurn -= IncreaseMana;
    }
}
