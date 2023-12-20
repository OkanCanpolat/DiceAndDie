using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SupriseCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    
    public Button Button;
    [SerializeField] private Sprite mouseEnterSprite;
    [SerializeField] private Sprite mouseExitSprite;
    [SerializeField] private Image questionMarkImage;
    [SerializeField] private TMP_Text descriptonText;
    [SerializeField] private Image contentImage;
    private SupriseWayPoint wayPoint;
    private SupriseCardContent Content;
    private Animator animator;
    private AudioSource audioSource;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    public void Init(SupriseCardContent content, SupriseWayPoint wayPoint)
    {
        Content = content;
        contentImage.sprite = Content.contentSprite;
        descriptonText.text = Content.description;
        this.wayPoint = wayPoint;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        questionMarkImage.sprite = mouseEnterSprite;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        questionMarkImage.sprite = mouseExitSprite;
    }
    public void OnFlipFinished()
    {
        audioSource.PlayOneShot(Content.OnFlipClip);
        Content.Action();
        StartCoroutine(WaitForClipEnd());
    }
    public void FlipCard()
    {
        audioSource.PlayOneShot(Content.OnClickClip);
        wayPoint.DeactivateButtons();
        animator.SetTrigger("Flip");
    }

    private IEnumerator WaitForClipEnd()
    {
        while (audioSource.isPlaying)
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        wayPoint.FinishAction();
    }
}
