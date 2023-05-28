using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AnimationProgressBar : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Animator animator;  // Animator reference
    public string animationName;  // Animation clip name
    private Scrollbar scrollbar;  // Scrollbar reference
    private bool isDragging;  // To track scrollbar interaction

    private void Start()
    {
        scrollbar = GetComponent<Scrollbar>();
    }

    private void Update()
    {
        // Update the scrollbar value only when not interacting with it
        if (!isDragging)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName(animationName))
            {
                scrollbar.value = stateInfo.normalizedTime % 1;  // Looping between 0 and 1
            }
        }
    }

    // Implementing interface members to handle pointer events
    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
        UIInteractionManager.Instance.IsScrollbarInteracting = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
        UIInteractionManager.Instance.IsScrollbarInteracting = false;
        animator.Play(animationName, -1, scrollbar.value);  // Play animation from the point dictated by scrollbar
    }
}