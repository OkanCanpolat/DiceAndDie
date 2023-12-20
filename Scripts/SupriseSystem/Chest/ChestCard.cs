using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChestCard : MonoBehaviour
{
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private Image image;
    private SupriseCardContent content;
    private AudioSource audioSource;
    private ChestManager chestManager;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void Init(SupriseCardContent card, ChestManager manager)
    {
        content = card;
        descriptionText.text = content.description;
        image.sprite = content.contentSprite;
        chestManager = manager;
    }
    public void OnClick()
    {
        StartCoroutine(ApplyContent());
    }

    private IEnumerator ApplyContent()
    {
        audioSource.PlayOneShot(content.OnClickClip);

        while (audioSource.isPlaying)
        {
            yield return null;
        }

        content.Action();
        chestManager.OnContentApplied();
    }
}
