using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestManager : MonoBehaviour
{
    [SerializeField] private GameObject chest;
    [SerializeField] private GameObject chestGiftCanvas;
    [SerializeField] private List<ChestCard> cards;
    [SerializeField] private List<SupriseCardContent> contents;
    [SerializeField] private PlayerStuffManager playerStuffs;
    private int keyAmountToOpenChest = 200;
    private float chestDistanceToCamera = 10f;
    private Animator chestAnimator;

    public void OpenChest()
    {
        int playerKeys = playerStuffs.GetKey();

        if (!DiceController.Instace.DiceEnable || playerKeys < keyAmountToOpenChest) return;

        DiceController.Instace.SetDiceEnable(false);
        DiceController.Instace.SetCanChangeView(false);

        playerStuffs.IncreaseKey(-keyAmountToOpenChest);
        Camera cam = Camera.main;
        PlayerSkillManager player = CombatManager.Instance.GetPlayer();
        Vector3 direction = (player.transform.position - cam.transform.position).normalized;
        Vector3 chestPosition = cam.transform.position + (direction * chestDistanceToCamera);
        GameObject chestObject = Instantiate(chest, chestPosition, Quaternion.identity);
        Vector3 cameraRelativePosition = new Vector3(cam.transform.position.x,
            chestObject.transform.position.y, cam.transform.position.z);
        chestObject.transform.LookAt(cameraRelativePosition);
        chestAnimator = chestObject.GetComponent<Animator>();
        FillCards();
        StartCoroutine(WaitForAnimation(chestObject));

    }
    public IEnumerator WaitForAnimation(GameObject chest)
    {
        float animLength = chestAnimator.GetCurrentAnimatorStateInfo(0).length;
        float speed = chestAnimator.GetCurrentAnimatorStateInfo(0).speed;
        float final = animLength * speed;
        yield return new WaitForSeconds(final);
        chestGiftCanvas.SetActive(true);
        Destroy(chest);
    }
    public void FillCards()
    {
        List<SupriseCardContent> temp = new List<SupriseCardContent>();

        foreach (SupriseCardContent content in contents)
        {
            temp.Add(content);
        }

        foreach (ChestCard card in cards)
        {
            int index = Random.Range(0, temp.Count);
            SupriseCardContent content = temp[index];
            card.Init(content, this);
            temp.Remove(content);
        }
    }
    public void OnContentApplied()
    {
        chestGiftCanvas.SetActive(false);
        DiceController.Instace.SetDiceEnable(true);
        DiceController.Instace.SetCanChangeView(true);
    }
}
