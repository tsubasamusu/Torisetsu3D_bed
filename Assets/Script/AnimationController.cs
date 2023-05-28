using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField]
    private Animator bedAnimator;

    private float defaultSpeed;

    [SerializeField]
    private float speed;

    private bool isReturn;

    /// <summary>
    /// アニメーションの名前
    /// </summary>
    [SerializeField]
    private string animationName;

    [SerializeField]
    private float speedUpVelocity = 1.5f;

    [SerializeField]
    private float speedDownVelocity = 1.5f;

    private void Start()
    {
        if (bedAnimator == null)
        {
            Debug.LogError("Animator is not assigned");
            return;
        }
        defaultSpeed = bedAnimator.speed;
        bedAnimator.speed = 0; // initially stop the animation
    }

    public void Update()
    {
        speed = bedAnimator.speed;
    }

    public void StartAnimation()
    {
        isReturn = false;
        bedAnimator.SetFloat("Speed",1f); 
        bedAnimator.speed = defaultSpeed;
    }

    public void StopAnimation()
    {
        bedAnimator.speed = 0;
    }

    public void SpeedUpAnimation()
    {
        isReturn = false;
        bedAnimator.SetFloat("Speed",1f); 
        bedAnimator.speed = speedUpVelocity * defaultSpeed;
    }

    public void SlowDownAnimation()
    {
        isReturn = false;
        bedAnimator.SetFloat("Speed",1f); 
        bedAnimator.speed = defaultSpeed / speedDownVelocity;
    }

    public void ReturnAnimation()
    {
        if (!isReturn)
        {
            isReturn = true;
            bedAnimator.SetFloat("Speed", -1f);  
            bedAnimator.speed = defaultSpeed;
        }
    }

    public void ResetAnimation()
    {
        bedAnimator.Play(animationName, 0, 0); // Play animation from the beginning
        bedAnimator.speed = defaultSpeed; // Set speed to 0
    }
}