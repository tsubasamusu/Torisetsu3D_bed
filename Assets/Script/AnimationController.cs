using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator animator;
    private float defaultSpeed;
    public float speed;
    private bool isReturn; 
    public string animationName; // Name of the animation clip
    public float speedUpVelocity = 1.5f;
    public float speedDownVelocity = 1.5f;

    private void Start()
    {
        if (animator == null)
        {
            Debug.LogError("Animator is not assigned");
            return;
        }
        defaultSpeed = animator.speed;
        animator.speed = 0; // initially stop the animation
    }

    public void Update()
    {
        speed = animator.speed;
    }

    public void StartAnimation()
    {
        isReturn = false;
        animator.SetFloat("Speed",1f); 
        animator.speed = defaultSpeed;
    }

    public void StopAnimation()
    {
        animator.speed = 0;
    }

    public void SpeedUpAnimation()
    {
        isReturn = false;
        animator.SetFloat("Speed",1f); 
        animator.speed = speedUpVelocity * defaultSpeed;
    }

    public void SlowDownAnimation()
    {
        isReturn = false;
        animator.SetFloat("Speed",1f); 
        animator.speed = defaultSpeed / speedDownVelocity;
    }

    public void ReturnAnimation()
    {
        if (!isReturn)
        {
            isReturn = true;
            animator.SetFloat("Speed", -1f);  
            animator.speed = defaultSpeed;
        }
    }

    public void ResetAnimation()
    {
        animator.Play(animationName, 0, 0); // Play animation from the beginning
        animator.speed = defaultSpeed; // Set speed to 0
    }
}