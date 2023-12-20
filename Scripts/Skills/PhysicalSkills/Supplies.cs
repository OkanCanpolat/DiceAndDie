using UnityEngine;
using System.Threading.Tasks;

[CreateAssetMenu(fileName = "Supplies", menuName = "Skills/Common/Supplies")]

public class Supplies : Skill
{
    [SerializeField] private GameObject effectPrefab;
    [SerializeField] private float effectDestroyTime;
    [SerializeField] private int supplyDelayMS;
    [SerializeField] private int keyAmount;
    [SerializeField] private int foodAmount;
    [SerializeField] private BodyParts effectPosition;
    private PlayerSkillManager player;
    private PlayerStuffManager stuffs;

    public override void Use()
    {

        player = CombatManager.Instance.GetPlayer();
        stuffs = player.GetComponent<PlayerStuffManager>();
        Transform effectPos = BodyPartDatabase.Instance.GetPartTransform(effectPosition);

        GameObject skill = Instantiate(effectPrefab, effectPos.position, Quaternion.identity);
        Destroy(skill, skillCompleteTime);
        GainSupply();
    }

    private async Task GainSupply()
    {
        await Task.Delay(supplyDelayMS);
        int result = Random.Range(1, 11);

        if(result % 2 == 0)
        {
            stuffs.IncreaseKey(keyAmount);
        }
        else
        {
            stuffs.IncreaseFood(foodAmount);
        }
    }
}
