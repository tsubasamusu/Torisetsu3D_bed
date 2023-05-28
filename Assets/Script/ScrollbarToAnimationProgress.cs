using UnityEngine;
using UnityEngine.UI;

public class ScrollbarToAnimationProgress : MonoBehaviour
{
    public Animator animator;
    private Scrollbar scrollbar;
    private bool userInteraction; // user is interacting with the scrollbar

    private void Start()
    {
        scrollbar = GetComponent<Scrollbar>();

        if (scrollbar == null)
        {
            Debug.LogError("Scrollbar component not found");
            return;
        }

        if (animator == null)
        {
            Debug.LogError("Animator is not assigned");
            return;
        }

        scrollbar.onValueChanged.AddListener(OnScrollbarValueChange);
    }

    private void OnScrollbarValueChange(float value)
    {
        userInteraction = true;

        if (animator != null)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            animator.Play(stateInfo.fullPathHash, -1, value);
        }
    }

    private void Update()
    {
        if (animator != null && scrollbar != null)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (!userInteraction) // update scrollbar value only when user is not interacting with it
            {
                scrollbar.value = stateInfo.normalizedTime % 1; // % 1 is used for looping animations
            }

            if (!Input.GetMouseButton(0)) // check for mouse or touch release
            {
                userInteraction = false;
            }
        }
    }
}